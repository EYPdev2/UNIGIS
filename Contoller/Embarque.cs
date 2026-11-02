using ActualizadorDoctosUnigis.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualizadorDoctosUnigis.Contoller
{
    class Embarque
    {
        public SqlConnection ObtenerConexion(string cod_estab)
        {
            cod_estab = cod_estab.ToString().Replace("EYP", "");

            DataTable dt = new DataTable();
            using (SqlConnection central = new Conexion().conexion())
            using (SqlCommand cmd = new SqlCommand("select servidor,base_datos from eyp_servidores where cod_estab=@cod_estab", central))
            {
                cmd.Parameters.Add("@cod_estab", SqlDbType.VarChar, 50).Value = cod_estab;
                new SqlDataAdapter(cmd).Fill(dt);
            }

            if (dt.Rows.Count == 0)
                return null;

            string servidor = dt.Rows[0][0].ToString().Trim();
            string bd = dt.Rows[0][1].ToString().Trim();
            if (servidor == "" || bd == "")
                return null;

            return new Conexion().conexion(servidor, bd);
        }

        public string BuscarFolioDelViaje(string estab, string viaje)
        {
            using (SqlConnection con = ObtenerConexion(estab))
            {
                if (con == null)
                    return null;

                con.Open();

                using (SqlCommand cmd = new SqlCommand("select top 1 folio from embarques where transaccion = '43' and notas = @notas and status = 'V' order by folio desc", con))
                {
                    cmd.Parameters.Add("@notas", SqlDbType.VarChar, 100).Value = "Viaje : " + viaje;
                    object folio = cmd.ExecuteScalar();
                    return folio == null || folio == DBNull.Value ? "" : folio.ToString().Trim();
                }
            }
        }

        public List<Entrega> CrearEmbarque(string estab, ObtenerViaje_Response.Rootobject ovra, string usuario, string viaje, List<Entrega> entregas, out string folioEmbarque)
        {
            folioEmbarque = "";
            using (SqlConnection con = ObtenerConexion(estab))
            {
                if (con == null)
                    throw new InvalidOperationException("El establecimiento '" + estab + "' no tiene servidor o base de datos configurados en eyp_servidores.");

                con.Open();
                using (SqlTransaction tx = con.BeginTransaction())
                {
                    DateTime fechaInicio = ovra.d.FechaInicioReal.Value;
                    DateTime fechaFin = ovra.d.FechaFinReal ?? DateTime.Now;

                    string folio;
                    using (SqlCommand cmd = new SqlCommand("EYP_InsertarEmb", con, tx))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;
                        cmd.Parameters.AddWithValue("@FechaInicio", fechaInicio);
                        cmd.Parameters.AddWithValue("@fechaFin", fechaFin);
                        cmd.Parameters.AddWithValue("@vehiculo", ovra.d.Vehiculo.Dominio.Trim());
                        cmd.Parameters.AddWithValue("@Fecha", fechaInicio);
                        cmd.Parameters.AddWithValue("@Peso", ovra.d.Peso);
                        cmd.Parameters.AddWithValue("@Volumen", ovra.d.Volumen);
                        cmd.Parameters.AddWithValue("@Cod_estab", estab);
                        cmd.Parameters.AddWithValue("@Chofer", ovra.d.Conductor.ReferenciaExterna);
                        cmd.Parameters.AddWithValue("@usuario", usuario);
                        cmd.Parameters.AddWithValue("@Viaje", viaje);
                        object resultado = cmd.ExecuteScalar();
                        if (resultado == null || resultado == DBNull.Value || resultado.ToString().Trim() == "")
                            throw new InvalidOperationException("El procedimiento EYP_InsertarEmb no devolvió el folio del embarque.");
                        folio = resultado.ToString().Trim();
                        folioEmbarque = folio;
                    }

                    List<Entrega> omitidas = InsertarRenglones(con, tx, folio, entregas);

                    tx.Commit();
                    return omitidas;
                }
            }
        }

        // Reintento: manda los renglones al SP dentro del embarque que ya existe. No manda los que ya estan procesados en ese embarque:
        // los de entrega (entregada > 0) en embarques_productos con la misma cantidad, y los No Entregado (entregada = 0) en
        // EYP_UNIGIS_embarques_productos con la misma cantidad programada.
        public List<Entrega> AgregarRenglones(string estab, string folioEmbarque, List<Entrega> entregas, out int yaProcesados)
        {
            yaProcesados = 0;
            using (SqlConnection con = ObtenerConexion(estab))
            {
                if (con == null)
                    throw new InvalidOperationException("El establecimiento '" + estab + "' no tiene servidor o base de datos configurados en eyp_servidores.");

                con.Open();
                using (SqlTransaction tx = con.BeginTransaction())
                {
                    HashSet<string> embarcados = LeerRenglonesDelEmbarque(con, tx, "embarques_productos", folioEmbarque);
                    HashSet<string> noEntregados = LeerRenglonesDelEmbarque(con, tx, "EYP_UNIGIS_embarques_productos", folioEmbarque);

                    List<Entrega> porEmbarcar = new List<Entrega>();
                    foreach (Entrega entrega in entregas)
                    {
                        bool entregada = entrega.Cantidad_entregada > 0;
                        string clave = entrega.referencia_ext.Trim() + "|" + entrega.Transaccion.Trim() + "|" + entrega.Cod_prod.Trim() + "|"
                            + FormatoCantidad((decimal)(entregada ? entrega.Cantidad_entregada : entrega.cantidad));
                        if ((entregada ? embarcados : noEntregados).Contains(clave))
                            yaProcesados++;
                        else
                            porEmbarcar.Add(entrega);
                    }

                    List<Entrega> omitidas = InsertarRenglones(con, tx, folioEmbarque, porEmbarcar);

                    tx.Commit();
                    return omitidas;
                }
            }
        }

        private static HashSet<string> LeerRenglonesDelEmbarque(SqlConnection con, SqlTransaction tx, string tabla, string folioEmbarque)
        {
            HashSet<string> renglones = new HashSet<string>();
            using (SqlCommand cmd = new SqlCommand("select rtrim(folio), rtrim(transaccion), rtrim(cod_prod), cantidad from " + tabla + " where rtrim(embarque) = @embarque", con, tx))
            {
                cmd.Parameters.Add("@embarque", SqlDbType.VarChar, 15).Value = folioEmbarque;
                using (SqlDataReader lector = cmd.ExecuteReader())
                {
                    while (lector.Read())
                        renglones.Add(Convert.ToString(lector[0]).Trim() + "|" + Convert.ToString(lector[1]).Trim() + "|" + Convert.ToString(lector[2]).Trim() + "|" + FormatoCantidad(Convert.ToDecimal(lector[3])));
                }
            }
            return renglones;
        }

        private static string FormatoCantidad(decimal cantidad)
        {
            return cantidad.ToString("0.####", System.Globalization.CultureInfo.InvariantCulture);
        }

        private List<Entrega> InsertarRenglones(SqlConnection con, SqlTransaction tx, string folioEmbarque, List<Entrega> entregas)
        {
            List<Entrega> omitidas = new List<Entrega>();
            foreach (Entrega entrega in entregas)
            {
                using (SqlCommand cmd = new SqlCommand("EYP_InsertarEmb_detalle", con, tx))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 120;
                    cmd.Parameters.AddWithValue("@Embarque", folioEmbarque);
                    cmd.Parameters.AddWithValue("@Folio", entrega.referencia_ext);
                    cmd.Parameters.AddWithValue("@Transaccion", entrega.Transaccion);
                    cmd.Parameters.AddWithValue("@cod_prod", entrega.Cod_prod);
                    cmd.Parameters.AddWithValue("@Cantidad", entrega.cantidad);
                    cmd.Parameters.AddWithValue("@Unidad", "");
                    cmd.Parameters.AddWithValue("@Cantidad_entregada", entrega.Cantidad_entregada);
                    object respuesta = cmd.ExecuteScalar();
                    if (respuesta != null && respuesta != DBNull.Value)
                        omitidas.Add(entrega);
                }

                if (entrega.Cantidad_entregada == 0)
                    RegistrarNoEntregado(con, tx, folioEmbarque, entrega);
            }
            return omitidas;
        }

        // El SP no deja renglon de los No Entregado; se registra aqui, con la cantidad programada, para no volver a procesarlos en un reintento.
        // Solo se registra si el folio tiene cantidad pendiente en productos_por_embarcar.
        private void RegistrarNoEntregado(SqlConnection con, SqlTransaction tx, string folioEmbarque, Entrega entrega)
        {
            using (SqlCommand pendiente = new SqlCommand("select isnull(sum(cantidad), 0) from productos_por_embarcar where folio = @folio and transaccion = @transaccion and cod_prod = @cod_prod", con, tx))
            {
                pendiente.CommandTimeout = 120;
                pendiente.Parameters.Add("@folio", SqlDbType.VarChar, 15).Value = entrega.referencia_ext.Trim();
                pendiente.Parameters.Add("@transaccion", SqlDbType.VarChar, 15).Value = entrega.Transaccion.Trim();
                pendiente.Parameters.Add("@cod_prod", SqlDbType.VarChar, 15).Value = entrega.Cod_prod.Trim();
                if (Convert.ToDecimal(pendiente.ExecuteScalar()) <= 0)
                    return;
            }

            using (SqlCommand cmd = new SqlCommand(
                "insert into EYP_UNIGIS_embarques_productos (embarque, folio, transaccion, unidad, cod_prod, cantidad) "
                + "select @embarque, @folio, @transaccion, isnull((select top 1 unidad from entysal where folio = @folio and transaccion = @transaccion and cod_prod = @cod_prod), ''), @cod_prod, @cantidad", con, tx))
            {
                cmd.CommandTimeout = 120;
                cmd.Parameters.Add("@embarque", SqlDbType.VarChar, 15).Value = folioEmbarque;
                cmd.Parameters.Add("@folio", SqlDbType.VarChar, 15).Value = entrega.referencia_ext.Trim();
                cmd.Parameters.Add("@transaccion", SqlDbType.VarChar, 15).Value = entrega.Transaccion.Trim();
                cmd.Parameters.Add("@cod_prod", SqlDbType.VarChar, 15).Value = entrega.Cod_prod.Trim();
                SqlParameter cantidad = cmd.Parameters.Add("@cantidad", SqlDbType.Decimal);
                cantidad.Precision = 19;
                cantidad.Scale = 4;
                cantidad.Value = (decimal)entrega.cantidad;
                cmd.ExecuteNonQuery();
            }
        }

        public SqlConnection conexionSucursal(string establecimiento)
        {
            using (SqlConnection openCon = new SqlConnection("Data Source = 192.168.8.4\\bms; Initial Catalog = BMS; User ID = sa; Password = @Sistemas74"))
            {
                StringBuilder qry = new StringBuilder();
                DataTable DTCon = new DataTable();
                qry.Append(" select servidor,base_datos from eyp_servidores where Cod_estab='" + establecimiento + "'");
                openCon.Open();
                string select = qry.ToString();
                qry.Clear();
                openCon.Open();
                using (SqlCommand querySaveStaff = new SqlCommand(select, openCon))
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = select;
                    cmd.CommandType = CommandType.Text;
                    querySaveStaff.Connection = openCon;
                    cmd.Connection = openCon;
                    SqlDataAdapter sqlda = new SqlDataAdapter(cmd);
                    sqlda.Fill(DTCon);

                    SqlConnection sql = new SqlConnection("Data Source=" + DTCon.Rows[0][0].ToString() + "; Initial Catalog = " + DTCon.Rows[0][1].ToString() + "; User ID =sa; Password= @Sistemas74");

                    return sql;
                }
            }


        }

    }
}

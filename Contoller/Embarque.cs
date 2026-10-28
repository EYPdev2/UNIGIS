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
    class ResultadoEmbarque
    {
        public string Folio = "";
        public bool Creado = false;
        public int Partidas = 0;
        public List<string> SinPendiente = new List<string>();
    }

    class Embarque
    {

        public string obtenerServidor(string cod_estab)
        {
            string conexion = "";
            cod_estab = cod_estab.ToString().Replace("EYP", "");

            DataTable dt = new DataTable();
            using (SqlConnection openCon = new SqlConnection("Data Source = 192.168.8.4\\BMS ; Initial Catalog = BMS; User ID = sa; Password = @Sistemas74"))
            {
                string saveStaff = " select servidor,base_datos from eyp_servidores where cod_estab='"+cod_estab+"'";

                using (SqlCommand querySaveStaff = new SqlCommand(saveStaff))
                {
                    Conexion cnn = new Conexion();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = saveStaff;
                    cmd.CommandType = CommandType.Text;
                    querySaveStaff.Connection = openCon;
                    cmd.Connection = cnn.conexion();
                    openCon.Open();
                    SqlDataAdapter sqlDA = new SqlDataAdapter(cmd);
                    sqlDA.Fill(dt);
                    cnn.cerrarconexion(cnn.conexion());
                }


                if (dt.Rows.Count == 0)
                    throw new Exception("El establecimiento " + cod_estab + " no está registrado en Eyp_Servidores.");

                // Si la sucursal no responde se detiene todo: en la central no existe EYP_InsertarEmb
                SqlConnection sqlconnection = new SqlConnection();
                try
                {
                    sqlconnection.ConnectionString = "Data Source = " + dt.Rows[0].ItemArray[0].ToString() + "; Initial Catalog = " + dt.Rows[0].ItemArray[1].ToString() + "; User ID = sa; Password = @Sistemas74";
                    sqlconnection.Open();
                    sqlconnection.Close();
                    conexion = dt.Rows[0].ItemArray[0].ToString() + "-" + dt.Rows[0].ItemArray[1].ToString();

                }

                catch (Exception ex)
                {
                    throw new Exception("No hay conexión con el servidor de la sucursal (" + dt.Rows[0].ItemArray[0].ToString().Trim() + "): " + ex.Message, ex);
                }

                return conexion;






            }
        }
        // Encabezado y detalle van en una sola transaccion en la sucursal: si algo falla no queda un embarque a medias,
        // y si ningun producto tenia algo pendiente se deshace todo para no dejar embarques vacios
        public ResultadoEmbarque GenerarEmbarque(string estab, ObtenerViaje_Response.Rootobject ovra, string usuario, string viaje, List<Entrega> entregas)
        {
            string servidorA = obtenerServidor(estab);
            string servidor = servidorA.Substring(0, servidorA.IndexOf('-'));
            string BD = servidorA.Substring(servidorA.IndexOf('-') + 1);
            ResultadoEmbarque resultado = new ResultadoEmbarque();

            Conexion cnn = new Conexion();
            using (SqlConnection openCon = cnn.conexion(servidor, BD))
            {
                openCon.Open();
                SqlTransaction tran = openCon.BeginTransaction();
                try
                {
                    resultado.Folio = Insertar_embarque(openCon, tran, estab, ovra, usuario, viaje);

                    bool huboMovimiento = false;
                    foreach (Entrega entrega in entregas)
                    {
                        string mensaje = Insertar_embarque_detalle(openCon, tran, entrega, resultado.Folio);
                        if (mensaje == "")
                            huboMovimiento = true;
                        else
                            resultado.SinPendiente.Add(entrega.Documento + " producto " + entrega.Cod_prod.Trim());
                    }

                    if (!huboMovimiento)
                    {
                        tran.Rollback();
                        resultado.Folio = "";
                        return resultado;
                    }

                    using (SqlCommand cuenta = new SqlCommand("select count(*) from embarques_productos where embarque = @Embarque", openCon, tran))
                    {
                        cuenta.Parameters.AddWithValue("@Embarque", resultado.Folio);
                        resultado.Partidas = Convert.ToInt32(cuenta.ExecuteScalar());
                    }

                    tran.Commit();
                    resultado.Creado = true;
                }
                catch
                {
                    try { tran.Rollback(); } catch (Exception) { }
                    throw;
                }
            }
            return resultado;
        }

        private string Insertar_embarque(SqlConnection openCon, SqlTransaction tran, string estab, ObtenerViaje_Response.Rootobject ovra, string usuario, string viaje)
        {
            if (ovra.d.FechaInicioReal == null)
                throw new Exception("El viaje " + viaje + " no tiene fecha de inicio real en Unigis.");

            //Validamos las fechas
            DateTime fecha_final = DateTime.Now;
            if (ovra.d.FechaFinReal != null)
            {
                fecha_final = ovra.d.FechaFinReal.Value;
            }

            using (SqlCommand cmd = new SqlCommand("EYP_InsertarEmb", openCon, tran))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FechaInicio", ovra.d.FechaInicioReal.Value);
                cmd.Parameters.AddWithValue("@fechaFin", fecha_final);
                cmd.Parameters.AddWithValue("@vehiculo", ovra.d.Vehiculo.Dominio.Trim());
                cmd.Parameters.AddWithValue("@Fecha", ovra.d.FechaInicioReal.Value);
                cmd.Parameters.AddWithValue("@Peso", ovra.d.Peso);
                cmd.Parameters.AddWithValue("@Volumen", ovra.d.Volumen);
                cmd.Parameters.AddWithValue("@Cod_estab", estab);
                cmd.Parameters.AddWithValue("@Chofer", ovra.d.Conductor.ReferenciaExterna);
                cmd.Parameters.AddWithValue("@usuario", usuario);
                cmd.Parameters.AddWithValue("@Viaje", viaje);
                DataTable dt = new DataTable();
                SqlDataAdapter sqlDA = new SqlDataAdapter(cmd);
                sqlDA.Fill(dt);
                return dt.Rows[0].ItemArray[0].ToString();
            }
        }

        // Regresa "" cuando el SP embarco el producto o lo regreso a pendiente; si no tenia nada pendiente regresa el mensaje del SP
        private string Insertar_embarque_detalle(SqlConnection openCon, SqlTransaction tran, Entrega entregas, string FolioE)
        {
            using (SqlCommand cmd = new SqlCommand("EYP_InsertarEmb_detalle", openCon, tran))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Embarque", FolioE);
                cmd.Parameters.AddWithValue("@Folio", entregas.referencia_ext);
                cmd.Parameters.AddWithValue("@Transaccion", entregas.Transaccion);
                cmd.Parameters.AddWithValue("@cod_prod", entregas.Cod_prod);
                cmd.Parameters.AddWithValue("@Cantidad", entregas.cantidad);
                cmd.Parameters.AddWithValue("@Unidad", "");
                cmd.Parameters.AddWithValue("@Cantidad_entregada", entregas.Cantidad_entregada);
                DataTable dt = new DataTable();
                SqlDataAdapter sqlDA = new SqlDataAdapter(cmd);
                sqlDA.Fill(dt);
                return dt.Rows.Count > 0 ? dt.Rows[0][0].ToString().Trim() : "";
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

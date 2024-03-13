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
                    conexion = "192.168.8.4\\BMS-BMS";
                }

                return conexion;






            }
        }
        public string Insertar_embarque(string estab, ObtenerViaje_Response.Rootobject ovra,string usuario,string viaje)
        {
            string servidorA = obtenerServidor(estab);
            string servidor = servidorA.Substring(0, servidorA.IndexOf('-'));
            string BD = servidorA.Substring(servidorA.IndexOf('-') + 1);
            DataTable dt = new DataTable();
            SqlConnection openConA = new SqlConnection("Data Source = " + servidor + "; Initial Catalog = " + BD + "; User ID = sa; Password = @Sistemas74");



            using (SqlConnection openCon = new SqlConnection("Data Source = " + servidor + "; Initial Catalog = " + BD + "; User ID = sa; Password = @Sistemas74"))
            //using (SqlConnection openCon = new SqlConnection("Data Source = 192.168.8.4\\bms; Initial Catalog = BMS03082022; User ID = sa; Password = @Sistemas74"))
            {

                //Validamos las fechas 
                DateTime fecha_final = DateTime.Now;
                if (ovra.d.FechaFinReal != null)
                {
                    fecha_final = ovra.d.FechaFinReal.Value ;
                }

                StringBuilder qry = new StringBuilder();
                qry.Append("EYP_InsertarEmb");
        
                openCon.Open();
                string insert = qry.ToString();
                using (SqlCommand querySaveStaff = new SqlCommand(insert, openCon))
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = insert;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FechaInicio", ovra.d.FechaInicioReal.Value);
                    cmd.Parameters.AddWithValue("@fechaFin", fecha_final);
                    cmd.Parameters.AddWithValue("@vehiculo", ovra.d.Vehiculo.Dominio.Trim());
                    cmd.Parameters.AddWithValue("@Fecha", ovra.d.FechaInicioReal.Value);
                    cmd.Parameters.AddWithValue("@Peso",  ovra.d.Peso );
                    cmd.Parameters.AddWithValue("@Volumen", ovra.d.Volumen);
                    cmd.Parameters.AddWithValue("@Cod_estab", estab);
                    cmd.Parameters.AddWithValue("@Chofer", ovra.d.Conductor.ReferenciaExterna);
                    cmd.Parameters.AddWithValue("@usuario", usuario);
                    cmd.Parameters.AddWithValue("@Viaje",viaje);
                    querySaveStaff.Connection = openCon;
                    cmd.Connection = openCon;
                    SqlDataAdapter sqlDA = new SqlDataAdapter(cmd);
                    string com =cmd.CommandText.ToString();
                    string com2 = cmd.Parameters.ToString();
                    sqlDA.Fill(dt);
                    return dt.Rows[0].ItemArray[0].ToString();


                }
            }




        }

        public  void Insertar_embarque_detalle(Entrega entregas, string estab ,string FolioE)
        {
            string servidorA = obtenerServidor(estab);
            string servidor = servidorA.Substring(0, servidorA.IndexOf('-'));
            string BD = servidorA.Substring(servidorA.IndexOf('-') + 1);
            DataTable dt = new DataTable();
            SqlConnection openConA = new SqlConnection("Data Source = " + servidor + "; Initial Catalog = " + BD + "; User ID = sa; Password = @Sistemas74");



            using (SqlConnection openCon = new SqlConnection("Data Source = " + servidor + "; Initial Catalog = " + BD + "; User ID = sa; Password = @Sistemas74"))
            //using (SqlConnection openCon = new SqlConnection("Data Source = 192.168.8.4\\bms; Initial Catalog = BMS03082022; User ID = sa; Password = @Sistemas74"))
            {

                 

                StringBuilder qry = new StringBuilder();
                qry.Append("    ");

                openCon.Open();
                string insert = qry.ToString();
                using (SqlCommand querySaveStaff = new SqlCommand(insert, openCon))
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = insert;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Embarque", FolioE);
                    cmd.Parameters.AddWithValue("@Folio", entregas.referencia_ext);
                    cmd.Parameters.AddWithValue("@Transaccion",entregas.Transaccion);
                    cmd.Parameters.AddWithValue("@cod_prod", entregas.Cod_prod);
                    cmd.Parameters.AddWithValue("@Cantidad", entregas.cantidad);
                    cmd.Parameters.AddWithValue("@Unidad", "");
                    cmd.Parameters.AddWithValue("@Cantidad_entregada" ,entregas.Cantidad_entregada);
                    
                    querySaveStaff.Connection = openCon;
                    cmd.Connection = openCon;
                    SqlDataAdapter sqlDA = new SqlDataAdapter(cmd);
                    sqlDA.Fill(dt);
                     


                }
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

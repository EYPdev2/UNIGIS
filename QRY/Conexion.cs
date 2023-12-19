using System.Data.SqlClient;

namespace ActualizadorDoctosUnigis
{
    public class Conexion
    {

        public SqlConnection conexion()
        {

            string CONSQL;
            CONSQL = "Data Source=192.168.8.4,9001 ;Initial Catalog=BMS;User ID=sa;Password=@Sistemas74";
            SqlConnection con = new SqlConnection(CONSQL);
            return con;
        }
        public SqlConnection conexion(string server,string BDD)
        {

            string CONSQL;
            CONSQL = "Data Source="+server+";Initial Catalog=" + BDD.ToString()+";User ID=sa;Password=@Sistemas74";
            SqlConnection con = new SqlConnection(CONSQL);
            return con;
        }


        public void cerrarconexion(SqlConnection CON)
        {

            CON.Close();

        }
    }
}

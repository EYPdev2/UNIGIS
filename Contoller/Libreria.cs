using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualizadorDoctosUnigis.Contoller
{
    class Libreria
    {
        Qrys Q = new Qrys();

        public string Decimales(string cantidad, string producto)
        {
            DataTable decimales = Q.Consultar(" select decimales from productos where cod_prod='" + producto + "'");
            int NDecimal = Convert.ToInt32(decimales.Rows[0][0].ToString());
            if (cantidad.ToString().IndexOf('.') > -1)
            {
                int idx = cantidad.IndexOf('.');
                int idx2 = cantidad.Length;
                if (NDecimal >= cantidad.Substring(idx, cantidad.Length - idx).Length)
                {
                    if (idx + 1 - idx2 == 0)
                    {
                        return cantidad.Substring(0, idx) + ".00";
                    }
                    else if(cantidad.Substring(idx, cantidad.Length - idx).Length>0)
                    {

                        //MessageBox.Show(cantidad);
                        return cantidad ;
                    }
                    else
                    {
                        return cantidad + ".00";
                    }
                }
                else
                {
                    return cantidad;
                }

            }
            else { return cantidad + ".00"; }

        }
        public string folio(string referencia_ext, int modo)
        {
            int idx = referencia_ext.IndexOf('-');
            if (modo == 0)
            {
                string s = referencia_ext.Substring(idx + 1);
                int idx2 = referencia_ext.Substring(idx + 1).IndexOf('|');
                s = s.Substring(0, idx2);
                return s;
            }
            else
            {
                string tran = referencia_ext.Substring(0, idx);
                switch (referencia_ext.Substring(0, idx))
                {
                    case "EdMaC": tran = "713"; break;
                    case "FC": tran = "36"; break;
                    case "RC": tran = "37"; break;
                    case "TE": tran = "35";break;

                    default:tran =""; break;

                        //case "PE":tran = "29";break;
                }
                return tran;
            }




        }

        public bool validaEstab(string usuario, string Deposito)
        {
            DataTable dtUsuario = Q.Consultar("select cod_estab from establecimientos_usuario where usuario='" + usuario + "' and acceso='1'");
            DataTable dtDeposito = Q.Consultar(" select cod_estab  from Eyp_Servidores eyp where  unigis='18'");

            foreach (DataRow row in dtUsuario.Rows)
            {
                if (row[0].ToString() == "18")
                {
                    return true;
                }

            }

            return false;
        }
        public DataTable Depositos_usuario(string usuario)
        {
            DataTable dtUsuario = Q.Consultar(" select unigis from Eyp_Servidores e " +
            " left join establecimientos_usuario eu on e.Cod_estab = eu.cod_estab" +
                " where eu.usuario = '" + usuario + "' and e.unigis is not null ");
            return dtUsuario;

        }

        public DataTable Conexiones(string folio)
        {
            DataTable dt = Q.Consultar("select e.cod_estab,eyp.servidor,base_datos from establecimientos e " +
                                       "left join Eyp_Servidores eyp on e.cod_estab = eyp.Cod_estab " +
                                       "where e.abreviatura = '" + folio.Substring(0, 3) + "'");
            if (dt.Rows.Count == 0)
            {
                dt = Q.Consultar("select e.cod_estab,eyp.servidor,base_datos from establecimientos e " +
                                       "left join Eyp_Servidores eyp on e.cod_estab = eyp.Cod_estab " +
                                       "where e.abreviatura = '" + folio.Substring(0, 2) + "'");
            }



            return dt;
        }

    }
}

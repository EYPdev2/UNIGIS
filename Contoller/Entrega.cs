using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ActualizadorDoctosUnigis.Contoller
{
    class Entrega
    {

        public string Cod_prod { get; set; }
        public string referencia_ext { get; set; }
        public double Cantidad_entregada { get; set; }
        public double cantidad { get; set; }
        public string Transaccion { get; set; }
        public string vehiculo { get; set; }
        public string chofer { get; set; }

        public string Estado { get; set; }
        public Entrega(DataRow dgv,string ch, string v )
        {
            Cod_prod = dgv["Producto"].ToString();
            referencia_ext = folio(dgv["Ref.Documento"].ToString(), 0);
            cantidad = Convert.ToDouble(dgv["Cantidad"].ToString());
            Cantidad_entregada = Convert.ToDouble(dgv["Cantidad_Entregada"].ToString()); ;
            Transaccion = folio(dgv["Ref.Documento"].ToString(), 1);
            vehiculo = v;
            chofer = ch;
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
            else {
                string tran = referencia_ext.Substring(0, idx);
                switch (referencia_ext.Substring(0, idx))
                {
                    case "EdMaC": tran = "713"; break;
                    case "FC": tran = "36"; break;
                    case "RC": tran = "37"; break;
                }
                return tran;
            }




        }



    }


}

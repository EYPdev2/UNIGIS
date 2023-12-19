using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ActualizadorDoctosUnigis
{
    public partial class ModificarFechaEntrega : Form
    {
        private string conBDD = "";
        private string conSERV = "";
        Qrys q = new Qrys();
        string[,] T = new string[20, 20];
        int count;
        public ModificarFechaEntrega()
        {
            InitializeComponent();
        }

        private void dtp_fechaDcto_ValueChanged(object sender, EventArgs e)
        {

        }

        private void ModificarFechaEntrega_Load(object sender, EventArgs e)
        {
            cargarTransaccion();
        }
        public void cargarTransaccion()
        {
            DataTable dt = q.catalogoTrans();
            count = 0;
            cmb_transaccion.Items.Clear();
            foreach (DataRow row in dt.Rows)
            {
                T[count, 0] = row.ItemArray[0].ToString();
                cmb_transaccion.Items.Add(row.ItemArray[0].ToString());
                T[count, 1] = row.ItemArray[1].ToString();
                count += 1;
            }
        }

        private string obtenertransaccion(string tr)
        {
            string aux = "";
            for (int x = 0; x < count; x++)
            {

                if (T[x, 0] == tr) { aux = T[x, 1]; }

            }

            return aux;
        }
        public void conexiones(string folio)
        {
            if (folio.Substring(0, 2) == "CD")
            {
                conSERV = "192.168.8.4\\BMS";
                conBDD = "BMS";
            }
            else if (folio.Substring(0, 2) == "NM")
            {
                conSERV = "192.168.6.99\\BMS";
                conBDD = "BMS";
            }
            else if (folio.Substring(0, 2) == "SA")
            {
                conSERV = "192.168.1.9\\BMS";
                conBDD = "BMS";
            }
            else if (folio.Substring(0, 2) == "PP")
            {
                conSERV = "192.168.44.99\\BMS";
                conBDD = "BMS";
            }
            else if (folio.Substring(0, 2) == "SL")
            {
                if (folio.Substring(0, 3) == "SLP")
                {
                    conSERV = "192.168.23.99\\BMS";
                    conBDD = "BMS";
                }
                else
                {
                    conSERV = "192.168.5.99\\BMS";
                    conBDD = "BMS";
                }
                }
            else if (folio.Substring(0, 2) == "TJ")
            {
                conSERV = "192.168.7.99\\BMS";
                conBDD = "BMS";
            }
            else if (folio.Substring(0, 2) == "SI")
            {
                conSERV = "192.168.4.99\\BMS";
                conBDD = "BMS";
            }
            else if (folio.Substring(0, 2) == "VR")
            {
                conSERV = "192.168.9.99\\BMS";
                conBDD = "BMS";
            }
            else if (folio.Substring(0, 2) == "CV")
            {
                conSERV = "192.168.1.99\\BMS";
                conBDD = "BMS";
            }
            else if (folio.Substring(0, 3) == "TKT")
            {
                conSERV = "192.168.13.99\\BMS";
                conBDD = "BMS";
            }
            else if (folio.Substring(0, 2) == "SF")
            {
                conSERV = "192.168.4.99\\BMS";
                conBDD = "BMS";
            }
            else if (folio.Substring(0, 2) == "TR")
            {
                conSERV = "192.168.17.99\\BMS";
                conBDD = "BMS";
            }
            else if (folio.Substring(0, 2) == "VP")
            {
                conSERV = "192.168.18.99\\BMS";
                conBDD = "BMS";
            }
            else if (folio.Substring(0, 2) == "HD")
            {
                conSERV = "192.168.22.99\\BMS";
                conBDD = "BMS";
            }
            else if (folio.Substring(0, 3) == "GEN")
            {
                conSERV = "192.168.2.99\\BMS";
                conBDD = "BMSGBC";
            }
            else if (folio.Substring(0, 3) == "GCH")
            {
                conSERV = "192.168.12.99\\BMS";
                conBDD = "BMS";
            }
            else if (folio.Substring(0, 3) == "GAG")
            {
                conSERV = "192.168.15.99\\BMS";
                conBDD = "BMSGBC";
            }

            else if (folio.Substring(0, 3) == "GSQ")
            {
                conSERV = "192.168.16.99\\BMS";
                conBDD = "BMSGBC";
            }

        }
        }


    }



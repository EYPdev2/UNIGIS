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
    public partial class Relacion_Pickup2 : Form
    {
        private Qrys Q = new Qrys();
        private string[] t = new string[5];
        private DataTable DTEM = new DataTable();
        private DataTable DTPPE = new DataTable();
        private string cod_estab;
        private string TA;
        private string usuario = "";
  


        private void Relacion_Pickup2_Load(object sender, EventArgs e)
        {
            DataTable dataTable1 = new DataTable();
            DataTable dataTable2 = this.Q.Consultar("select  nombre,transaccion from transacciones where transaccion in ('36','37')");
            int index = 0;
            foreach (DataRow row in (InternalDataCollectionBase)dataTable2.Rows)
            {
                this.comboBox1.Items.Add(row[0].ToString().Trim());
                this.t[index] = row[1].ToString();
                ++index;
            }
        }
        public Relacion_Pickup2(string user)
        {
            this.usuario = user;
            this.InitializeComponent();
        }

        private void obtenerPickup()
        {
            DataTable dataTable1 = new DataTable();
            DataTable dataTable2 = this.Q.Consultar($"select FOLIO, transaccion,TA.nombre AS  nombre_ta,TA.tipo_atencion,f.cod_estab from facremtick F LEFT JOIN tipos_atencion TA ON TA.tipo_atencion = F.tipo_atencion  where folio ='{this.textBox1.Text}' and transaccion ='{this.t[this.comboBox1.SelectedIndex]}'");
            if (dataTable2.Rows.Count == 0)
                dataTable2 = this.Q.Consultar($"select FOLIO, transaccion,TA.nombre AS  nombre_ta,TA.tipo_atencion,f.cod_estab from facremtick F LEFT JOIN tipos_atencion TA ON TA.tipo_atencion = F.tipo_atencion  where folio ='{this.textBox1.Text}' and transaccion ='{this.t[this.comboBox1.SelectedIndex]}'", "40");
            if (dataTable2.Rows.Count > 0)
            {
                this.cod_estab = dataTable2.Rows[0].ItemArray[4].ToString();
                this.TA = dataTable2.Rows[0].ItemArray[3].ToString();
                if (dataTable2.Rows[0].ItemArray[3].ToString().Trim() == "R" || dataTable2.Rows[0].ItemArray[3].ToString().Trim() == "P")
                {
                    this.DTPPE = new DataTable();
                    this.DTPPE = this.Q.Consultar($"Select * from productos_por_embarcar where folio='{dataTable2.Rows[0].ItemArray[0].ToString()}' and transaccion='{dataTable2.Rows[0].ItemArray[1].ToString()}' and status=0", dataTable2.Rows[0].ItemArray[4].ToString());
                    if (this.DTPPE.Rows.Count != 0)
                    {
                        this.DTEM = this.Q.Consultar($"select e.cod_prod,e.unidad,sum(e.cantidad *case when e.transaccion='713'then -1 else 1 end) as cantidad from movimientos_internos mi   left join entysal e on mi.folio = e.folio and e.transaccion = mi.transaccion  where mi.folio_referencia = '{dataTable2.Rows[0].ItemArray[0].ToString()}' and mi.transaccion_referencia = '{dataTable2.Rows[0].ItemArray[1].ToString()}'  group by e.cod_prod, e.unidad  ", dataTable2.Rows[0].ItemArray[4].ToString());
                        if (this.validarEliminar(this.DTPPE, this.DTEM))
                        {
                            this.textBox2.Text = dataTable2.Rows[0].ItemArray[2].ToString();
                            this.button1.Enabled = true;
                        }
                        else
                        {
                            int num = (int)MessageBox.Show("El Folio ya fue Entregado Parcialmente", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            this.limpiar();
                        }
                    }
                    else
                    {
                        int num = (int)MessageBox.Show("El Folio ya fue Entregado", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.limpiar();
                    }
                }
                else
                {
                    this.textBox2.Text = dataTable2.Rows[0].ItemArray[2].ToString();
                    int num = (int)MessageBox.Show("El Folio no es Pick-Up", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.limpiar();
                }
            }
            else
            {
                int num = (int)MessageBox.Show("Folio no Existente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.limpiar();
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Return || string.IsNullOrWhiteSpace(textBox1.Text))
            {
                return;
            }

            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Favor de seleccionar Transacción", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            obtenerPickup();
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Return || comboBox1.SelectedIndex == -1)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Favor de ingresar Folio", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            obtenerPickup();
        }

        public bool validarEliminar(DataTable dtppe, DataTable dtVM)
        {
            foreach (DataRow row in (InternalDataCollectionBase)dtVM.Rows)
            {
                string filterExpression = $"cod_prod ={row.ItemArray[0].ToString()} and cantidad >={row.ItemArray[2].ToString()}";
                if (dtppe.Select(filterExpression).Length == 0)
                    return false;
            }
            return true;
        }
        public bool Eliminar(DataTable dtppe, DataTable dtVM)
        {
            foreach (DataRow row in (InternalDataCollectionBase)dtVM.Rows)
            {
                string filterExpression = $"cod_prod ={row.ItemArray[0].ToString()} and cantidad >={row.ItemArray[2].ToString()}";
                DataRow[] dataRowArray = dtppe.Select(filterExpression);
                Decimal num1 = Convert.ToDecimal(dataRowArray[0].ItemArray[4].ToString());
                Decimal num2 = Convert.ToDecimal(row.ItemArray[2].ToString());
                if (num1 - num2 > 0M)
                    this.Q.Consultar($"Update Productos_por_embarcar set cantidad={(num1 - num2).ToString()} where Cod_prod='{row.ItemArray[0].ToString()}' and folio='{dataRowArray[0].ItemArray[0].ToString()}'  and transaccion='{dataRowArray[0].ItemArray[1].ToString()}'", this.cod_estab);
                else
                    this.Q.Consultar($"delete  Productos_por_embarcar   where cod_prod='{row.ItemArray[0].ToString()}' and folio='{dataRowArray[0].ItemArray[0].ToString()}' and transaccion='{dataRowArray[0].ItemArray[1].ToString()}'", this.cod_estab);
            }
            this.Q.Consultar($"update  facremtick set tipo_atencion='A'    where folio='{dtppe.Rows[0].ItemArray[0].ToString().Trim()}' and transaccion='{dtppe.Rows[0].ItemArray[1].ToString().Trim()}'", this.cod_estab);
            this.Q.Consultar($"insert into historico_cambios(codigo,transaccion,fecha,dato,valor_anterior,valor_nuevo,usuario) values('{dtppe.Rows[0].ItemArray[0].ToString().Trim()}', '{dtppe.Rows[0].ItemArray[1].ToString().Trim()}', '{DateTime.Now.ToString("yyyy/MM/dd HH:mm")}', 'PickUP', '{this.TA}', 'A', '{this.usuario}')", this.cod_estab);
            return true;
        }

        public void limpiar()
        {
            this.textBox1.Text = "";
            this.textBox1.Text = "";
            this.button1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea eliminar el pickup del documento seleccionado?", "Atención", MessageBoxButtons.YesNo) != DialogResult.Yes || !this.Eliminar(this.DTPPE, this.DTEM))
                return;
            int num = (int)MessageBox.Show("Cambio Efectuado correctamente", "OK", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            this.limpiar();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

}
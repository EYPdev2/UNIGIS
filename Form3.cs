using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
 
using System.Globalization;
 using System.CodeDom;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.VisualBasic;
using System.Security.Cryptography;

namespace ActualizadorDoctosUnigis
{
    public partial class frm_Login : Form
    {
        Qrys q = new Qrys();
        public frm_Login()
        {
            InitializeComponent();
        }

        private void txt_login_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txt_login_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    txt_usuarioN.Text = q.Empleado(txt_login.Text);
                    txt_pass.Enabled = true;
                    txt_pass.Focus();
                }
                catch (Exception ex)
                { MessageBox.Show("El Usuario no Existe"); }

            }



        }

        private void txt_pass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                guardar();
            }
        }

        public string Encriptado(String c) {

            string str;
            c = c.Trim();
            if (c != "") {

                str = "";
                String str1 = "[]{}#%&/()";
                Encoding enc = Encoding.GetEncoding(1252);
                byte[] myByte = new byte[c.Length];
                int num = c.Length - 1;
                for (int i = 0; i <= num; i++) {
                    //  MessageBox.Show(((int)Convert.ToChar(str1.Substring(i, 1))).ToString() +" +  +"+ ((int)Convert.ToChar(c.Substring(i, 1))).ToString());
                    int num1 = (int)Convert.ToChar(str1.Substring(i, 1)) + (int)Convert.ToChar(c.Substring(i, 1)) + i;
                    //    MessageBox.Show(((char)num1).ToString());
                    if (num1 > 255)
                    {
                        num1 = num1 - 255;
                    }
                    char x = Convert.ToChar(num1);

                    string ch = string.Join("", x);
                    string str2 = ToLiteral(Convert.ToChar(num1).ToString());
                    if (str2.Trim().ToString() != "" && i == c.Length) {
                        str2 = "%";
                    }
                    myByte[i] = Convert.ToByte(num1);
                    str = enc.GetString(myByte);
                    //¿Ãóå“™œ›¢ 
                }

            }

            else {
                str = c;

            }
            return str;
        }
        public void guardar()
        {
            if (Encriptado(txt_pass.Text).ToString() == q.CEmpleado(txt_login.Text).ToString().Trim())
            {
                Main frm_main = new Main();
                frm_main.usuario = txt_login.Text;
                frm_main.nombreU = txt_usuarioN.Text;
                frm_main.nivel = q.NEmpleado(txt_login.Text);
                frm_main.Show();
                this.Hide();
            }
            else
            {

                MessageBox.Show("Contraseña Incorrecta", "Verificar Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            guardar();
        }

        private void txt_pass_TextChanged(object sender, EventArgs e)
        {

        }
        public void permisosVentana(string nivel)
        {

        }

        private static string ToLiteral(string valueTextForCompiler)
        {
     
            return Microsoft.CodeAnalysis.CSharp.SymbolDisplay.FormatLiteral(valueTextForCompiler, false);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(LicBMSCadena("Q+w8IOv+MAZVXl/KDZUFWIdOfWVTDNm1lagkEopPH0PVk1VYbYc+TGq55FDHExYuzsNPQIZ1c7qbsS3eSBUuDg==", 1));
            // MessageBox.Show(Invertir_Encriptado("Q+w8IOv+MAZVXl/KDZUFWIdOfWVTDNm1lagkEopPH0PVk1VYbYc+TGq55FDHExYuzsNPQIZ1c7qbsS3eSBUuDg=="));
            //MessageBox.Show( Conversions.ToString(Strings.Chr(checked(checked(Strings.Asc("Q") - 17) - 0))));
            string Cadena = "gRI1CAgT0i1SgtXCu1twD8pU0E3ld4I6JxChBCqKa3UYrZAA7ErbzJCKDcc5MjXFASCa4opZ/gzYAokRFe7E3w==";
            //9455702761300614
            //1067437747231353
            //abcdefghijklmnopqrstuvwxyz

            string x = Decrypt(Cadena, "9455702761300614");
                MessageBox.Show(Decrypt(Cadena, "9455702761300614"));
            // LicBMSInvEncriptado( "1067437747231353", "Q+w8IOv+MAZVXl/KDZUFWIdOfWVTDNm1lagkEopPH0PVk1VYbYc+TGq55FDHExYuzsNPQIZ1c7qbsS3eSBUuDg==", 1);
            string y = Encrypt("Empresa:1;Fecha:31/12/2078;Equipos:120;Usuarios:999", "2307511253762772");

        }
    
        public   string Decrypt(string CadenaEncript, string Identificador)
        {
            string str;
            try
            {
                string str1 = "bsy*020620*mb9";
                string str2 = "SHA1";
                int num = 2;
                string str3 = "@1B2c3D4e5F6g7H8";
                int num1 = 256;
                byte[] bytes = Encoding.ASCII.GetBytes(str3);
                byte[] numArray = Encoding.ASCII.GetBytes(str1);
                byte[] numArray1 = Convert.FromBase64String(CadenaEncript);
                 
                byte[] bytes1 = (new PasswordDeriveBytes(Identificador, numArray, str2, num)).GetBytes(num1 / 8);
                ICryptoTransform cryptoTransform = (new RijndaelManaged()
                {
                    Padding = PaddingMode.PKCS7,
                    Mode = CipherMode.CBC
                }).CreateDecryptor(bytes1, bytes);
                MemoryStream memoryStream = new MemoryStream(numArray1);
                //MessageBox.Show(Encoding.UTF8.GetString(numArray1,0,64));
                CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Read);
             
                byte[] numArray2 = new byte[checked(checked((int)numArray1.Length - 1) + 1)];
                int num2 = cryptoStream.Read(numArray2, 0, (int)numArray2.Length);
                memoryStream.Close();
               // cryptoStream.Close();
               str = Encoding.UTF8.GetString(numArray2, 0, num2);
            }
            catch (Exception exception)
            {
                ProjectData.SetProjectError(exception);
                str = "";
                ProjectData.ClearProjectError();
            }
            return str;
        }
        public   string Encrypt(string Cadena, string Identificador)
        {
            string base64String;
            try
            {
                string str = "bsy*020620*mb9";
                string str1 = "SHA1";
                int num = 2;
                string str2 = "@1B2c3D4e5F6g7H8";
                int num1 = 256;
                byte[] bytes = Encoding.ASCII.GetBytes(str2);
                byte[] numArray = Encoding.ASCII.GetBytes(str);
                byte[] bytes1 = Encoding.UTF8.GetBytes(Cadena);
                byte[] numArray1 = (new PasswordDeriveBytes(Identificador, numArray, str1, num)).GetBytes(num1 / 8);
                ICryptoTransform cryptoTransform = (new RijndaelManaged()
                {
                    Padding = PaddingMode.PKCS7,
                    Mode = CipherMode.CBC
                }).CreateEncryptor(numArray1, bytes);
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write);
                cryptoStream.Write(bytes1, 0, (int)bytes1.Length);
                cryptoStream.FlushFinalBlock();
                byte[] array = memoryStream.ToArray();
                memoryStream.Close();
                cryptoStream.Close();
                base64String = Convert.ToBase64String(array);
            }
            catch (Exception exception)
            {
                ProjectData.SetProjectError(exception);
                base64String = "";
                ProjectData.ClearProjectError();
            }
            return base64String;
        }

        private void frm_Login_Load(object sender, EventArgs e)
        {

        }
    }
}

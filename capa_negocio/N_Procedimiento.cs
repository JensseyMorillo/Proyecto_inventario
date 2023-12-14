using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data;

namespace capa_negocio
{
    public class N_Procedimiento
    {
        SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conectar"].ConnectionString);

        SqlDataReader Dr;
      
        public void formatoMoneda(TextBox xTBox)
        {
            if (xTBox.Text == string.Empty)
            {
                return;
            }
            else
            {
                decimal Monto;
                Monto = Convert.ToDecimal(xTBox.Text);
                xTBox.Text = Monto.ToString("N2");
            }
        }

        public string GenerarCodigo(string Tabla)
        {
            string Codigo = string.Empty;
            

            SqlCommand cmd = new SqlCommand("Select Count(*) as TotalRegistros From "+ Tabla, conexion);
            cmd.CommandType = CommandType.Text;
            conexion.Open();
            Total1 = 0;
            Dr = cmd.ExecuteReader();

            if (Dr.Read())
            {
                Total1 = Convert.ToInt32(Dr["TotalRegistros"]) + 1;
            }

            Dr.Close();

            if(Total1 < 10)
            {
                Codigo = "0000000" + Total1;
            }
            else if (Total1 < 100)
            {
                Codigo = "000000" + Total1;
            }
            else if(Total1 < 100000)
            {
                Codigo = "00000" + Total1;
            }
            else if(Total1 < 100000)
            {
                Codigo = "0000" + Total1;
            }
            else if(Total1 < 1000000)
            {
                Codigo = "000" + Total1;
            }
            else if (Total1 < 1000000)
            {
                Codigo = "00" + Total1;
            }
            else if (Total1 < 10000000)
            {
                Codigo = "0" + Total1;
            }

            conexion.Close();
            return Codigo;
        }

        private int Total1 = 0;
        public string GenerarCodigoId(string Tabla)
        {

            int total = 0;

            SqlCommand cmd = new SqlCommand("Select Count(*) as TotalRegistros From " + Tabla, conexion);
            cmd.CommandType = CommandType.Text;
            conexion.Open();

            Dr = cmd.ExecuteReader();

            if (Dr.Read())
            {
                total = Convert.ToInt32(Dr["TotalRegistros"]) + 1;
            }

            Dr.Close();

            conexion.Close();
            return Convert.ToString(total);
           
        }

        public void limpiarControles(Form xform)
        {
            foreach(var xCtrl in xform.Controls)
            {
                if(xCtrl is TextBox)
                {
                    ((TextBox)xCtrl).Text = string.Empty;
                }
                else if(xCtrl is ComboBox)
                {
                    ((ComboBox)xCtrl).Text = string.Empty;
                }
            }
        }
    }
}

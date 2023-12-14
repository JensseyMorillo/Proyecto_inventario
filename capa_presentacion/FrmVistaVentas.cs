using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using capa_negocio;
namespace capa_presentacion
{
    public partial class FrmVistaVentas : Form
    {
        public FrmVistaVentas()
        {
            InitializeComponent();
        }

        N_Ventas ventas = new N_Ventas();
        N_inventario inve = new N_inventario();

        private void Mostrar_producto(string buscar)
        {
            tablaInvetario.DataSource = ventas.MostrarDatosVentas(buscar);
        }
        private void Mostrar_producto2(string buscar)
        {
            tablaInvetario.DataSource = inve.datosInventario(buscar);
        }

        private void FrmVistaVentas_Load(object sender, EventArgs e)
        {
           
           
            tablaInvetario.ClearSelection();
            Mostrar_producto("");
            Mostrar_producto2("");
        }

        private void tablaInvetario_DoubleClick(object sender, EventArgs e)
        {
            if (tablaInvetario.Rows.Count == 0)
            {
                return;
            }
            else
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }

        private void txtbuscar_TextChanged(object sender, EventArgs e)
        {
            if (comboelegir.Text == "CODIGO")
            {
                Mostrar_producto(txtbuscar.Text);
            }
            if (comboelegir.Text == "DESCRIPCION")
            {
                Mostrar_producto2(txtbuscar.Text);
            }

        }
    }
}

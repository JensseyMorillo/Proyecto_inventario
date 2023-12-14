using capa_entidad;
using capa_negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace capa_presentacion
{
    public partial class FrmVistaCliente : Form
    {
        public FrmVistaCliente()
        {
            InitializeComponent();
        }

        E_cliente objEntidad = new E_cliente();
        N_cliente objNegocio = new N_cliente();

        public void mostrartabla(string buscar)
        {
            tablaClientes.DataSource = objNegocio.datosCliente(buscar);
        }
        public void mostrartabla2(string buscar)
        {
            tablaClientes.DataSource = objNegocio.datosCliente2(buscar);
        }

        public void acciontabla()
        {
            tablaClientes.Columns[0].Visible = false;
            tablaClientes.Columns[10].Visible = false;
            tablaClientes.ClearSelection();
        }

        private void FrmVistaCliente_Load(object sender, EventArgs e)
        {
            mostrartabla("");
            mostrartabla2("");
            acciontabla();
        }

       

        private void tablaClientes_DoubleClick(object sender, EventArgs e)
        {
            if (tablaClientes.Rows.Count == 0)
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
            if (comboelegir.Text == "Apellido")
            {
                mostrartabla2(txtbuscar.Text);
            }
            if (comboelegir.Text == "Nombre")
            {
                mostrartabla(txtbuscar.Text);
            }
        }
    }
    
}

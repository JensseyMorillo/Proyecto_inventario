using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using capa_entidad;
using capa_negocio;

namespace capa_presentacion
{
    public partial class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esta Seguro que Desea Salir?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                this.Close();
               
            }

        }

        private Form formactivar = null;

        public void abrirFormulario(Form formhijo)
        {
            if (formactivar != null)
                formactivar.Close();
            formactivar = formhijo;
            formhijo.TopLevel = false;
            formhijo.Dock = DockStyle.Fill;
            wrapper.Controls.Add(formhijo);
            wrapper.Tag = formhijo;
            formhijo.BringToFront();
            formhijo.Show();
        }

       
        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            abrirFormulario(new FormInvent());
            labelTema.Text = "INVENTARIO";

        }

        private void btnFacturacion_Click(object sender, EventArgs e)
        {
            abrirFormulario(new FormFacturacion());
            labelTema.Text = "FACTURACION";
        }

        private void btnRegistro_Click(object sender, EventArgs e)
        {
            abrirFormulario(new FormRegistro());
            labelTema.Text = "REGISTRO";
        }

        private void btnCliente_Click(object sender, EventArgs e)
        {
            abrirFormulario(new FormCliente());
            labelTema.Text = "CLIENTES";
        }

      

        private void VerUsuarioData()
        {
            
            lblusuario.Text = UserLoginCache.cargo;
        }
        private void btnBorrar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            
            VerUsuarioData();
        }
    }
}

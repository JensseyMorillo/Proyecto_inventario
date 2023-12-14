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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        N_usuario objdatos = new N_usuario();

        private void button1_Click(object sender, EventArgs e)
        {
            if(txtuser.Text != "USUARIO")
            {
                if(txtPass.Text != "CONTRASEÑA")
                {
                    var validlogin = objdatos.selectlogin(txtuser.Text, txtPass.Text);
                    if (validlogin == true)
                    {
                        FormPrincipal menu = new FormPrincipal();
                        menu.Show();
                        menu.FormClosed += logout;
                        this.Hide();
                    }
                    else
                        MessageBox.Show("Contraseña o usuario incorrecto.");
                }
            }

        }

        private void logout (object sender, FormClosedEventArgs e)
        {
            txtPass.Clear();
            txtuser.Clear();
            this.Show();
            txtuser.Focus();
        }

        private void salir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void minimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void txtuser_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtPass.Focus();
                e.Handled = true;
            }
        }

        private void txtPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                button1.Focus();
                e.Handled = true;
            }
        }

        private void button1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
    }
}

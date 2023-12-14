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
    public partial class FormCliente : Form
    {
        public FormCliente()
        {
            InitializeComponent();
        }

        E_cliente objEntidad = new E_cliente();
        N_cliente objNegocio = new N_cliente();
        DateTime hoy = DateTime.Now;
        public bool editarse = false;
        public string IDcliente;

        private void btnguardar_Click(object sender, EventArgs e)
        {
            if (validarcampos())
            {
                if (editarse == false)
                {
                    try
                    {
                        objEntidad.Nombre = txtNombre.Text.Trim();
                        objEntidad.Apellido = txtApellido.Text.Trim();
                        objEntidad.Cedula = txtCedula.Text.Trim();
                        objEntidad.Telefono = txtTelefono.Text.Trim();
                        objEntidad.Carro = txtCarro.Text.Trim();
                        objEntidad.Modelo = txtModelo.Text.Trim();
                        objEntidad.Año = txtaño.Text.Trim();
                        objEntidad.Placa = txtPlaca.Text.Trim();
                        objEntidad.Descripcion = txtDescrip.Text.Trim();
                        objEntidad.Fecha = Convert.ToDateTime(txtFecha.Text = hoy.ToString("yyyy-MM-dd"));
                        objEntidad.Estado = "exciste";

                        objNegocio.insertarCliente(objEntidad);
                        MessageBox.Show("se han guardado los datos correctamente.");
                        mostrartabla("");
                        limpiarcampos();



                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("no se ha guardado los datos correctamente" + ex);
                    }
                }
                else if (editarse == true)
                {
                    try
                    {
                        objEntidad.IdCliente = Convert.ToInt32(IDcliente);
                        objEntidad.Nombre = txtNombre.Text.Trim();
                        objEntidad.Apellido = txtApellido.Text.Trim();
                        objEntidad.Cedula = txtCedula.Text.Trim();
                        objEntidad.Telefono = txtTelefono.Text.Trim();
                        objEntidad.Carro = txtCarro.Text.Trim();
                        objEntidad.Modelo = txtModelo.Text.Trim();
                        objEntidad.Año = txtaño.Text.Trim();
                        objEntidad.Placa = txtPlaca.Text.Trim();
                        objEntidad.Descripcion = txtDescrip.Text.Trim();
                        objEntidad.Fecha = Convert.ToDateTime(txtFecha.Text = hoy.ToString("yyyy-MM-dd"));
                        objEntidad.Estado = "exciste";
                        objNegocio.editarCliente(objEntidad);
                        MessageBox.Show("se han editado los datos correctamente.");
                        mostrartabla("");
                        editarse = false;
                        limpiarcampos();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("no se ha editado los datos correctamente" + ex);
                    }
                }

            }

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if(tablaClientes.SelectedRows.Count > 0)
            {
                editarse = true;
                IDcliente = tablaClientes.CurrentRow.Cells[0].Value.ToString();
                txtNombre.Text = tablaClientes.CurrentRow.Cells[1].Value.ToString();
                txtApellido.Text = tablaClientes.CurrentRow.Cells[2].Value.ToString();
                txtCedula.Text = tablaClientes.CurrentRow.Cells[3].Value.ToString();
                txtTelefono.Text = tablaClientes.CurrentRow.Cells[4].Value.ToString();
                txtCarro.Text = tablaClientes.CurrentRow.Cells[5].Value.ToString();
                txtModelo.Text = tablaClientes.CurrentRow.Cells[6].Value.ToString();
                txtaño.Text = tablaClientes.CurrentRow.Cells[7].Value.ToString();
                txtPlaca.Text = tablaClientes.CurrentRow.Cells[8].Value.ToString();
                txtDescrip.Text = tablaClientes.CurrentRow.Cells[9].Value.ToString();
                mostrartabla("");

            }
            else
            {
                MessageBox.Show("Seleccione la informacion que desea editar.");
            }

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if(tablaClientes.SelectedRows.Count > 0)
            {
                if(MessageBox.Show("Estas Seguro que Deseas Eliminar estos datos", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    objEntidad.IdCliente = Convert.ToInt32(tablaClientes.CurrentRow.Cells[0].Value.ToString());
                    objEntidad.Estado = "Noexciste";
                    objNegocio.eliminarCliente(objEntidad);
                    mostrartabla("");
                }

               
            }

        }

        public void acciontabla() 
        {
            tablaClientes.Columns[0].Visible = false;
            tablaClientes.Columns[10].Visible = false;
            tablaClientes.ClearSelection();
        }

        public void mostrartabla(string buscar)
        {
            tablaClientes.DataSource = objNegocio.datosCliente(buscar);
        }

        private bool validarcampos()
        {
            bool Ok = true;
            if(txtNombre.Text == "")
            {
                Ok = false;
                errorProvider1.SetError(txtNombre, "Ingrese un nombre.");
            }
            if (txtApellido.Text == "")
            {
                Ok = false;
                errorProvider1.SetError(txtApellido, "Ingrese un apellido.");
            }
            if (txtCedula.Text == "")
            {
                Ok = false;
                errorProvider1.SetError(txtCedula, "Ingrese la cedula o RNC.");
            }
            if (txtTelefono.Text == "")
            {
                Ok = false;
                errorProvider1.SetError(txtTelefono, "Ingrese un telefono.");
            }
            if (txtCarro.Text == "")
            {
                Ok = false;
                errorProvider1.SetError(txtCarro, "Ingrese un carro.");
            }
            if (txtModelo.Text == "")
            {
                Ok = false;
                errorProvider1.SetError(txtModelo, "Ingrese un modelo.");
            }
            if (txtaño.Text == "")
            {
                Ok = false;
                errorProvider1.SetError(txtaño, "Ingrese un año.");
            }
            if (txtPlaca.Text == "")
            {
                Ok = false;
                errorProvider1.SetError(txtPlaca, "Ingrese una placa.");
            }
            if (txtDescrip.Text == "")
            {
                Ok = false;
                errorProvider1.SetError(txtDescrip, "Ingrese una Descripcion.");
            }
            return Ok;
        }

        public void limpiarcampos()
        {
            editarse = false;
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtCedula.Text = "";
            txtTelefono.Text = "";
            txtCarro.Text = "";
            txtModelo.Text = "";
            txtaño.Text = "";
            txtPlaca.Text = "";
            txtDescrip.Text = "";
            txtNombre.Focus();
        }

        private void FormCliente_Load(object sender, EventArgs e)
        {
            txtFecha.Text = hoy.ToString("yyyy-MM-dd");
            txtPlaca.Text.ToUpper();
            mostrartabla("");
            acciontabla();

        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            mostrartabla(txtBuscar.Text);
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            limpiarcampos();
        }

       

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtApellido.Focus();
                e.Handled = true;
            }
        }

        private void txtApellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtCedula.Focus();
                e.Handled = true;
            }
        }

        private void txtCedula_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtTelefono.Focus();
                e.Handled = true;
            }
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtCarro.Focus();
                e.Handled = true;
            }
        }

        private void txtCarro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtModelo.Focus();
                e.Handled = true;
            }
        }

        private void txtModelo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtaño.Focus();
                e.Handled = true;
            }
        }

        private void txtaño_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtPlaca.Focus();
                e.Handled = true;
            }
        }


        private void txtPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtDescrip.Focus();
                e.Handled = true;
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Quieres Convertir esta tabla a un Excel?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                ExportarDatos(tablaClientes);

            }
        }

        public void ExportarDatos(DataGridView dataListado)
        {
            Microsoft.Office.Interop.Excel.Application exportaexcel = new Microsoft.Office.Interop.Excel.Application();
            exportaexcel.Application.Workbooks.Add(true);

            int indicecolums = 0;

            foreach (DataGridViewColumn column in dataListado.Columns)
            {
                indicecolums++;

                exportaexcel.Cells[1, indicecolums] = column.Name;
            }

            int indicefilas = 0;
            foreach (DataGridViewRow fila in dataListado.Rows)
            {
                indicefilas++;
                indicecolums = 0;
                foreach (DataGridViewColumn column in dataListado.Columns)
                {
                    indicecolums++;
                    exportaexcel.Cells[indicefilas + 1, indicecolums] = fila.Cells[column.Name].Value;
                }
            }

            exportaexcel.Visible = true;
        }
    }
}

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
    public partial class FormInvent : Form
    {
        public FormInvent()
        {
            InitializeComponent();
        }

        N_Procedimiento procedimiento = new N_Procedimiento();
        E_inventario objEntidad = new E_inventario();
        N_inventario objNegocio = new N_inventario();
        public bool editarse = false;
        public string IDInventario;

        public void acciontabla()
        {
          
            tablaInventario.Columns[5].Visible = false;
            tablaInventario.Columns[0].Visible = false;
            tablaInventario.ClearSelection();
        }

        private bool validacion()
        {

            bool ok = true;

            if (UserLoginCache.cargo == cargo.Usuario)
            {
                bunifuImageButton1.Enabled = false;
                btnBorrar.Enabled = false;
                btnEditar.Enabled = false;
                btnGuardar.Enabled = false;
                btnExcel.Enabled = false;
            }
            return ok;
        }

        public void mostrartabla(string buscar)
        {
            tablaInventario.DataSource = objNegocio.datosInventario(buscar);
        }
        public void mostrartabla2(string buscar)
        {
            tablaInventario.DataSource = objNegocio.datosInventarioCodigo(buscar);
        }

        public void limpiarcampos()
        {
            editarse = false;
            txtCodigo.Text = "";
            txtDescrip.Text = "";
            txtExiste.Text = "";
            txtPrecio.Text = "";
            txtCodigo.Focus();
        }

        private void FormInvent_Load(object sender, EventArgs e)
        {
            validacion();
            mostrartabla("");
            mostrartabla2("");
            acciontabla();
           
            txtCodigo.Text.ToUpper();
            txtPrecio.Text = "0.00";
            txtExiste.Text = "0";
        }

        private bool validarcampos()
        {
            bool Ok = true;
            if (txtCodigo.Text == "")
            {
                Ok = false;
                errorProvider1.SetError(txtCodigo, "Ingrese el codigo.");
            }
            if (txtDescrip.Text == "")
            {
                Ok = false;
                errorProvider1.SetError(txtDescrip, "Ingrese un producto.");
            }
            if (txtPrecio.Text == "")
            {
                Ok = false;
                errorProvider1.SetError(txtPrecio, "Ingrese el precio.");
            }
            if (txtExiste.Text == "")
            {
                Ok = false;
                errorProvider1.SetError(txtExiste, "Ingrese una cantidad.");
            }
           
            
            return Ok;
        }

        private void txtExiste_KeyPress(object sender, KeyPressEventArgs e)
        {
           
            if (!char.IsNumber(e.KeyChar) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
            }
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtExiste.Focus();
                e.Handled = true;
            }

            if (!char.IsNumber(e.KeyChar) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validarcampos())
            {
                if(editarse == false)
                {
                    try 
                    {
                        objEntidad.Codigo = txtCodigo.Text.Trim();
                        objEntidad.Descripcion = txtDescrip.Text.Trim();
                        objEntidad.Precio = Convert.ToDecimal(txtPrecio.Text.Trim());
                        objEntidad.Excistencia = Convert.ToInt32(txtExiste.Text.Trim());
                        objEntidad.Estado = "exciste";
                        objNegocio.insertarProductos(objEntidad);
                        MessageBox.Show("Se han guardado los datos correctamente.");
                        mostrartabla("");
                        mostrartabla2("");
                        limpiarcampos();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("no se ha guardado los datos correctamente" + ex);
                    }

                }else if(editarse == true)
                {
                    try
                    {
                        objEntidad.ID_inventario = Convert.ToInt32(IDInventario);
                        objEntidad.Codigo = txtCodigo.Text.Trim();
                        objEntidad.Descripcion = txtDescrip.Text.Trim();
                        objEntidad.Precio = Convert.ToDecimal(txtPrecio.Text.Trim());
                        objEntidad.Excistencia = Convert.ToInt32(txtExiste.Text.Trim());
                        objEntidad.Estado = "exciste";
                        objNegocio.EditarInventario(objEntidad);
                        MessageBox.Show("Se han Editado los datos correctamente.");
                        mostrartabla("");
                        mostrartabla2("");
                        limpiarcampos();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("no se ha editado los datos correctamente" + ex);
                    }
                }

            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if(tablaInventario.SelectedRows.Count > 0)
            {
                editarse = true;
                IDInventario = tablaInventario.CurrentRow.Cells[0].Value.ToString();
                txtCodigo.Text = tablaInventario.CurrentRow.Cells[1].Value.ToString();
                txtDescrip.Text = tablaInventario.CurrentRow.Cells[2].Value.ToString();
                txtPrecio.Text = tablaInventario.CurrentRow.Cells[3].Value.ToString();
                txtExiste.Text = tablaInventario.CurrentRow.Cells[4].Value.ToString();
                mostrartabla("");
                mostrartabla2("");
            }
            else
            {
                MessageBox.Show("Seleccione la informacion que desea editar.");

            }
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if(tablaInventario.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("Estas Seguro que Deseas Eliminar estos datos", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    objEntidad.ID_inventario = Convert.ToInt32(tablaInventario.CurrentRow.Cells[0].Value.ToString());
                    objEntidad.Estado = "Noexciste";
                    objNegocio.EliminarInventario(objEntidad);
                    mostrartabla("");
                    mostrartabla2("");
                }
            }
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            limpiarcampos();
        }

        private void txtPrecio_Leave(object sender, EventArgs e)
        {
            procedimiento.formatoMoneda(txtPrecio);
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtDescrip.Focus();
                e.Handled = true;
            }
        }

        private void txtDescrip_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtPrecio.Focus();
                e.Handled = true;
            }
        }
       
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if(comboelegir.Text == "CODIGO")
            {
                mostrartabla2(txtBuscar.Text);
            }
            if(comboelegir.Text == "DESCRIPCION")
            {
                mostrartabla(txtBuscar.Text);
            }
            
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Quieres Convertir esta tabla a un Excel?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                ExportarDatos(tablaInventario);

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

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
using capa_entidad;

namespace capa_presentacion
{
    public partial class FormRegistro : Form
    {
        public FormRegistro()
        {
            InitializeComponent();
        }

        N_Procedimiento Procedimiento = new N_Procedimiento();
        N_Detalle_venta detalle = new N_Detalle_venta();
        E_DetalleVentas Detalle = new E_DetalleVentas();
        N_Ventas ventas = new N_Ventas();
        E_Ventas Venta = new E_Ventas();
       
        public delegate void UpdateDelegate(object sender, UpdateEventArgs args);
        public event UpdateDelegate UpdateEventHandler;

        public class UpdateEventArgs : EventArgs
        {
            public string Data { get; set; }
        }


        private void FormRegistro_Load(object sender, EventArgs e)
        {
            
            //cargarDatos("");
            cargardatos2("");
            //tablaRegistro.Columns[6].DefaultCellStyle.Format = "#,##0.00";
            //tablaRegistro.Columns[7].DefaultCellStyle.Format = "#,##0.00";
            //tablaRegistro.Columns[9].DefaultCellStyle.Format = "#,##0.00";

            validacion();
        }

        private void AnVen_UpdateEventHandler(object sender, VistaFacturacion.UpdateEventArgs args)
        {
            //cargarDatos("");
            cargardatos2("");
        }

        //private void cargarDatos(string buscar)
        //{
        //    tablaDetalle.DataSource = ventas.MostrarIngresoVentas(buscar);
        //    tablaDetalle.ClearSelection();
        //    tablaDetalle.Columns[0].Visible = false;
        //    tablaDetalle.Columns[1].Visible = false;
        //    tablaDetalle.Columns[8].Visible = false;
        //}

        private void cargardatos2(string buscar)
        {
            tablaRegistro.DataSource = detalle.MostrarDetallesVentaCO(buscar);
            tablaRegistro.Columns[0].Visible = false;
            tablaRegistro.Columns[1].Visible = false;
            //tablaRegistro.Columns[8].Visible = false;

        }



        private void txtbuscar_TextChanged(object sender, EventArgs e)
        {
            
          
                cargardatos2(txtbuscar.Text);
               

            
            
        }

        private void comboelegir_TextChanged(object sender, EventArgs e)
        {
          
           
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

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Quieres Convertir esta tabla a un Excel?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                 ExportarDatos(tablaRegistro);

            }
           
        }

        private void tablaRegistro_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
                txtBuscar2.Text = tablaRegistro.CurrentRow.Cells[2].Value.ToString();

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if(tablaRegistro.Rows.Count == 0)
            {
                MessageBox.Show("no hay comprar para anular.", "Anular Ventas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                if(tablaRegistro.CurrentRow == null)
                {
                    MessageBox.Show("Debe seleccionar una compra para anular.", "Anular Ventas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else
                {
                    VistaFacturacion AnularVentas = new VistaFacturacion();
                    AnularVentas.UpdateEventHandler += AnVen_UpdateEventHandler;
                    AnularVentas.txtIdVenta.Text = tablaRegistro.SelectedRows[0].Cells[0].Value.ToString();
                    AnularVentas.Id_cliente.Text = tablaRegistro.SelectedRows[0].Cells[1].Value.ToString();
                    AnularVentas.txtNoFactura.Text = tablaRegistro.SelectedRows[0].Cells[2].Value.ToString();
                    AnularVentas.txtNombreC.Text = tablaRegistro.SelectedRows[0].Cells[3].Value.ToString();
                    AnularVentas.txtCedula.Text = tablaRegistro.SelectedRows[0].Cells[4].Value.ToString();
                    AnularVentas.txtfecha.Text = tablaRegistro.SelectedRows[0].Cells[5].Value.ToString();
                    AnularVentas.txtSubT.Text = tablaRegistro.SelectedRows[0].Cells[6].Value.ToString();
                    AnularVentas.txtImpuesto2.Text = tablaRegistro.SelectedRows[0].Cells[7].Value.ToString();
                    AnularVentas.txtTotal.Text = tablaRegistro.SelectedRows[0].Cells[8].Value.ToString();
                    AnularVentas.CboTipoVentas.Text = tablaRegistro.SelectedRows[0].Cells[10].Value.ToString();
                    AnularVentas.ShowDialog();


                }
            }
        }

        private bool validacion()
        {

            bool ok = true;

            if (UserLoginCache.cargo == cargo.Usuario)
            {
                btnCancelar.Enabled = false;
                btnEliminarDet.Enabled = false;
            }
            return ok;
        }
        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
                txtbuscar.Text="";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (tablaRegistro.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("Estas Seguro que Deseas Eliminar estos datos", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    Venta.Id_facturacion = Convert.ToInt32(tablaRegistro.CurrentRow.Cells[0].Value.ToString());
                    Venta.Estado = "Nulo";
                    ventas.eliminarFactura2(Venta);
                    cargardatos2("");
                }


            }
            else
            {
                MessageBox.Show("Seleccione la tabla que desee eliminar");
            }
        }

       

        private void txtBuscar2_TextChanged(object sender, EventArgs e)
        {
            
                //cargarDatos(txtBuscar2.Text);
            
        }
        private void btnEliminarDet_Click(object sender, EventArgs e)

        {
            if (tablaDetalle.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("Estas Seguro que Deseas Eliminar estos datos", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    Detalle.Id_facturacion = Convert.ToInt32(tablaDetalle.CurrentRow.Cells[0].Value.ToString());
                    Detalle.Id_Inventario = Convert.ToInt32(tablaDetalle.CurrentRow.Cells[1].Value.ToString());
                    Detalle.Cantidad = Convert.ToInt32(tablaDetalle.CurrentRow.Cells[3].Value.ToString());
                    Detalle.Estado = "Nulo";
                    detalle.eliminarDetalleFactura2(Detalle);
                    //cargarDatos("");
                }


            }
            else
            {
                MessageBox.Show("Seleccione la tabla que desee eliminar");
            }
        }

        private void btnExcel2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Quieres Convertir esta tabla a un Excel?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                ExportarDatos(tablaDetalle);

            }
        }
    }
}

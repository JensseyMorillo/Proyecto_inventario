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
    public partial class VistaFacturacion : Form
    {
        public VistaFacturacion()
        {
            InitializeComponent();
        }

        DateTime hoy = DateTime.Now;
        N_Procedimiento procedimiento = new N_Procedimiento();
        N_Ventas ventas = new N_Ventas();
        E_Ventas venta = new E_Ventas();
        N_Detalle_venta DetalleVentas = new N_Detalle_venta();
        E_DetalleVentas detalleVenta = new E_DetalleVentas();
        public bool existe1 = false;
        public string IdVenta;


        public delegate void UpdateDelegate(object sender, UpdateEventArgs args);
        public event UpdateDelegate UpdateEventHandler;

        public class UpdateEventArgs : EventArgs
        {
            public string Data { get; set; }
        }

        protected void Anular()
        {
            UpdateEventArgs args = new UpdateEventArgs();
            UpdateEventHandler.Invoke(this,args);

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void VistaFacturacion_Load(object sender, EventArgs e)
        {
            procedimiento.formatoMoneda(txtSubT);
            procedimiento.formatoMoneda(txtImpuesto2);
            procedimiento.formatoMoneda(txtTotal);
            cargardatos3();
            mejorarVista();
        }

        private void cargardatos3()
        {
            tablaFacturar.DataSource = DetalleVentas.MostrarDetallesVentaCO2(Convert.ToInt32(txtIdVenta.Text));
            tablaFacturar.ClearSelection();
        }
        private void mejorarVista()
        {
            tablaFacturar.Columns[0].Visible = false;
            tablaFacturar.Columns[1].Visible = false;
            tablaFacturar.Columns[2].Width = 150;
            tablaFacturar.Columns[3].Width = 150;
            tablaFacturar.Columns[4].Visible = false;
            tablaFacturar.Columns[5].Width = 150;
            tablaFacturar.Columns[6].Width = 150;
            tablaFacturar.Columns[7].Width = 150;

            tablaFacturar.Columns[5].DefaultCellStyle.Format = "#,##0.00";
            tablaFacturar.Columns[6].DefaultCellStyle.Format = "#,##0.00";
            tablaFacturar.Columns[7].DefaultCellStyle.Format = "#,##0.00";


        }

        public void editar()
        {
            if (txtNoFactura.Text == string.Empty ||
                       txtCedula.Text == string.Empty || txtNombreC.Text == string.Empty)


            {
                MessageBox.Show("No puedes anular si no eliges un producto. ", "Anular Factura", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
            else
            {

                try
                {
                    venta.Id_facturacion = Convert.ToInt32(txtIdVenta.Text.Trim());
                    venta.No_factura = txtNoFactura.Text.Trim();
                    venta.Id_cliente = Convert.ToInt32(Id_cliente.Text.Trim());
                    venta.Fecha_venta = Convert.ToDateTime(txtfecha.Text.Trim());
                    venta.Impuesto = Convert.ToDecimal(txtImpuesto2.Text.Trim());
                    venta.Subtotal = Convert.ToDecimal(txtSubT.Text.Trim());
                    venta.MontoTotal = Convert.ToDecimal(txtTotal.Text.Trim());
                    venta.Estado = "Anulado";
                    venta.TipoVenta1 = CboTipoVentas.Text.Trim();
                    ventas.anularVentas(venta);

                    foreach (DataGridViewRow row in tablaFacturar.Rows)
                    {
                        detalleVenta.Id_detalle = Convert.ToInt32(row.Cells[0].Value.ToString());
                        detalleVenta.Id_facturacion = Convert.ToInt32(txtIdVenta.Text.Trim());
                        detalleVenta.Id_Inventario = Convert.ToInt32(row.Cells[1].Value.ToString());
                        detalleVenta.Cantidad = Convert.ToInt32(row.Cells[3].Value.ToString());
                        detalleVenta.Precio = Convert.ToDecimal(row.Cells[4].Value.ToString());
                        detalleVenta.Impuesto = Convert.ToDecimal(row.Cells[6].Value.ToString());
                        detalleVenta.Subtotal = Convert.ToDecimal(row.Cells[5].Value.ToString());
                        detalleVenta.MontoTotal = Convert.ToDecimal(row.Cells[7].Value.ToString());
                        detalleVenta.Estado = "Anulado"; 


                        DetalleVentas.eliminarDetalleFactura2(detalleVenta);

                    }
                    
                       
                    MessageBox.Show("los datos se han anulado correctamente.");
                    Anular();


                }
                catch (Exception ex)
                {
                    MessageBox.Show("no se ha anulado los datos. " + ex);
                }
            }
        }



        private void tablaFacturar_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

       

        private void btnfacturar_Click(object sender, EventArgs e)
        {
            editar();
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        

        private void txtCantidad_Leave(object sender, EventArgs e)
        {
           
            
        }
    }
}

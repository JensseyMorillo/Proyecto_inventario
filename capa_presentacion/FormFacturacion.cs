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
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System.IO;

namespace capa_presentacion
{
    public partial class FormFacturacion : Form
    {
        public FormFacturacion()
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

        protected void Agregar()
        {
            UpdateEventArgs args = new UpdateEventArgs();
            
        }

        private void GenerarCorrelativos()
        {
            txtNoFactura.Text = "FACT" + procedimiento.GenerarCodigo("facturacion");
            txtIdVenta.Text = procedimiento.GenerarCodigoId("facturacion");
        }

        private void FormFacturacion_Load(object sender, EventArgs e)
        {
            
            txtfecha.Text = hoy.ToString("yyyy-MM-dd");
            txtCodigo.Text.ToUpper();
            txtStock.Text = "0";
            txtImpuesto2.Text = "0.00";
            txtSubT.Text = "0.00";
            txtTotal.Text = "0.00";
            GenerarCorrelativos();
            mejorarVista();
            

        }

        public static double importe = 0, ImporteNeto = 0, ITBIS = 0, montototal= 0, porcentaje = 0;
        public static double importeNeto = 0, porcentaje1 = 0;

        public static int ContFila = 0;

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            agregarDetalle();
        }

       

        public static decimal Total = 0,Total1 = 0, total2 = 0, total3 = 0;

       

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                btnAgregar.Focus();
                e.Handled = true;
            }
        }

       
        private void btnfacturar_Click(object sender, EventArgs e)
        {
           

            if(txtNoFactura.Text == string.Empty || CboTipoVentas.Text == string.Empty ||
                        txtCedula.Text == string.Empty || txtNombreC.Text == string.Empty || txt_inve.Text == string.Empty)


            {
                MessageBox.Show("No puedes facturar si no eliges un producto. ", "Aviso Factura", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
            else
            {

                try
                {
                    venta.No_factura = txtNoFactura.Text.Trim();
                    venta.Id_cliente = Convert.ToInt32(Id_cliente.Text);
                    venta.Fecha_venta = Convert.ToDateTime(txtfecha.Text.Trim());
                    venta.Impuesto = Convert.ToDecimal(txtImpuesto2.Text.Trim());
                    venta.Subtotal = Convert.ToDecimal(txtSubT.Text.Trim());
                    venta.MontoTotal = Convert.ToDecimal(txtTotal.Text.Trim());
                    venta.Estado = "Vendido";
                    venta.TipoVenta1 = CboTipoVentas.Text.Trim();
                    ventas.insertarVentas(venta);

                    foreach (DataGridViewRow row in tablaFacturar.Rows)
                    {
                        detalleVenta.Id_facturacion = Convert.ToInt32(txtIdVenta.Text.Trim());
                        detalleVenta.Id_Inventario = Convert.ToInt32(row.Cells[0].Value.ToString());
                        detalleVenta.Cantidad = Convert.ToInt32(row.Cells[2].Value.ToString());
                        detalleVenta.Precio = Convert.ToDecimal(row.Cells[3].Value.ToString());
                        detalleVenta.Impuesto = Convert.ToDecimal(row.Cells[5].Value.ToString());
                        detalleVenta.Estado = row.Cells[7].Value.ToString();
                        detalleVenta.Subtotal = Convert.ToDecimal(row.Cells[4].Value.ToString());
                        detalleVenta.MontoTotal = Convert.ToDecimal(row.Cells[6].Value.ToString());
                       


                        DetalleVentas.insertarDetalleVentas(detalleVenta);

                    }

                    MessageBox.Show("se han guardado los datos. ");
                    facturar();
                    procedimiento.limpiarControles(this);
                    txtSubT.Text = "0.00";
                    txtImpuesto2.Text = "0.00";
                    txtTotal.Text = "0.00";
                    Agregar();
                    this.Hide();




                }
                catch (Exception ex)
                {
                    MessageBox.Show("no se han guardado los datos. ");
                }
            }

            
              
            
            
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Estas seguro que deseas cancelar esta compra?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                this.Hide();
            }


        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if(ContFila > 0)
                {
                    Total = Total - Convert.ToDecimal(tablaFacturar.Rows[tablaFacturar.CurrentRow.Index].Cells[4].Value);
                   

                    tablaFacturar.Rows.RemoveAt(tablaFacturar.CurrentRow.Index);
                    ContFila--;
                }

                Total1 = 0;
                total2 = 0;
                total3 = 0;

                foreach (DataGridViewRow row1 in tablaFacturar.Rows)
                {
                    Total1 += Convert.ToDecimal(row1.Cells[4].Value);
                    total2 += Convert.ToDecimal(row1.Cells[5].Value);
                    total3 += Convert.ToDecimal(row1.Cells[6].Value);


                }
                txtSubT.Text = Total1.ToString("N2");
                txtImpuesto2.Text = total2.ToString("N2");
                txtTotal.Text = total3.ToString("N2");

            }
            catch(Exception ex)
            {
                MessageBox.Show("No hay filas para eliminar." );

            }
        }



        private void tablaFacturar_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                
                    
                    txt_inve.Text = tablaFacturar.Rows[tablaFacturar.CurrentRow.Index].Cells[0].Value.ToString();
                    txtCodigo.Text = tablaFacturar.Rows[tablaFacturar.CurrentRow.Index].Cells[1].Value.ToString();
                    txtDescrip.Text = tablaFacturar.Rows[tablaFacturar.CurrentRow.Index].Cells[1].Value.ToString();
                    txtCantidad.Text = tablaFacturar.Rows[tablaFacturar.CurrentRow.Index].Cells[2].Value.ToString();
                    txtStock.Text = tablaFacturar.Rows[tablaFacturar.CurrentRow.Index].Cells[2].Value.ToString();
                    txtPrecio.Text = tablaFacturar.Rows[tablaFacturar.CurrentRow.Index].Cells[3].Value.ToString();
                    txtCantidad.Focus();

                    
                
               
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Debe seleccionar los datos que desee mostrar.");
            }

        }

        private void txtStock_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsNumber(e.KeyChar) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void txtCantidad_Leave(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false)
            {
                if (txtCantidad.Text == string.Empty)
                {
                    return;
                }
                else
                {
                    try
                    {
                        if (CboTipoVentas.Text == "TARJETA" && txtCantidad.Text != String.Empty)
                        {
                            importe = Convert.ToDouble(txtPrecio.Text) * Convert.ToDouble(txtCantidad.Text);
                            porcentaje = importe * 1.45;
                            importeNeto = Convert.ToDouble(porcentaje.ToString("N2"));
                            txtImpuesto.Text = "0.00";
                            montototal = Convert.ToDouble(importeNeto.ToString("N2"));
                            porcentaje1 = porcentaje;

                        }
                        else if (CboTipoVentas.Text == "EFECTIVO" && txtCantidad.Text != String.Empty)
                        {

                            importe = Convert.ToDouble(txtPrecio.Text) * Convert.ToDouble(txtCantidad.Text);
                            porcentaje = importe * 1.35;
                            importeNeto = Convert.ToDouble(porcentaje.ToString("N2"));
                            txtImpuesto.Text = "0.00";
                            montototal = Convert.ToDouble(importeNeto.ToString("N2"));
                            porcentaje1 = porcentaje;

                        }
                        else if (CboTipoVentas.Text == "ESPECIAL" && txtCantidad.Text != String.Empty)
                        {

                            importe = Convert.ToDouble(txtPrecio.Text) * Convert.ToDouble(txtCantidad.Text);
                            porcentaje = importe * 1.25;
                            importeNeto = Convert.ToDouble(porcentaje.ToString("N2"));
                            txtImpuesto.Text = "0.00";
                            montototal = Convert.ToDouble(importeNeto.ToString("N2"));
                            porcentaje1 = porcentaje;

                        }



                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al calcular impuesto " + ex.Message, "Calcular ITBIS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    }

                }
            }
            else
            {
                if (txtCantidad.Text == string.Empty)
                {
                    return;
                }
                else
                {
                    try
                    {
                        if (CboTipoVentas.Text == "TARJETA" && txtCantidad.Text != String.Empty)
                        {
                            importe = Convert.ToDouble(txtPrecio.Text) * Convert.ToDouble(txtCantidad.Text);
                            porcentaje = importe * 1.45;
                            importeNeto = Convert.ToDouble(porcentaje.ToString("N2"));
                            ITBIS = importeNeto * 0.18;
                            txtImpuesto.Text = ITBIS.ToString("N2");
                            montototal = Convert.ToDouble(importeNeto + Convert.ToDouble(txtImpuesto.Text));
                            porcentaje1 = porcentaje;

                        }
                        else if (CboTipoVentas.Text == "EFECTIVO" && txtCantidad.Text != String.Empty)
                        {

                            importe = Convert.ToDouble(txtPrecio.Text) * Convert.ToDouble(txtCantidad.Text);
                            porcentaje = importe * 1.35;
                            importeNeto = Convert.ToDouble(porcentaje.ToString("N2"));
                            ITBIS = importeNeto * 0.18;
                            txtImpuesto.Text = ITBIS.ToString("N2");
                            montototal = Convert.ToDouble(importeNeto + Convert.ToDouble(txtImpuesto.Text));
                            porcentaje1 = porcentaje;

                        }
                        else if (CboTipoVentas.Text == "ESPECIAL" && txtCantidad.Text != String.Empty)
                        {

                            importe = Convert.ToDouble(txtPrecio.Text) * Convert.ToDouble(txtCantidad.Text);
                            porcentaje = importe * 1.25;
                            importeNeto = Convert.ToDouble(porcentaje.ToString("N2"));
                            ITBIS = importeNeto * 0.18;
                            txtImpuesto.Text = ITBIS.ToString("N2");
                            montototal = Convert.ToDouble(importeNeto + Convert.ToDouble(txtImpuesto.Text));
                            porcentaje1 = porcentaje;

                        }




                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al calcular impuesto " + ex.Message, "Calcular ITBIS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    }

                }
            }
            
        }

      

        private void btnllenar_Click(object sender, EventArgs e)
        {
            FrmVistaVentas vistaVentas = new FrmVistaVentas();
            vistaVentas.ShowDialog();

            try
            {
                if (vistaVentas.DialogResult == DialogResult.OK)
                {
                    txt_inve.Text = vistaVentas.tablaInvetario.Rows[vistaVentas.tablaInvetario.CurrentRow.Index].Cells[0].Value.ToString();
                    txtCodigo.Text = vistaVentas.tablaInvetario.Rows[vistaVentas.tablaInvetario.CurrentRow.Index].Cells[1].Value.ToString();
                    txtDescrip.Text = vistaVentas.tablaInvetario.Rows[vistaVentas.tablaInvetario.CurrentRow.Index].Cells[2].Value.ToString();
                    txtStock.Text = vistaVentas.tablaInvetario.Rows[vistaVentas.tablaInvetario.CurrentRow.Index].Cells[4].Value.ToString();
                    txtPrecio.Text = vistaVentas.tablaInvetario.Rows[vistaVentas.tablaInvetario.CurrentRow.Index].Cells[3].Value.ToString();


                    txtCantidad.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Debe seleccionar los datos que desee mostrar.");
            }
        }

        public void limpiarcampos()
        {

            txtNombreC.Text = "";
            txtCedula.Text = "";
            txtCantidad.Text = "";
            txtCodigo.Text = "";
            txtDescrip.Text = "";
            txtImpuesto.Text = "";
            txtStock.Text = "";
            txtPrecio.Text = "";
            CboTipoVentas.Focus();
        }

        public void limpiar()
        {
           
            txtCantidad.Text = "";
            txtCodigo.Text = "";
            txtDescrip.Text = "";
            txtImpuesto.Text = "";
            txtStock.Text = "";
            txtPrecio.Text = "";
            txtCantidad.Focus();
        }


        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            FrmVistaCliente vista_cliente = new FrmVistaCliente();
            vista_cliente.ShowDialog();

            try
            {
                if(vista_cliente.DialogResult == DialogResult.OK)
                {
                    Id_cliente.Text = vista_cliente.tablaClientes.Rows[vista_cliente.tablaClientes.CurrentRow.Index].Cells[0].Value.ToString();
                    txtCedula.Text = vista_cliente.tablaClientes.Rows[vista_cliente.tablaClientes.CurrentRow.Index].Cells[3].Value.ToString();
                    txtNombreC.Text = vista_cliente.tablaClientes.Rows[vista_cliente.tablaClientes.CurrentRow.Index].Cells[1].Value.ToString();
                    txtCodigo.Focus();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Debe seleccionar los datos que desee mostrar.");
            }
        }

        private void agregarDetalle()
        {
            try
            {
                if(Convert.ToInt32(txtStock.Text) == 0)
                {
                    MessageBox.Show("No hay Existencia de este Producto. ", "Agregar Producto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else
                {
                    if(txtNoFactura.Text == string.Empty || CboTipoVentas.Text== string.Empty ||
                        txtCedula.Text==string.Empty || txtNombreC.Text == string.Empty || txtDescrip.Text == string.Empty || txtPrecio.Text == string.Empty)
                        
                    {
                        MessageBox.Show("Debe de completar todas los campos. ", "Agregar Producto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    }
                    else
                    {
                        bool existe = false;
                        int noFila = 0;

                        if(ContFila == 0)
                        {
                            

                            tablaFacturar.Rows.Add(txt_inve.Text, txtDescrip.Text,txtCantidad.Text,txtPrecio.Text,importeNeto.ToString("N2"), txtImpuesto.Text,montototal.ToString("N2"),txtEstado.Text);
                            tablaFacturar.ClearSelection();

                            btnllenar.Focus();

                            ContFila++;
                        }
                        else
                        {
                            foreach(DataGridViewRow row in tablaFacturar.Rows)
                            {
                                if(row.Cells[0].Value.ToString()== txt_inve.Text)
                                {
                                    existe = true;
                                }
                            }

                            if(existe == true)
                            {
                                tablaFacturar.Rows[noFila].Cells[2].Value = Convert.ToDouble(txtCantidad.Text) + Convert.ToDouble(tablaFacturar.Rows[noFila].Cells[2].Value);
                                tablaFacturar.Rows[noFila].Cells[4].Value = (importeNeto + Convert.ToDouble(tablaFacturar.Rows[noFila].Cells[4].Value)).ToString("N2");
                                tablaFacturar.Rows[noFila].Cells[5].Value = (Convert.ToDouble(txtImpuesto.Text) + Convert.ToDouble(tablaFacturar.Rows[noFila].Cells[5].Value)).ToString("N2");
                                tablaFacturar.Rows[noFila].Cells[6].Value = (montototal + Convert.ToDouble(tablaFacturar.Rows[noFila].Cells[6].Value)).ToString("N2");

                            }
                            else
                            {
                                tablaFacturar.Rows.Add(txt_inve.Text, txtDescrip.Text, txtCantidad.Text, txtPrecio.Text, importeNeto.ToString("N2"), txtImpuesto.Text,montototal.ToString("N2"),txtEstado.Text);  
                                tablaFacturar.ClearSelection();

                                btnllenar.Focus();

                                ContFila++;
                            }
                        }
                        Total1 = 0;
                        total2 = 0;
                        total3 = 0;
                        foreach (DataGridViewRow row1 in tablaFacturar.Rows)
                        {
                            Total1 += Convert.ToDecimal(row1.Cells[4].Value);
                            total2 += Convert.ToDecimal(row1.Cells[5].Value);
                            total3 += Convert.ToDecimal(row1.Cells[6].Value);


                        }
                        txtSubT.Text = Total1.ToString("N2");
                        txtImpuesto2.Text = total2.ToString("N2");
                        txtTotal.Text = total3.ToString("N2");

                        limpiar();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Debe de completar los campos de la factura. ");

            }
        }

       private void facturar()
        {
            SaveFileDialog guardar = new SaveFileDialog();
            guardar.FileName = txtNoFactura.Text + ".pdf";
            guardar.ShowDialog();


            string paginaHtml_texto = Properties.Resources.Plantilla.ToString();
            paginaHtml_texto = paginaHtml_texto.Replace("@CLIENTE", txtNombreC.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@DOCUMENTO", txtCedula.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@FECHA", txtfecha.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@FACTURA", txtNoFactura.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@TIPOPAGO", CboTipoVentas.Text);
          

            string filas = string.Empty;


            foreach (DataGridViewRow row in tablaFacturar.Rows)
            {

                filas += "<tr>";
                filas += "<td>" + row.Cells[1].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells[2].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells[4].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells[5].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells[6].Value.ToString() + "</td>";
                filas += "</tr>";


            }
            paginaHtml_texto = paginaHtml_texto.Replace("@FILAS", filas);
            paginaHtml_texto = paginaHtml_texto.Replace("@SUBTOTAL", txtSubT.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@ITBIS", txtImpuesto2.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@TOTAL", txtTotal.Text);


            if (guardar.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(guardar.FileName, FileMode.Create))
                {
                    Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);

                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();
                    pdfDoc.Add(new Phrase(""));

                    iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(Properties.Resources.WhatsApp_Image_2023_01_12_at_20_32_58__4_, System.Drawing.Imaging.ImageFormat.Png);
                    img.ScaleToFit(80, 60);
                    img.Alignment = iTextSharp.text.Image.UNDERLYING;
                    img.SetAbsolutePosition(pdfDoc.LeftMargin, pdfDoc.Top - 60);
                    pdfDoc.Add(img);

                    using (StringReader sr = new StringReader(paginaHtml_texto))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    }

                    pdfDoc.Close();
                    stream.Close();
                }
            }


        }

        private void mejorarVista()
        {
            tablaFacturar.Columns[1].Width = 150;
            tablaFacturar.Columns[2].Width = 150;
            tablaFacturar.Columns[3].Width = 150;
            tablaFacturar.Columns[4].Width = 150;
            tablaFacturar.Columns[5].Width = 150;
            tablaFacturar.Columns[6].Width = 150;

            tablaFacturar.Columns[4].DefaultCellStyle.Format = "#,##0.00";
            tablaFacturar.Columns[5].DefaultCellStyle.Format = "#,##0.00";
            tablaFacturar.Columns[6].DefaultCellStyle.Format = "#,##0.00";


        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capa_entidad
{
    public class E_Ventas
    {
        private int id_facturacion;
        private int id_cliente;
        private string no_factura;
        private decimal impuesto;
        private decimal subtotal;
        private decimal montoTotal;
        private string estado;
        private DateTime fecha_venta;
        private string TipoVenta;
        public int Id_facturacion { get => id_facturacion; set => id_facturacion = value; }
        public int Id_cliente { get => id_cliente; set => id_cliente = value; }
        public string No_factura { get => no_factura; set => no_factura = value; }
        public decimal Impuesto { get => impuesto; set => impuesto = value; }
        public decimal Subtotal { get => subtotal; set => subtotal = value; }
        public decimal MontoTotal { get => montoTotal; set => montoTotal = value; }
        public string Estado { get => estado; set => estado = value; }
        public DateTime Fecha_venta { get => fecha_venta; set => fecha_venta = value; }
        public string TipoVenta1 { get => TipoVenta; set => TipoVenta = value; }
    }
}

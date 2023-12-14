using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capa_entidad
{
    public class E_DetalleVentas
    {
        private int id_detalle;
        private int id_facturacion;
        private int id_Inventario;
        private int cantidad;
        private decimal precio;
        private decimal impuesto;
        private decimal subtotal;
        private decimal montoTotal;
        private string estado;
       

        public int Id_detalle { get => id_detalle; set => id_detalle = value; }
        public int Id_facturacion { get => id_facturacion; set => id_facturacion = value; }
        public int Id_Inventario { get => id_Inventario; set => id_Inventario = value; }
        public int Cantidad { get => cantidad; set => cantidad = value; }
        public decimal Precio { get => precio; set => precio = value; }
        public decimal Impuesto { get => impuesto; set => impuesto = value; }
        public decimal Subtotal { get => subtotal; set => subtotal = value; }
        public decimal MontoTotal { get => montoTotal; set => montoTotal = value; }
        public string Estado { get => estado; set => estado = value; }
    }
}

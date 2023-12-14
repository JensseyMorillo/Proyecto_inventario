using capa_entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using capa_datos;

namespace capa_negocio
{
    public class N_Detalle_venta
    {
        D_Detalle_venta objdatos = new D_Detalle_venta();
        public void insertarDetalleVentas(E_DetalleVentas venta)
        {
            objdatos.insertarDetalleVentas(venta);
        }

        public void editarDetalleVentas(E_DetalleVentas venta)
        {
            objdatos.editarDetalleVentas(venta);

        }
        public void eliminarDetalleFactura2(E_DetalleVentas cliente)
        {
            objdatos.eliminarDetalleFactura2(cliente);
        }

        public DataTable MostrarDetallesVentaCO(string buscar)
        {
            return objdatos.MostrarDetalleVentasCO(buscar);
        }
        public DataTable MostrarDetallesVentaCO2(int buscar)
        {
            return objdatos.MostrarDetalleVentasCO2(buscar);
        }

    }
}


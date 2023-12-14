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
    public class N_Ventas
    {
        D_ventas objVentas = new D_ventas();

        public void insertarVentas(E_Ventas venta)
        {
            objVentas.insertarVentas(venta);
        }

        public void anularVentas(E_Ventas venta)
        {
            objVentas.anularVentas(venta);
        }
        public void eliminarFactura2(E_Ventas cliente)
        {
            objVentas.eliminarFactura2(cliente);
        }
        public void eliminarFactura(E_Ventas ventas)
        {
            objVentas.eliminarFactura(ventas);
        }

        //public DataTable MostrarIngresoVentas(string buscar)
        //{
        //    return objVentas.MostrarTngresoVentas(buscar);
        //}

        public List<E_inventario> MostrarDatosVentas(string buscar)
        {
            return objVentas.MostrarProductoVentas(buscar);
        }
    }
}

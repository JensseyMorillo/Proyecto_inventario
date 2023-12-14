using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using capa_datos;
using capa_entidad;

namespace capa_negocio
{
    public class N_inventario
    {
        D_inventario objdatos = new D_inventario();

        public void insertarProductos(E_inventario inventario)
        {
            objdatos.insertarProductos(inventario);
        }

        public void EditarInventario(E_inventario inventario)
        {
            objdatos.EditarInventario(inventario);
        }

        public void EliminarInventario(E_inventario inventario)
        {
            objdatos.EliminarInventario(inventario);

        }

        public List<E_inventario> datosInventario(string buscar)
        {
            return objdatos.datosInventario(buscar);
        }

        public List<E_inventario> datosInventarioCodigo(string buscar)
        {
            return objdatos.datosInventarioCodigo(buscar);
        }
    }
}

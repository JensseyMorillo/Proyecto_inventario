using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using capa_datos;
using capa_entidad;

namespace capa_negocio
{
    public class N_cliente
    {
        D_Cliente objdatos = new D_Cliente();

        public void insertarCliente(E_cliente cliente)
        {
            objdatos.insertarCliente(cliente);
        }

        public void editarCliente(E_cliente cliente)
        {
            objdatos.editarCliente(cliente);
        }

        public void eliminarCliente(E_cliente cliente)
        {
            objdatos.eliminarCliente(cliente);
        }

        public List<E_cliente> datosCliente(string buscar)
        {
            return objdatos.datosCliente(buscar);
        }

        public List<E_cliente> datosCliente2(string buscar)
        {
            return objdatos.datosCliente2(buscar);
        }

    }
}

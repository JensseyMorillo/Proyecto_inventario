using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capa_entidad
{
    public class E_inventario
    {
        private int id_inventario;
        private string codigo;
        private string descripcion;
        private decimal precio;
        private int excistencia;
        private string estado;

        public int ID_inventario { get => id_inventario; set => id_inventario = value; }
        public string Codigo { get => codigo; set => codigo = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public decimal Precio { get => precio; set => precio = value; }
        public int Excistencia { get => excistencia; set => excistencia = value; }
        public string Estado { get => estado; set => estado = value; }
    }
}

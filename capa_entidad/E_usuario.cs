using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capa_entidad
{
    public class E_usuario
    {
        private int IDusuario;
        private string tipo_usuario;
        private string usuario;
        private string contraseña;

        public int IDusuario1 { get => IDusuario; set => IDusuario = value; }
        public string Tipo_usuario { get => tipo_usuario; set => tipo_usuario = value; }
        public string Usuario { get => usuario; set => usuario = value; }
        public string Contraseña { get => contraseña; set => contraseña = value; }
    }
}

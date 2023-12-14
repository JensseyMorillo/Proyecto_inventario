using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using capa_entidad;
using capa_datos;

namespace capa_negocio
{
    public class N_usuario
    {
        D_usuario objdatos = new D_usuario();

        public bool selectlogin(string usuario, string contraseña)
        {
            return objdatos.selectlogin(usuario, contraseña);
        }

        public void metodoCargo()
        {
            if (UserLoginCache.cargo == cargo.Administrador)
            {
                //codes
            }
            if (UserLoginCache.cargo == cargo.Usuario)
            {
                //codes
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos;

internal class Login
{
    public string NombreUsuario { get; set; }
    public string Contraseña { get; set; }
    public string Rol { get; set; }

    

    public Login(string nombreUsuario, string contraseña, string rol)
    {
        NombreUsuario = nombreUsuario;
        Contraseña = contraseña;
        Rol = rol;
    }
}

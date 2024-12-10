using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class E_Empleado
    {
        public int IdEmpleado { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public bool Estado { get; set; }
        public int IdTipodeEmpleado { get; set; }
        public int IdRole { get; set; }
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class E_Roles
    {
        public int IdRole { get; set; }
        public string Role { get; set; }
        public bool Estado { get; set; } // 1 para Activo, 0 para Inactivo
    }
}

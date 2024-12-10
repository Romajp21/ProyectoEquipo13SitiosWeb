using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class E_Correo
    {
        public string Modulo { get; set; } // Módulo (Artistas, Clientes, etc.)
        public string Correo { get; set; } // Dirección de correo
        public string Nombre { get; set; } // Nombre del destinatario (opcional, si se requiere)
        public string Apellido { get; set; } // Apellido del destinatario (opcional, si se requiere)
    }
}

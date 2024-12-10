using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class E_MembresiaContenido
    {
        public int IdMembresContenido { get; set; }
        public int IdMembresia { get; set; }
        public string NombreMembresia { get; set; } // Solo lectura, no se usa para crear o actualizar
        public string NombreContenido { get; set; }
        public int CreadoPor { get; set; }
        public string Comentario { get; set; }

        public bool Estado { get; set; } // Nuevo campo de Estado
    }
}

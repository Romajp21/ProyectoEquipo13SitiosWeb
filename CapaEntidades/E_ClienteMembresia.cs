using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class E_ContenidoMembresia
    {
        public int IdMembresContenido { get; set; }
        public string NombreContenido { get; set; }
        public string ComentarioAprobacion { get; set; }
        public DateTime FechaAprobacion { get; set; }
        public bool EstadoAprobacion { get; set; }
    }

    public class E_ClienteMembresia
    {
        public int IdMembresia { get; set; }
        public string NombreMembresia { get; set; }
        public decimal Precio { get; set; }
        public int Duracion { get; set; } // En meses
        public List<E_ContenidoMembresia> Contenidos { get; set; } = new List<E_ContenidoMembresia>();
    }
}
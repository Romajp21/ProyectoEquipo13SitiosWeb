using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class E_Artista
    {
        //public int IdArtista { get; set; }
        //public string Nombre { get; set; }
        //public string Apellidos { get; set; }
        //public string Telefono { get; set; }
        //public string Correo { get; set; }
        //public bool Estado { get; set; }
        //public int IdRole { get; set; }


        public int IdArtista { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public bool Estado { get; set; }
        public List<string> Especialidades { get; set; } = new List<string>();
        public List<E_Portafolio> Portafolio { get; set; } = new List<E_Portafolio>(); // Lista del portafolio
        public int IdRole { get; set; }
        public string Rol { get; set; }
        public bool AprobadoPorJefatura { get; set; }
        public string ImagenPerfil { get; set; }
        public string Bio { get; set; } // Nueva propiedad para la biografía del artista



    }
}

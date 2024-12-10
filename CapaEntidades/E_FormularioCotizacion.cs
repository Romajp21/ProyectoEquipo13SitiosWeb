using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class E_FormularioCotizacion
    {
        public int IdFormulario { get; set; }
        public DateTime Fecha { get; set; }
        public int IDCliente { get; set; }
        public int IdCategoriaTatuajes { get; set; }
        public int IdArtista { get; set; }
        public int IdParteDelCuerpo { get; set; }
        public int IdTamano { get; set; }
        public decimal TiempoEstimado { get; set; }
        public int SesionesEstimadas { get; set; }
        public decimal PrecioCotizado { get; set; }
        public string Estado { get; set; }
        public string ComentarioAsistente { get; set; }
        public string ComentarioArtista { get; set; }
        public string ComentarioJefe { get; set; }
    }
}

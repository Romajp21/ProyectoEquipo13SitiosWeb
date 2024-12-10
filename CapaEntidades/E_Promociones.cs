using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class E_Promociones
    {
        public int IdPromocion { get; set; }
        public string DescripcionPromocion { get; set; }
        public int CantidadDisponibles { get; set; }
        public decimal PrecioOriginal { get; set; }
        public decimal PorcentajeDeDescuento { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int CreadoPor { get; set; }
    }
}

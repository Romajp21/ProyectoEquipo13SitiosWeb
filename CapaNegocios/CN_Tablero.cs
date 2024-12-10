using CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocios
{
    public class CN_Tablero
    {
        private readonly CD_Tablero _dataLayer = new CD_Tablero();

        public Dictionary<string, object> ObtenerDatosTablero(string rol, int? idUsuario, out string mensaje)
        {
            return _dataLayer.ObtenerDatosTablero(rol, idUsuario, out mensaje);
        }
    }
}
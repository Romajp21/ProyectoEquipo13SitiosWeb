using CapaDatos;
using CapaEntidades;
using System.Collections.Generic;

namespace CapaNegocios
{
    public class CN_ClienteMembresia
    {
        private readonly CD_ClienteMembresia _cdClienteMembresia = new CD_ClienteMembresia();

        // Método para listar membresías y su contenido aprobado
        public List<E_ClienteMembresia> ListarMembresias(out string mensaje)
        {
            mensaje = string.Empty;

            // Llama al método de la capa de datos
            List<E_ClienteMembresia> membresias = _cdClienteMembresia.ListarMembresias(out mensaje);

            // Validación de negocio: verifica que existan membresías
            if (membresias == null || membresias.Count == 0)
            {
                mensaje = "No se encontraron membresías activas con contenido aprobado.";
                return new List<E_ClienteMembresia>();
            }

            return membresias;
        }
    }
}

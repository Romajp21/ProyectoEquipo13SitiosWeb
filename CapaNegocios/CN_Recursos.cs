using System.Collections.Generic;
using CapaDatos; // Asegúrate de tener referencia a la capa de datos

namespace CapaNegocios
{
    public class CN_Recursos
    {
        // Instancia de la clase de la capa de datos
        private CD_Recursos _datos = new CD_Recursos();
  
        /// <summary>
        /// Método para obtener correos basados en el módulo (Artistas, Asistente, Jefatura, Clientes)
        /// </summary>
        /// <param name="modulo">Nombre del módulo (Artistas, Asistente, Jefatura, Clientes)</param>
        /// <returns>Lista de correos electrónicos asociados al módulo</returns>
        public List<string> ObtenerCorreosPorModulo(string modulo)
        {
            // Validar que el módulo no sea nulo o vacío
            if (string.IsNullOrWhiteSpace(modulo))
                throw new System.ArgumentException("El módulo no puede estar vacío.");

            // Validar que el módulo sea uno permitido
            var modulosPermitidos = new List<string> { "Artistas", "Asistente", "Jefatura", "Clientes" };
            if (!modulosPermitidos.Contains(modulo))
                throw new System.ArgumentException($"El módulo '{modulo}' no es válido. Debe ser uno de los siguientes: {string.Join(", ", modulosPermitidos)}.");

            // Consumir el método de la capa de datos y retornar la lista de correos
            return _datos.ObtenerCorreos(modulo);
        }
    }
}

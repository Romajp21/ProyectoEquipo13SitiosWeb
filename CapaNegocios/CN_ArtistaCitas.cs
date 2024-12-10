using CapaDatos;
using CapaEntidades;
using System.Collections.Generic;

namespace CapaNegocios
{
    public class CN_ArtistaCitas
    {
        private readonly CD_ArtistaCitas _capaDatos = new CD_ArtistaCitas();

        // Método para listar citas por artista
        public List<E_ArtistaCitas> ListarCitasPorArtista(int idArtista, out string mensaje)
        {
            mensaje = string.Empty;

            if (idArtista <= 0)
            {
                mensaje = "El ID del artista no es válido.";
                return new List<E_ArtistaCitas>();
            }

            return _capaDatos.ListarCitasPorArtista(idArtista, out mensaje);
        }

        // Método para actualizar el estado de una cita
        public bool ActualizarEstadoCita(int idCita, out string mensaje)
        {
            mensaje = string.Empty;

            // Validar el ID de la cita
            if (idCita <= 0)
            {
                mensaje = "El ID de la cita no es válido.";
                return false;
            }

            // Llamar al método de la capa de datos para actualizar el estado a "Completado"
            return _capaDatos.ActualizarEstadoCita(idCita, out mensaje);
        }
    }
}

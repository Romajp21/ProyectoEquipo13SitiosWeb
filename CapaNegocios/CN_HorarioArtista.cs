using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;

namespace CapaNegocios
{
    public class CN_HorarioArtista
    {
        private readonly CapaDatos.CD_HorarioArtista objCapaDatos = new CapaDatos.CD_HorarioArtista();

        // Método para listar horarios por artista
        public List<E_HorarioArtista> ListarHorarios(int? idArtista, out string mensaje)
        {
            // Manejar el caso donde idArtista sea null
            if (!idArtista.HasValue)
            {
                idArtista = 0; // Valor por defecto para listar todos los horarios
            }

            return objCapaDatos.ListarHorarios(idArtista.Value, out mensaje);
        }

        // Método para registrar un nuevo horario
        public bool RegistrarHorario(E_HorarioArtista horario, out string mensaje)
        {
            if (!ValidarHorario(horario, out mensaje))
            {
                return false;
            }

            return objCapaDatos.RegistrarHorario(horario, out mensaje);
        }

        // Método para actualizar un horario existente
        public bool ActualizarHorario(E_HorarioArtista horario, out string mensaje)
        {
            if (!ValidarHorario(horario, out mensaje))
            {
                return false;
            }

            return objCapaDatos.ActualizarHorario(horario, out mensaje);
        }

        // Método para eliminar un horario
        public bool EliminarHorario(int idHorario, out string mensaje)
        {
            if (idHorario <= 0)
            {
                mensaje = "El ID del horario no es válido.";
                return false;
            }

            return objCapaDatos.EliminarHorario(idHorario, out mensaje);
        }

        // Método privado para validar un horario
        private bool ValidarHorario(E_HorarioArtista horario, out string mensaje)
        {
            mensaje = string.Empty;

            if (horario.IdArtista <= 0)
            {
                mensaje = "El ID del artista no es válido.";
                return false;
            }

            if (horario.Fecha == default(DateTime))
            {
                mensaje = "La fecha del horario no es válida.";
                return false;
            }

            if (horario.HoraInicio >= horario.HoraFin)
            {
                mensaje = "La hora de inicio debe ser menor que la hora de fin.";
                return false;
            }

            return true;
        }

        // Obtener un horario por ID
        public E_HorarioArtista ObtenerHorarioPorId(int idHorarioArtista, out string mensaje)
        {
            if (idHorarioArtista <= 0)
            {
                mensaje = "El ID del horario no es válido.";
                return null;
            }

            return objCapaDatos.ObtenerHorarioPorId(idHorarioArtista, out mensaje);
        }
    }
}

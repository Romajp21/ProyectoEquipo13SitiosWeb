using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_AgendaArtista
    {
        // Método para obtener la conexión a la base de datos
        private SqlConnection ObtenerConexion()
        {
            SqlConnection conn = new SqlConnection(Conexion.cn);
            conn.Open();
            return conn;
        }

        // Método para listar la agenda de un artista en una fecha específica (o todas si la fecha es null)
        public List<E_AgendaArtista> ListarAgenda(int idArtista, int IdHorarioArtista, string fecha, out string mensaje)
        {
            List<E_AgendaArtista> lista = new List<E_AgendaArtista>();
            mensaje = string.Empty;

            try
            {
                using (SqlConnection conn = ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_CRUDAgendaArtista", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Parámetros del procedimiento almacenado
                    cmd.Parameters.AddWithValue("@Accion", 2);
                    cmd.Parameters.AddWithValue("@IdHorarioArtista", (object)IdHorarioArtista ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@IdArtista", (object)idArtista ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Fecha", (object)fecha ?? DBNull.Value);
                    cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                    // Leer los datos del procedimiento almacenado
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new E_AgendaArtista
                            {
                                IdAgendaArtista = Convert.ToInt32(dr["IdAgendaArtista"]),
                                IdArtista = Convert.ToInt32(dr["IdArtista"]),
                                Fecha = dr["Fecha"] != DBNull.Value ? Convert.ToDateTime(dr["Fecha"]) : DateTime.MinValue,
                                HoraInicio = TimeSpan.Parse(dr["HoraInicio"].ToString()),
                                HoraFin = TimeSpan.Parse(dr["HoraFin"].ToString()),
                                Estado = dr["Estado"].ToString(),
                                IdHorarioArtista = dr["IdHorarioArtista"] != DBNull.Value ? Convert.ToInt32(dr["IdHorarioArtista"]) : (int?)null
                            });
                        }
                    }

                    // Obtener el mensaje del procedimiento almacenado
                    mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                mensaje = $"Error: {ex.Message}. Trace: {ex.StackTrace}";
            }

            return lista;
        }

        // Método para registrar una nueva agenda
        public bool RegistrarAgenda(E_AgendaArtista agenda, out string mensaje)
        {
            mensaje = string.Empty;
            bool resultado = false;

            try
            {
                using (SqlConnection conn = ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_CRUDAgendaArtista", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Parámetros del procedimiento almacenado
                    cmd.Parameters.AddWithValue("@Accion", 1);
                    cmd.Parameters.AddWithValue("@IdHorarioArtista", (object)agenda.IdHorarioArtista ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@IdArtista", agenda.IdArtista);
                    cmd.Parameters.AddWithValue("@Fecha", agenda.Fecha);
                    cmd.Parameters.AddWithValue("@HoraInicio", agenda.HoraInicio);
                    cmd.Parameters.AddWithValue("@HoraFin", agenda.HoraFin);
                    cmd.Parameters.AddWithValue("@Estado", agenda.Estado);
                    cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                    // Ejecutar el comando
                    cmd.ExecuteNonQuery();

                    // Obtener el resultado y el mensaje del procedimiento almacenado
                    resultado = Convert.ToBoolean(cmd.Parameters["@Resultado"].Value);
                    mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                mensaje = $"Error: {ex.Message}. Trace: {ex.StackTrace}";
            }

            return resultado;
        }

        // Método para actualizar una agenda existente
        public bool ActualizarAgenda(E_AgendaArtista agenda, out string mensaje)
        {
            mensaje = string.Empty;
            bool resultado = false;

            try
            {
                using (SqlConnection conn = ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_CRUDAgendaArtista", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Parámetros del procedimiento almacenado
                    cmd.Parameters.AddWithValue("@Accion", 3);
                    cmd.Parameters.AddWithValue("@IdAgendaArtista", agenda.IdAgendaArtista);
                    cmd.Parameters.AddWithValue("@IdHorarioArtista", (object)agenda.IdHorarioArtista ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@IdArtista", agenda.IdArtista);
                    cmd.Parameters.AddWithValue("@Fecha", agenda.Fecha);
                    cmd.Parameters.AddWithValue("@HoraInicio", agenda.HoraInicio);
                    cmd.Parameters.AddWithValue("@HoraFin", agenda.HoraFin);
                    cmd.Parameters.AddWithValue("@Estado", agenda.Estado);
                    cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                    // Ejecutar el comando
                    cmd.ExecuteNonQuery();

                    // Obtener el resultado y el mensaje del procedimiento almacenado
                    resultado = Convert.ToBoolean(cmd.Parameters["@Resultado"].Value);
                    mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                mensaje = $"Error: {ex.Message}. Trace: {ex.StackTrace}";
            }

            return resultado;
        }

        // Método para eliminar una agenda
        public bool EliminarAgenda(int idAgenda, out string mensaje)
        {
            mensaje = string.Empty;
            bool resultado = false;

            try
            {
                using (SqlConnection conn = ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_CRUDAgendaArtista", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Parámetros del procedimiento almacenado
                    cmd.Parameters.AddWithValue("@Accion", 4); // Acción de eliminación
                    cmd.Parameters.AddWithValue("@IdAgendaArtista", idAgenda);
                    cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                    // Ejecutar el comando
                    cmd.ExecuteNonQuery();

                    // Obtener el resultado y el mensaje del procedimiento almacenado
                    resultado = Convert.ToBoolean(cmd.Parameters["@Resultado"].Value);
                    mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                mensaje = $"Error: {ex.Message}";
            }

            return resultado;
        }

        // Método para listar horarios disponibles para un artista
        public List<E_HorarioArtista> ListarHorariosDisponibles(int idArtista, out string mensaje)
        {
            List<E_HorarioArtista> lista = new List<E_HorarioArtista>();
            mensaje = string.Empty;

            try
            {
                using (SqlConnection conn = ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_CRUDAgendaArtista", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Parámetros del procedimiento almacenado
                    cmd.Parameters.AddWithValue("@Accion", 5); // Nueva acción para listar horarios
                    cmd.Parameters.AddWithValue("@IdArtista", idArtista);
                    cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                    // Leer los datos del procedimiento almacenado
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new E_HorarioArtista
                            {
                                IdHorarioArtista = Convert.ToInt32(dr["IdHorarioArtista"]),
                                IdArtista = Convert.ToInt32(dr["IdArtista"]),
                                Fecha = dr["Fecha"] != DBNull.Value ? Convert.ToDateTime(dr["Fecha"]) : DateTime.MinValue,
                                HoraInicio = TimeSpan.Parse(dr["HoraInicio"].ToString()),
                                HoraFin = TimeSpan.Parse(dr["HoraFin"].ToString()),
                                Estado = dr["Estado"] != DBNull.Value && bool.TryParse(dr["Estado"].ToString(), out bool estado) ? estado : false

                            });
                        }
                    }

                    // Obtener el mensaje del procedimiento almacenado
                    mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                mensaje = $"Error: {ex.Message}. Trace: {ex.StackTrace}";
            }

            return lista;
        }

        // Método para obtener la agenda por ID
        public E_AgendaArtista ObtenerAgendaPorId(int idAgenda, out string mensaje)
        {
            mensaje = string.Empty;
            E_AgendaArtista agenda = null;

            try
            {
                using (SqlConnection conn = ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_CRUDAgendaArtista", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Parámetros del procedimiento almacenado
                    cmd.Parameters.AddWithValue("@Accion", 6); // Nueva acción para obtener agenda por ID
                    cmd.Parameters.AddWithValue("@IdAgendaArtista", idAgenda);
                    cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                    // Leer los datos del procedimiento almacenado
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            agenda = new E_AgendaArtista
                            {
                                IdAgendaArtista = Convert.ToInt32(dr["IdAgendaArtista"]),
                                IdArtista = Convert.ToInt32(dr["IdArtista"]),
                                IdHorarioArtista = dr["IdHorarioArtista"] != DBNull.Value ? Convert.ToInt32(dr["IdHorarioArtista"]) : (int?)null,
                                Fecha = dr["Fecha"] != DBNull.Value ? Convert.ToDateTime(dr["Fecha"]) : DateTime.MinValue,
                                HoraInicio = TimeSpan.Parse(dr["HoraInicio"].ToString()),
                                HoraFin = TimeSpan.Parse(dr["HoraFin"].ToString()),
                                Estado = dr["Estado"].ToString()
                            };
                        }
                    }

                    // Obtener el mensaje del procedimiento almacenado
                    mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                mensaje = $"Error: {ex.Message}. Trace: {ex.StackTrace}";
            }

            return agenda;
        }

    }
}

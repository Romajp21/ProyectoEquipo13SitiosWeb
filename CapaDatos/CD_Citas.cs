using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_Citas
    {
        private SqlConnection ObtenerConexion()
        {
            SqlConnection conn = new SqlConnection(Conexion.cn);
            conn.Open();
            return conn;
        }

        // Método para obtener horarios disponibles
        public List<E_DisponibilidadCitas> ObtenerHorariosDisponibles(int? idArtista, int horasSesion, int sesionesEstimadas, out string mensaje)
        {
            List<E_DisponibilidadCitas> lista = new List<E_DisponibilidadCitas>();
            mensaje = string.Empty;

            try
            {
                using (SqlConnection conn = ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_CRUDCitas", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Accion", 5); // Acción para obtener disponibilidad
                    cmd.Parameters.AddWithValue("@IdArtista", (object)idArtista ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@HorasSesion", horasSesion);
                    cmd.Parameters.AddWithValue("@SesionesEstimadas", sesionesEstimadas);
                    cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new E_DisponibilidadCitas
                            {
                                IdArtista = dr["IdArtista"] as int?,
                                IdHorarioArtista = dr["IdHorarioArtista"] as int?, // Incluir IdHorarioArtista
                                Fecha = Convert.ToDateTime(dr["Fecha"]),
                                HoraInicio = TimeSpan.Parse(dr["HoraInicio"].ToString()),
                                HoraFin = TimeSpan.Parse(dr["HoraFin"].ToString()),
                                Estado = dr["Estado"].ToString()
                            });
                        }
                    }
                    mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                mensaje = $"Error: {ex.Message}. Trace: {ex.StackTrace}";
            }

            return lista;
        }


        // Método para listar citas
        public List<E_Citas> ListarCitas(int? idCliente, int? idArtista, out string mensaje)
        {
            List<E_Citas> lista = new List<E_Citas>();
            mensaje = string.Empty;

            try
            {
                using (SqlConnection conn = ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_CRUDCitas", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Accion", 2); // Acción de consulta
                    cmd.Parameters.AddWithValue("@IdCliente", (object)idCliente ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@IdArtista", (object)idArtista ?? DBNull.Value);
                    cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new E_Citas
                            {
                                IdCita = Convert.ToInt32(dr["IdCita"]),
                                IdCliente = Convert.ToInt32(dr["IdCliente"]),
                                IdArtista = dr["IdArtista"] as int?,
                                IdHorarioArtista = dr["IdHorarioArtista"] as int?, // Nuevo campo
                                Fecha = Convert.ToDateTime(dr["Fecha"]),
                                HoraInicio = TimeSpan.Parse(dr["HoraInicio"].ToString()),
                                HoraFin = TimeSpan.Parse(dr["HoraFin"].ToString()),
                                Descripcion = dr["Descripcion"].ToString(),
                                IdFormulario = dr["IdFormulario"] as int?,
                                Estado = dr["Estado"].ToString(),
                                Token = dr["Token"] as int?
                            });
                        }
                    }
                    mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                mensaje = $"Error: {ex.Message}. Trace: {ex.StackTrace}";
            }

            return lista;
        }

        // Método para registrar una cita y bloquear el horario
        public bool RegistrarCita(E_Citas cita, out string mensaje)
        {
            mensaje = string.Empty;
            bool resultado = false;

            try
            {
                using (SqlConnection conn = ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_CRUDCitas", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Accion", 1); // Acción de registro
                    cmd.Parameters.AddWithValue("@IdCliente", cita.IdCliente);
                    cmd.Parameters.AddWithValue("@IdArtista", cita.IdArtista);
                    cmd.Parameters.AddWithValue("@IdHorarioArtista", cita.IdHorarioArtista ?? (object)DBNull.Value); // Nuevo campo
                    cmd.Parameters.AddWithValue("@Fecha", cita.Fecha);
                    cmd.Parameters.AddWithValue("@HoraInicio", cita.HoraInicio);
                    cmd.Parameters.AddWithValue("@HoraFin", cita.HoraFin);
                    cmd.Parameters.AddWithValue("@Descripcion", cita.Descripcion ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@IdFormulario", (object)cita.IdFormulario ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Estado", cita.Estado);
                    cmd.Parameters.AddWithValue("@Token", (object)cita.Token ?? DBNull.Value);
                    cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();

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

        // Método para listar artistas
        public List<E_Artista> ListarArtistas(out string mensaje)
        {
            List<E_Artista> lista = new List<E_Artista>();
            mensaje = string.Empty;

            try
            {
                using (SqlConnection conn = ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_CRUDCitas", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Accion", 6); // Acción para listar artistas
                    cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new E_Artista
                            {
                                IdArtista = Convert.ToInt32(dr["IdArtista"]),
                                Nombre = dr["Nombre"].ToString(),
                                Apellidos = dr["Apellidos"].ToString(),
                                Telefono = dr["Telefono"].ToString(),
                                Correo = dr["Correo"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"])
                            });
                        }
                    }
                    mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                mensaje = $"Error: {ex.Message}. Trace: {ex.StackTrace}";
            }

            return lista;
        }

        // Método para actualizar una cita
        public bool ActualizarCita(E_Citas cita, out string mensaje)
        {
            mensaje = string.Empty;
            bool resultado = false;

            try
            {
                using (SqlConnection conn = ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_CRUDCitas", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Accion", 3); // Acción de actualización
                    cmd.Parameters.AddWithValue("@IdCita", cita.IdCita);
                    cmd.Parameters.AddWithValue("@IdCliente", cita.IdCliente);
                    cmd.Parameters.AddWithValue("@IdArtista", cita.IdArtista);
                    cmd.Parameters.AddWithValue("@IdHorarioArtista", cita.IdHorarioArtista ?? (object)DBNull.Value); // Nuevo campo
                    cmd.Parameters.AddWithValue("@Fecha", cita.Fecha);
                    cmd.Parameters.AddWithValue("@HoraInicio", cita.HoraInicio);
                    cmd.Parameters.AddWithValue("@HoraFin", cita.HoraFin);
                    cmd.Parameters.AddWithValue("@Descripcion", cita.Descripcion ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@IdFormulario", (object)cita.IdFormulario ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Estado", cita.Estado);
                    cmd.Parameters.AddWithValue("@Token", (object)cita.Token ?? DBNull.Value);
                    cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();

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

        // Método para eliminar una cita
        public bool EliminarCita(int idCita, out string mensaje)
        {
            mensaje = string.Empty;
            bool resultado = false;

            try
            {
                using (SqlConnection conn = ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_CRUDCitas", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Accion", 4); // Acción de eliminación
                    cmd.Parameters.AddWithValue("@IdCita", idCita);
                    cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
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
    }
}

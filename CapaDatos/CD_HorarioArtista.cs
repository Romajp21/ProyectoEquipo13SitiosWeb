using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_HorarioArtista
    {
        private SqlConnection ObtenerConexion()
        {
            SqlConnection conn = new SqlConnection(Conexion.cn);
            conn.Open();
            return conn;
        }

        // Listar Horarios
        public List<E_HorarioArtista> ListarHorarios(int idArtista, out string mensaje)
        {
            List<E_HorarioArtista> lista = new List<E_HorarioArtista>();
            mensaje = string.Empty;

            try
            {
                using (SqlConnection conn = ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_CRUDHorarioArtista", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Accion", 2);
                    cmd.Parameters.AddWithValue("@IdArtista", (object)idArtista ?? DBNull.Value);
                    cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new E_HorarioArtista
                            {
                                IdHorarioArtista = Convert.ToInt32(dr["IdHorarioArtista"]),
                                IdArtista = Convert.ToInt32(dr["IdArtista"]),
                                Fecha = Convert.ToDateTime(dr["Fecha"]),
                                HoraInicio = TimeSpan.Parse(dr["HoraInicio"].ToString()),
                                HoraFin = TimeSpan.Parse(dr["HoraFin"].ToString()),
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

        // Registrar Horario
        public bool RegistrarHorario(E_HorarioArtista horario, out string mensaje)
        {
            mensaje = string.Empty;
            bool resultado = false;

            try
            {
                using (SqlConnection conn = ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_CRUDHorarioArtista", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Accion", 1);
                    cmd.Parameters.AddWithValue("@IdArtista", horario.IdArtista);
                    cmd.Parameters.AddWithValue("@Fecha", horario.Fecha);
                    cmd.Parameters.AddWithValue("@HoraInicio", horario.HoraInicio);
                    cmd.Parameters.AddWithValue("@HoraFin", horario.HoraFin);
                    cmd.Parameters.AddWithValue("@Estado", horario.Estado);
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

        // Actualizar Horario
        public bool ActualizarHorario(E_HorarioArtista horario, out string mensaje)
        {
            mensaje = string.Empty;
            bool resultado = false;

            try
            {
                using (SqlConnection conn = ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_CRUDHorarioArtista", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Accion", 3);
                    cmd.Parameters.AddWithValue("@IdHorarioArtista", horario.IdHorarioArtista);
                    cmd.Parameters.AddWithValue("@IdArtista", horario.IdArtista);
                    cmd.Parameters.AddWithValue("@Fecha", horario.Fecha);
                    cmd.Parameters.AddWithValue("@HoraInicio", horario.HoraInicio);
                    cmd.Parameters.AddWithValue("@HoraFin", horario.HoraFin);
                    cmd.Parameters.AddWithValue("@Estado", horario.Estado);
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

        // Obtener un horario por ID
        public E_HorarioArtista ObtenerHorarioPorId(int idHorarioArtista, out string mensaje)
        {
            mensaje = string.Empty;
            E_HorarioArtista horario = null;

            try
            {
                using (SqlConnection conn = ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_CRUDHorarioArtista", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Accion", 5); // Acción para leer
                    cmd.Parameters.AddWithValue("@IdHorarioArtista", idHorarioArtista);
                    cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            horario = new E_HorarioArtista
                            {
                                IdHorarioArtista = Convert.ToInt32(dr["IdHorarioArtista"]),
                                IdArtista = Convert.ToInt32(dr["IdArtista"]),
                                Fecha = Convert.ToDateTime(dr["Fecha"]),
                                HoraInicio = TimeSpan.Parse(dr["HoraInicio"].ToString()),
                                HoraFin = TimeSpan.Parse(dr["HoraFin"].ToString()),
                                Estado = Convert.ToBoolean(dr["Estado"])
                            };
                        }
                    }

                    mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                mensaje = $"Error: {ex.Message}. Trace: {ex.StackTrace}";
            }

            return horario;
        }

        // Eliminar Horario
        public bool EliminarHorario(int idHorarioArtista, out string mensaje)
        {
            mensaje = string.Empty;
            bool resultado = false;

            try
            {
                using (SqlConnection conn = ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_CRUDHorarioArtista", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Accion", 4); // Acción de eliminar
                    cmd.Parameters.AddWithValue("@IdHorarioArtista", idHorarioArtista);
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

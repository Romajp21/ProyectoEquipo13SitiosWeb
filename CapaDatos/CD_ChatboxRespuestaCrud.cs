using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_ChatboxRespuestaCrud
    {
        public List<E_ChatboxRespuestaCrud> ListarRespuestas(out string mensaje)
        {
            mensaje = string.Empty;
            var respuestas = new List<E_ChatboxRespuestaCrud>();

            try
            {
                using (SqlConnection conn = new SqlConnection(Conexion.cn))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_ChatboxRespuestas", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Action", 1); // Acción: Listar todas las respuestas
                        cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@Mensaje", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                respuestas.Add(new E_ChatboxRespuestaCrud
                                {
                                    IdRespuesta = Convert.ToInt32(dr["IdRespuesta"]),
                                    TextoRespuesta = dr["TextoRespuesta"].ToString(),
                                    IdPregunta = Convert.ToInt32(dr["IdPregunta"]),
                                    TextoPregunta = dr["TextoPregunta"].ToString(),
                                    IdPreguntaDestino = dr["IdPreguntaDestino"] as int?,
                                    Orden = Convert.ToInt32(dr["Orden"]),
                                    Estado = Convert.ToBoolean(dr["Estado"])
                                });
                            }
                        }

                        mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = $"Error: {ex.Message}";
            }

            return respuestas;
        }

        public List<E_ChatboxRespuestaCrud> ObtenerRespuestasPorPregunta(int idPregunta, out string mensaje)
        {
            mensaje = string.Empty;
            var respuestas = new List<E_ChatboxRespuestaCrud>();

            try
            {
                using (SqlConnection conn = new SqlConnection(Conexion.cn))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_ChatboxRespuestas", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Action", 2); // Acción: Obtener respuestas de una pregunta
                        cmd.Parameters.AddWithValue("@IdPregunta", idPregunta);
                        cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@Mensaje", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                respuestas.Add(new E_ChatboxRespuestaCrud
                                {
                                    IdRespuesta = Convert.ToInt32(dr["IdRespuesta"]),
                                    TextoRespuesta = dr["TextoRespuesta"].ToString(),
                                    IdPreguntaDestino = dr["IdPreguntaDestino"] as int?,
                                    Orden = Convert.ToInt32(dr["Orden"]),
                                    Estado = Convert.ToBoolean(dr["Estado"])
                                });
                            }
                        }

                        mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = $"Error: {ex.Message}";
            }

            return respuestas;
        }

        public E_ChatboxRespuestaCrud ObtenerRespuestaPorId(int idRespuesta, out string mensaje)
        {
            mensaje = string.Empty;
            E_ChatboxRespuestaCrud respuesta = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(Conexion.cn))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_ChatboxRespuestas", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Action", 3); // Acción: Obtener respuesta por ID
                        cmd.Parameters.AddWithValue("@IdRespuesta", idRespuesta);
                        cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@Mensaje", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                respuesta = new E_ChatboxRespuestaCrud
                                {
                                    IdRespuesta = Convert.ToInt32(dr["IdRespuesta"]),
                                    TextoRespuesta = dr["TextoRespuesta"].ToString(),
                                    IdPregunta = Convert.ToInt32(dr["IdPregunta"]),
                                    IdPreguntaDestino = dr["IdPreguntaDestino"] as int?,
                                    Orden = Convert.ToInt32(dr["Orden"]),
                                    Estado = Convert.ToBoolean(dr["Estado"])
                                };
                            }
                        }

                        mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = $"Error: {ex.Message}";
            }

            return respuesta;
        }

        public bool InsertarRespuesta(E_ChatboxRespuestaCrud respuesta, out string mensaje)
        {
            mensaje = string.Empty;

            try
            {
                using (SqlConnection conn = new SqlConnection(Conexion.cn))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_ChatboxRespuestas", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Action", 4); // Acción: Insertar respuesta
                        cmd.Parameters.AddWithValue("@IdPregunta", respuesta.IdPregunta);
                        cmd.Parameters.AddWithValue("@TextoRespuesta", respuesta.TextoRespuesta);
                        cmd.Parameters.AddWithValue("@IdPreguntaDestino", respuesta.IdPreguntaDestino ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Orden", respuesta.Orden);
                        cmd.Parameters.AddWithValue("@Estado", respuesta.Estado);
                        cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@Mensaje", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();
                        mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
                        return Convert.ToBoolean(cmd.Parameters["@Resultado"].Value);
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = $"Error: {ex.Message}";
                return false;
            }
        }

        public bool ActualizarRespuesta(E_ChatboxRespuestaCrud respuesta, out string mensaje)
        {
            mensaje = string.Empty;

            try
            {
                using (SqlConnection conn = new SqlConnection(Conexion.cn))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_ChatboxRespuestas", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Action", 5); // Acción: Actualizar respuesta
                        cmd.Parameters.AddWithValue("@IdRespuesta", respuesta.IdRespuesta);
                        cmd.Parameters.AddWithValue("@IdPregunta", respuesta.IdPregunta);
                        cmd.Parameters.AddWithValue("@TextoRespuesta", respuesta.TextoRespuesta);
                        cmd.Parameters.AddWithValue("@IdPreguntaDestino", respuesta.IdPreguntaDestino ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Orden", respuesta.Orden);
                        cmd.Parameters.AddWithValue("@Estado", respuesta.Estado);
                        cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@Mensaje", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();
                        mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
                        return Convert.ToBoolean(cmd.Parameters["@Resultado"].Value);
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = $"Error: {ex.Message}";
                return false;
            }
        }

        public bool EliminarRespuesta(int idRespuesta, out string mensaje)
        {
            mensaje = string.Empty;

            try
            {
                using (SqlConnection conn = new SqlConnection(Conexion.cn))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_ChatboxRespuestas", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Action", 6); // Acción: Eliminar respuesta
                        cmd.Parameters.AddWithValue("@IdRespuesta", idRespuesta);
                        cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@Mensaje", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();
                        mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
                        return Convert.ToBoolean(cmd.Parameters["@Resultado"].Value);
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = $"Error: {ex.Message}";
                return false;
            }
        }
    }
}

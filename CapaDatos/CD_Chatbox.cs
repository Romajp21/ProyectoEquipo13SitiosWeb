using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_Chatbox
    {
        // Obtener preguntas iniciales con respuestas
        public List<E_Chatbox> ObtenerPreguntasIniciales(out string mensaje)
        {
            mensaje = string.Empty;
            List<E_Chatbox> preguntas = new List<E_Chatbox>();

            try
            {
                using (SqlConnection conn = new SqlConnection(Conexion.cn))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_ChatboxManager", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Action", 1); // Acción para obtener preguntas iniciales
                        cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@Mensaje", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                var pregunta = new E_Chatbox
                                {
                                    IdPregunta = Convert.ToInt32(dr["IdPregunta"]),
                                    TextoPregunta = dr["TextoPregunta"].ToString(),
                                    Respuestas = new List<E_ChatboxRespuesta>()
                                };

                                // Leer respuestas asociadas
                                while (dr.Read())
                                {
                                    pregunta.Respuestas.Add(new E_ChatboxRespuesta
                                    {
                                        IdRespuesta = Convert.ToInt32(dr["IdRespuesta"]),
                                        TextoRespuesta = dr["TextoRespuesta"].ToString(),
                                        IdPreguntaDestino = dr["IdPreguntaDestino"] != DBNull.Value ? (int?)Convert.ToInt32(dr["IdPreguntaDestino"]) : null,
                                        Orden = Convert.ToInt32(dr["Orden"])
                                    });
                                }

                                preguntas.Add(pregunta);
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

            return preguntas;
        }

        // Obtener respuestas asociadas a una pregunta específica
        public List<E_ChatboxRespuesta> ObtenerRespuestasPorPregunta(int idPregunta, out string mensaje)
        {
            mensaje = string.Empty;
            List<E_ChatboxRespuesta> respuestas = new List<E_ChatboxRespuesta>();

            try
            {
                using (SqlConnection conn = new SqlConnection(Conexion.cn))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_ChatboxManager", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Action", 2); // Acción para obtener respuestas
                        cmd.Parameters.AddWithValue("@IdPregunta", idPregunta);
                        cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@Mensaje", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                respuestas.Add(new E_ChatboxRespuesta
                                {
                                    IdRespuesta = Convert.ToInt32(dr["IdRespuesta"]),
                                    TextoRespuesta = dr["TextoRespuesta"].ToString(),
                                    IdPreguntaDestino = dr["IdPreguntaDestino"] != DBNull.Value ? (int?)Convert.ToInt32(dr["IdPreguntaDestino"]) : null,
                                    Orden = Convert.ToInt32(dr["Orden"])
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

        // Obtener una pregunta específica con sus respuestas
        public E_Chatbox ObtenerPreguntaPorId(int idPregunta, out string mensaje)
        {
            mensaje = string.Empty;
            E_Chatbox pregunta = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(Conexion.cn))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_ChatboxManager", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Action", 3); // Nueva acción para obtener pregunta específica
                        cmd.Parameters.AddWithValue("@IdPregunta", idPregunta);
                        cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@Mensaje", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            // Leer la pregunta seleccionada
                            if (dr.Read())
                            {
                                pregunta = new E_Chatbox
                                {
                                    IdPregunta = dr.GetInt32(dr.GetOrdinal("IdPregunta")),
                                    TextoPregunta = dr["TextoPregunta"].ToString(),
                                    EstadoPregunta = dr.GetBoolean(dr.GetOrdinal("Estado")),
                                    Respuestas = new List<E_ChatboxRespuesta>()
                                };
                            }

                            // Leer respuestas asociadas
                            if (dr.NextResult())
                            {
                                while (dr.Read())
                                {
                                    pregunta?.Respuestas.Add(new E_ChatboxRespuesta
                                    {
                                        IdRespuesta = dr.GetInt32(dr.GetOrdinal("IdRespuesta")),
                                        TextoRespuesta = dr["TextoRespuesta"].ToString(),
                                        IdPreguntaDestino = dr["IdPreguntaDestino"] != DBNull.Value
                                            ? dr.GetInt32(dr.GetOrdinal("IdPreguntaDestino"))
                                            : (int?)null,
                                        Orden = dr.GetInt32(dr.GetOrdinal("Orden")),
                                        EstadoRespuesta = dr.GetBoolean(dr.GetOrdinal("Estado"))
                                    });
                                }
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

            return pregunta;
        }
    }

}


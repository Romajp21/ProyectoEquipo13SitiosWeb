using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_ChatboxPreguntaCrud
    {
        public List<E_ChatboxPreguntaCrud> ListarPreguntas(out string mensaje)
        {
            mensaje = string.Empty;
            var preguntas = new List<E_ChatboxPreguntaCrud>();

            try
            {
                using (SqlConnection conn = new SqlConnection(Conexion.cn))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_ChatboxPreguntas", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Action", 1); // Acción: Listar preguntas
                        cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@Mensaje", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                preguntas.Add(new E_ChatboxPreguntaCrud
                                {
                                    IdPregunta = Convert.ToInt32(dr["IdPregunta"]),
                                    TextoPregunta = dr["TextoPregunta"].ToString(),
                                    EstadoPregunta = Convert.ToBoolean(dr["EstadoPregunta"]) // Corregido para interpretar correctamente
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

            return preguntas;
        }

        public bool InsertarPregunta(E_ChatboxPreguntaCrud pregunta, out string mensaje)
        {
            mensaje = string.Empty;

            try
            {
                using (SqlConnection conn = new SqlConnection(Conexion.cn))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_ChatboxPreguntas", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Action", 2); // Acción: Insertar pregunta
                        cmd.Parameters.AddWithValue("@TextoPregunta", pregunta.TextoPregunta);
                        cmd.Parameters.AddWithValue("@EstadoPregunta", pregunta.EstadoPregunta);
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

        public bool ActualizarPregunta(E_ChatboxPreguntaCrud pregunta, out string mensaje)
        {
            mensaje = string.Empty;

            try
            {
                using (SqlConnection conn = new SqlConnection(Conexion.cn))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_ChatboxPreguntas", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Action", 3); // Acción: Actualizar pregunta
                        cmd.Parameters.AddWithValue("@IdPregunta", pregunta.IdPregunta);
                        cmd.Parameters.AddWithValue("@TextoPregunta", pregunta.TextoPregunta);
                        cmd.Parameters.AddWithValue("@EstadoPregunta", pregunta.EstadoPregunta);
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

        public bool EliminarPregunta(int idPregunta, out string mensaje)
        {
            mensaje = string.Empty;

            try
            {
                using (SqlConnection conn = new SqlConnection(Conexion.cn))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_ChatboxPreguntas", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Action", 4); // Acción: Eliminar pregunta
                        cmd.Parameters.AddWithValue("@IdPregunta", idPregunta);
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

        public E_ChatboxPreguntaCrud ObtenerPreguntaPorId(int idPregunta, out string mensaje)
        {
            mensaje = string.Empty;
            E_ChatboxPreguntaCrud pregunta = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(Conexion.cn))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_ChatboxPreguntas", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Action", 5); // Acción para obtener una pregunta específica
                        cmd.Parameters.AddWithValue("@IdPregunta", idPregunta);
                        cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@Mensaje", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                pregunta = new E_ChatboxPreguntaCrud
                                {
                                    IdPregunta = Convert.ToInt32(dr["IdPregunta"]),
                                    TextoPregunta = dr["TextoPregunta"].ToString(),
                                    EstadoPregunta = Convert.ToBoolean(dr["EstadoPregunta"])
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

            return pregunta;
        }
    }
}
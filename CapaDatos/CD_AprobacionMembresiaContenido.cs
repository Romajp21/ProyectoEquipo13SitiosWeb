using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_AprobacionMembresiaContenido
    {
        // Método para registrar una nueva aprobación
        public bool RegistrarAprobacion(E_AprobacionMembresiaContenido aprobacion, out string mensaje)
        {
            mensaje = string.Empty;
            bool resultado = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_CRUDAprobacionMembresiaContenido", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Accion", 1); // Acción para crear
                    cmd.Parameters.AddWithValue("@IdMembresContenido", aprobacion.IdMembresContenido);
                    cmd.Parameters.AddWithValue("@Estado", aprobacion.Estado);
                    cmd.Parameters.AddWithValue("@Comentario", aprobacion.Comentario);
                    cmd.Parameters.AddWithValue("@AprobadoPor", aprobacion.AprobadoPor);

                    cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["@Resultado"].Value);
                    mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                mensaje = "Error: " + ex.Message;
            }

            return resultado;
        }

        // Método para listar aprobaciones
        public List<E_AprobacionMembresiaContenido> ListarAprobaciones(int? idMembresia, int? idMembresContenido, out string mensaje)
        {
            List<E_AprobacionMembresiaContenido> lista = new List<E_AprobacionMembresiaContenido>();
            mensaje = string.Empty;

            try
            {
                using (SqlConnection conn = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_CRUDAprobacionMembresiaContenido", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Accion", 2); // Acción para leer
                    if (idMembresia.HasValue)
                        cmd.Parameters.AddWithValue("@IdMembresia", idMembresia.Value);
                    if (idMembresContenido.HasValue)
                        cmd.Parameters.AddWithValue("@IdMembresContenido", idMembresContenido.Value);

                    cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                    conn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var aprobacion = new E_AprobacionMembresiaContenido
                            {
                                IdAprobacion = dr["IdAprobacion"] != DBNull.Value ? Convert.ToInt32(dr["IdAprobacion"]) : 0,
                                IdMembresia = dr["IdMembresia"] != DBNull.Value ? Convert.ToInt32(dr["IdMembresia"]) : 0,
                                IdMembresContenido = dr["IdMembresContenido"] != DBNull.Value ? Convert.ToInt32(dr["IdMembresContenido"]) : 0,
                                NombreMembresia = dr["NombreMembresia"] != DBNull.Value ? dr["NombreMembresia"].ToString() : string.Empty,
                                NombreContenido = dr["NombreContenido"] != DBNull.Value ? dr["NombreContenido"].ToString() : string.Empty,
                                ComentarioAsistente = dr["ComentarioAsistente"] != DBNull.Value ? dr["ComentarioAsistente"].ToString() : string.Empty,
                                Estado = dr["Estado"] != DBNull.Value ? Convert.ToBoolean(dr["Estado"]) : false,
                                Comentario = dr["Comentario"] != DBNull.Value ? dr["Comentario"].ToString() : string.Empty,
                                AprobadoPor = dr["AprobadoPor"] != DBNull.Value ? Convert.ToInt32(dr["AprobadoPor"]) : 0,
                                FechaAprobacion = dr["FechaAprobacion"] != DBNull.Value ? Convert.ToDateTime(dr["FechaAprobacion"]) : (DateTime?)null
                            };
                            lista.Add(aprobacion);
                        }
                    }

                    mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                mensaje = "Error: " + ex.Message;
            }

            return lista;
        }

        // Método para actualizar una aprobación existente
        public bool ActualizarAprobacion(E_AprobacionMembresiaContenido aprobacion, out string mensaje)
        {
            mensaje = string.Empty;
            bool resultado = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_CRUDAprobacionMembresiaContenido", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Accion", 3); // Acción para actualizar
                    cmd.Parameters.AddWithValue("@IdAprobacion", aprobacion.IdAprobacion);
                    cmd.Parameters.AddWithValue("@Estado", aprobacion.Estado);
                    cmd.Parameters.AddWithValue("@Comentario", aprobacion.Comentario);
                    cmd.Parameters.AddWithValue("@AprobadoPor", aprobacion.AprobadoPor);

                    cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["@Resultado"].Value);
                    mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                mensaje = "Error: " + ex.Message;
            }

            return resultado;
        }

        // Método para eliminar una aprobación
        public bool EliminarAprobacion(int idAprobacion, out string mensaje)
        {
            mensaje = string.Empty;
            bool resultado = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_CRUDAprobacionMembresiaContenido", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Accion", 4); // Acción para eliminar
                    cmd.Parameters.AddWithValue("@IdAprobacion", idAprobacion);

                    cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["@Resultado"].Value);
                    mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                mensaje = "Error: " + ex.Message;
            }

            return resultado;
        }
    }
}

using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_MembresiaContenido
    {
        // Método para listar contenidos de membresías
        public List<E_MembresiaContenido> ListarContenido(int? idMembresia, out string mensaje)
        {
            List<E_MembresiaContenido> lista = new List<E_MembresiaContenido>();
            mensaje = string.Empty;

            try
            {
                using (SqlConnection conn = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_CRUDMembresiaContenido", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Accion", 2);
                    if (idMembresia.HasValue)
                        cmd.Parameters.AddWithValue("@IdMembresia", idMembresia.Value);

                    cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                    conn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new E_MembresiaContenido
                            {
                                IdMembresContenido = Convert.ToInt32(dr["IdMembresContenido"]),
                                IdMembresia = Convert.ToInt32(dr["IdMembresia"]),
                                NombreMembresia = dr["NombreMembresia"].ToString(),
                                NombreContenido = dr["NombreContenido"].ToString(),
                                CreadoPor = Convert.ToInt32(dr["CreadoPor"]),
                                Comentario = dr["Comentario"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"]) // Incluye el campo Estado
                            });
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

        // Método para agregar contenido a una membresía
        public bool RegistrarContenido(E_MembresiaContenido contenido, out string mensaje)
        {
            mensaje = string.Empty;
            bool resultado = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_CRUDMembresiaContenido", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Accion", 1);
                    cmd.Parameters.AddWithValue("@IdMembresia", contenido.IdMembresia);
                    cmd.Parameters.AddWithValue("@NombreContenido", contenido.NombreContenido);
                    cmd.Parameters.AddWithValue("@CreadoPor", contenido.CreadoPor);
                    cmd.Parameters.AddWithValue("@Comentario", contenido.Comentario);
                    cmd.Parameters.AddWithValue("@Estado", contenido.Estado); // Incluye el Estado
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

        // Método para actualizar contenido de una membresía
        public bool ActualizarContenido(E_MembresiaContenido contenido, out string mensaje)
        {
            mensaje = string.Empty;
            bool resultado = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_CRUDMembresiaContenido", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Accion", 3);
                    cmd.Parameters.AddWithValue("@IdMembresContenido", contenido.IdMembresContenido);
                    cmd.Parameters.AddWithValue("@NombreContenido", contenido.NombreContenido);
                    cmd.Parameters.AddWithValue("@Comentario", contenido.Comentario);
                    cmd.Parameters.AddWithValue("@Estado", contenido.Estado); // Incluye el Estado
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

        // Método para eliminar contenido de una membresía
        public bool EliminarContenido(int idMembresContenido, out string mensaje)
        {
            mensaje = string.Empty;
            bool resultado = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_CRUDMembresiaContenido", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Accion", 4);
                    cmd.Parameters.AddWithValue("@IdMembresContenido", idMembresContenido);
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

using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_Testimonio
    {
        // Método para obtener y abrir la conexión
        private SqlConnection ObtenerConexion()
        {
            SqlConnection conn = new SqlConnection(Conexion.cn);
            conn.Open();
            return conn;
        }

        // Listar citas sin testimonios para un cliente
        public List<E_Citas> ListarCitasSinTestimonios(int idCliente, out string mensaje)
        {
            List<E_Citas> lista = new List<E_Citas>();
            mensaje = string.Empty;

            try
            {
                using (SqlConnection conn = ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_CRUDTestimonios", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Accion", 2);
                    cmd.Parameters.AddWithValue("@IdCliente", idCliente);
                    cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new E_Citas
                            {
                                IdCita = Convert.ToInt32(dr["IdCita"]),
                                Fecha = Convert.ToDateTime(dr["Fecha"]),
                                Descripcion = dr["Descripcion"].ToString()
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


        // Registrar un nuevo testimonio
        public bool RegistrarTestimonio(E_Testimonio testimonio, out string mensaje)
        {
            mensaje = string.Empty;
            bool resultado = false;

            try
            {
                using (SqlConnection conn = ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_CRUDTestimonios", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Accion", 1);
                    cmd.Parameters.AddWithValue("@IdCliente", testimonio.IdCliente);
                    cmd.Parameters.AddWithValue("@IdCita", testimonio.IdCita);
                    cmd.Parameters.AddWithValue("@Testimonio", testimonio.Testimonio);
                    cmd.Parameters.AddWithValue("@Calificacion", testimonio.Calificacion);
                    cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();

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

        // Aprobar o rechazar un testimonio
        public bool AprobarTestimonio(int idTestimonio, bool estado, int revisadoPor, out string mensaje)
        {
            mensaje = string.Empty;
            bool resultado = false;

            try
            {
                using (SqlConnection conn = ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_CRUDTestimonios", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Accion", 3);
                    cmd.Parameters.AddWithValue("@IdTestimonio", idTestimonio);
                    cmd.Parameters.AddWithValue("@Estado", estado);
                    cmd.Parameters.AddWithValue("@RevisadoPor", revisadoPor);
                    cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();

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

        public List<E_Testimonio> ListarTestimoniosPendientes(out string mensaje)
        {
            List<E_Testimonio> lista = new List<E_Testimonio>();
            mensaje = string.Empty;

            try
            {
                using (SqlConnection conn = ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_CRUDTestimonios", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Accion", 4); // Acción 4: Obtener testimonios pendientes
                    cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new E_Testimonio
                            {
                                IdTestimonio = Convert.ToInt32(dr["IdTestimonio"]),
                                IdCita = Convert.ToInt32(dr["IdCita"]),
                                Cliente = dr["Cliente"].ToString(), // Nombre completo del cliente
                                Artista = dr["Artista"].ToString(), // Nombre completo del artista
                                Calificacion = Convert.ToInt32(dr["Calificacion"]),
                                RevisadoPor = dr["RevisadoPor"] != DBNull.Value ? Convert.ToInt32(dr["RevisadoPor"]) : null,
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

        public bool AprobarRechazarTestimonio(int idTestimonio, int revisadoPor, bool estado, out string mensaje)
        {
            mensaje = string.Empty;
            bool resultado = false;

            try
            {
                using (SqlConnection conn = ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_CRUDTestimonios", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Accion", 3);
                    cmd.Parameters.AddWithValue("@IdTestimonio", idTestimonio);
                    cmd.Parameters.AddWithValue("@RevisadoPor", revisadoPor);
                    cmd.Parameters.AddWithValue("@Estado", estado);
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

        public List<E_Testimonio> ListarTestimoniosAprobados(int cantidad, out string mensaje)
        {
            List<E_Testimonio> lista = new List<E_Testimonio>();
            mensaje = string.Empty;

            try
            {
                using (SqlConnection conn = ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_CRUDTestimonios", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Accion", 5);
                    cmd.Parameters.AddWithValue("@Cantidad", cantidad);
                    cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new E_Testimonio
                            {
                                IdTestimonio = Convert.ToInt32(dr["IdTestimonio"]),
                                Cliente = dr["Cliente"].ToString(),
                                Testimonio = dr["Testimonio"].ToString(),
                                Calificacion = Convert.ToInt32(dr["Calificacion"]),
                                FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"])
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
    }
}

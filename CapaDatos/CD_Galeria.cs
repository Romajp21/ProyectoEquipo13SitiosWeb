using CapaEntidades;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System;

namespace CapaDatos
{
    public class CD_Galeria
    {
        // Método para centralizar la obtención y apertura de la conexión
        private SqlConnection ObtenerConexion()
        {
            SqlConnection conn = new SqlConnection(Conexion.cn);
            conn.Open();
            return conn;
        }

        // Método para listar todas las imágenes de la galería
        public List<E_Galeria> ListarGaleria(out string mensaje)
        {
            List<E_Galeria> lista = new List<E_Galeria>();
            mensaje = string.Empty;

            try
            {
                using (SqlConnection conn = ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_CRUDGaleria", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Accion", 2);
                    cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new E_Galeria
                            {
                                IdImagen = Convert.ToInt32(dr["IdImagen"]),
                                IdArtista = Convert.ToInt32(dr["IdArtista"]),
                                DescripcionTatuaje = dr["DescripcionTatuaje"].ToString(),
                                RutaImagen = dr["RutaImagen"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"]),
                                Fecha = Convert.ToDateTime(dr["Fecha"])
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

        // Método para registrar una nueva imagen en la galería
        public bool RegistrarImagen(E_Galeria galeria, out string mensaje)
        {
            mensaje = string.Empty;
            bool resultado = false;

            try
            {
                using (SqlConnection conn = ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_CRUDGaleria", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Accion", 1);
                    cmd.Parameters.AddWithValue("@IdArtista", galeria.IdArtista);
                    cmd.Parameters.AddWithValue("@DescripcionTatuaje", galeria.DescripcionTatuaje);
                    //cmd.Parameters.AddWithValue("@RutaImagen", galeria.RutaImagen);
                    cmd.Parameters.AddWithValue("@RutaImagen", galeria.RutaImagen); // Base64 de la imagen

                    cmd.Parameters.AddWithValue("@Estado", galeria.Estado);
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

        // Método para actualizar una imagen en la galería
        public bool ActualizarImagen(E_Galeria galeria, out string mensaje)
        {
            mensaje = string.Empty;
            bool resultado = false;

            try
            {
                using (SqlConnection conn = ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_CRUDGaleria", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Accion", 3);
                    cmd.Parameters.AddWithValue("@IdImagen", galeria.IdImagen);
                    cmd.Parameters.AddWithValue("@IdArtista", galeria.IdArtista);
                    cmd.Parameters.AddWithValue("@DescripcionTatuaje", galeria.DescripcionTatuaje);
                    //cmd.Parameters.AddWithValue("@RutaImagen", galeria.RutaImagen);
                    cmd.Parameters.AddWithValue("@RutaImagen", galeria.RutaImagen); // Base64 de la imagen

                    cmd.Parameters.AddWithValue("@Estado", galeria.Estado);
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

        // Método para eliminar una imagen de la galería
        public bool EliminarImagen(int idImagen, out string mensaje)
        {
            mensaje = string.Empty;
            bool resultado = false;

            try
            {
                using (SqlConnection conn = ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_CRUDGaleria", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Accion", 4);
                    cmd.Parameters.AddWithValue("@IdImagen", idImagen);
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


        public List<E_Galeria> ListarGaleriaActivas(out string mensaje)
        {
            List<E_Galeria> lista = new List<E_Galeria>();
            mensaje = string.Empty;

            try
            {
                using (SqlConnection conn = ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_CRUDGaleria", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Accion", 2);
                    cmd.Parameters.AddWithValue("@Estado", 1); // Solo imágenes activas
                    cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new E_Galeria
                            {
                                IdImagen = Convert.ToInt32(dr["IdImagen"]),
                                IdArtista = Convert.ToInt32(dr["IdArtista"]),
                                DescripcionTatuaje = dr["DescripcionTatuaje"].ToString(),
                                RutaImagen = dr["RutaImagen"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"]),
                                Fecha = Convert.ToDateTime(dr["Fecha"])
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


        public E_Galeria ObtenerImagenPorDescripcion(string descripcion, out string mensaje)
        {
            mensaje = string.Empty;
            E_Galeria imagen = null;

            try
            {
                using (SqlConnection conn = ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_CRUDGaleria", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Accion", 6);
                    cmd.Parameters.AddWithValue("@DescripcionTatuaje", descripcion);
                    cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            imagen = new E_Galeria
                            {
                                IdImagen = Convert.ToInt32(dr["IdImagen"]),
                                IdArtista = Convert.ToInt32(dr["IdArtista"]),
                                DescripcionTatuaje = dr["DescripcionTatuaje"].ToString(),
                                RutaImagen = dr["RutaImagen"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"]),
                                Fecha = Convert.ToDateTime(dr["Fecha"])
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

            return imagen;
        }



    }
}

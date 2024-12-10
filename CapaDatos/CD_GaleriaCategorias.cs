using CapaEntidades;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System;

namespace CapaDatos
{
    public class CD_GaleriaCategorias
    {
        private SqlConnection ObtenerConexion()
        {
            SqlConnection conn = new SqlConnection(Conexion.cn);
            conn.Open();
            return conn;
        }

        public List<E_GaleriaCategorias> ListarGaleriaCategorias(out string mensaje)
        {
            List<E_GaleriaCategorias> lista = new List<E_GaleriaCategorias>();
            mensaje = string.Empty;

            try
            {
                using (SqlConnection conn = ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_CRUDGaleriaCategorias", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Accion", 2);
                    cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new E_GaleriaCategorias
                            {
                                IdImagen = Convert.ToInt32(dr["IdImagen"]),
                                IdCategoriaTatuajes = Convert.ToInt32(dr["IdCategoriaTatuajes"]),
                                Estado = Convert.ToBoolean(dr["Estado"]),
                                DescripcionTatuaje = dr["DescripcionTatuaje"].ToString(), // Nuevo campo
                                RutaImagen = dr["RutaImagen"].ToString()                 // Nuevo campo
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

        public List<E_CategoriaTatuajes> ListarCategorias(out string mensaje)
        {
            List<E_CategoriaTatuajes> lista = new List<E_CategoriaTatuajes>();
            mensaje = string.Empty;

            try
            {
                using (SqlConnection conn = ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_CRUDGaleriaCategorias", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Accion", 5);
                    cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new E_CategoriaTatuajes
                            {
                                IdCategoriaTatuajes = Convert.ToInt32(dr["IdCategoriaTatuajes"]),
                                NombreCategoria = dr["NombreCategoria"].ToString()
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

        public bool RegistrarGaleriaCategoria(E_GaleriaCategorias galeriaCategoria, out string mensaje)
        {
            mensaje = string.Empty;
            bool resultado = false;

            try
            {
                using (SqlConnection conn = ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_CRUDGaleriaCategorias", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Accion", 1);
                    cmd.Parameters.AddWithValue("@IdImagen", galeriaCategoria.IdImagen);
                    cmd.Parameters.AddWithValue("@IdCategoriaTatuajes", galeriaCategoria.IdCategoriaTatuajes);
                    cmd.Parameters.AddWithValue("@Estado", galeriaCategoria.Estado);
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

        public bool ActualizarGaleriaCategoria(E_GaleriaCategorias galeriaCategoria, out string mensaje)
        {
            mensaje = string.Empty;
            bool resultado = false;

            try
            {
                using (SqlConnection conn = ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_CRUDGaleriaCategorias", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Accion", 3);
                    cmd.Parameters.AddWithValue("@IdImagen", galeriaCategoria.IdImagen);
                    cmd.Parameters.AddWithValue("@IdCategoriaTatuajes", galeriaCategoria.IdCategoriaTatuajes);
                    cmd.Parameters.AddWithValue("@Estado", galeriaCategoria.Estado);
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

        public bool EliminarGaleriaCategoria(int idImagen, int idCategoriaTatuajes, out string mensaje)
        {
            mensaje = string.Empty;
            bool resultado = false;

            try
            {
                using (SqlConnection conn = ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_CRUDGaleriaCategorias", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Accion", 4);
                    cmd.Parameters.AddWithValue("@IdImagen", idImagen);
                    cmd.Parameters.AddWithValue("@IdCategoriaTatuajes", idCategoriaTatuajes);
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

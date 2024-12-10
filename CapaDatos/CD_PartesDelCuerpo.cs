using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CapaEntidades;

namespace CapaDatos
{
    public class CD_PartesDelCuerpo
    {
        public List<E_PartesDelCuerpo> ListarPartesDelCuerpo(out string mensaje)
        {
            List<E_PartesDelCuerpo> lista = new List<E_PartesDelCuerpo>();
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_CRUDPartesDelCuerpo", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Accion", 2);
                    cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new E_PartesDelCuerpo
                            {
                                IdParteDelCuerpo = Convert.ToInt32(dr["IdParteDelCuerpo"]),
                                ParteDelCuerpo = dr["ParteDelCuerpo"].ToString(),
                                Descripcion = dr["Descripcion"]?.ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"])
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

        public bool CrearParteDelCuerpo(E_PartesDelCuerpo parte, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_CRUDPartesDelCuerpo", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Accion", 1);
                    cmd.Parameters.AddWithValue("@ParteDelCuerpo", parte.ParteDelCuerpo);
                    cmd.Parameters.AddWithValue("@Descripcion", parte.Descripcion);
                    cmd.Parameters.AddWithValue("@Estado", parte.Estado);
                    cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                    oconexion.Open();
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

        public bool ActualizarParteDelCuerpo(E_PartesDelCuerpo parte, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_CRUDPartesDelCuerpo", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Accion", 3);
                    cmd.Parameters.AddWithValue("@IdParteDelCuerpo", parte.IdParteDelCuerpo);
                    cmd.Parameters.AddWithValue("@ParteDelCuerpo", parte.ParteDelCuerpo);
                    cmd.Parameters.AddWithValue("@Descripcion", parte.Descripcion);
                    cmd.Parameters.AddWithValue("@Estado", parte.Estado);
                    cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                    oconexion.Open();
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

        public bool EliminarParteDelCuerpo(int idParteDelCuerpo, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_CRUDPartesDelCuerpo", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Accion", 4);
                    cmd.Parameters.AddWithValue("@IdParteDelCuerpo", idParteDelCuerpo);
                    cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                    oconexion.Open();
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

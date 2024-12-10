using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CapaEntidades;

namespace CapaDatos
{
    public class CD_AprobacionMembresias
    {
        // Método para listar aprobaciones pendientes y sus detalles
        public List<E_AprobacionMembresias> ListarAprobaciones(out string mensaje)
        {
            List<E_AprobacionMembresias> lista = new List<E_AprobacionMembresias>();
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_ListarAprobacionMembresias", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new E_AprobacionMembresias
                            {
                                IdAprobacion = Convert.ToInt32(dr["IdAprobacion"]),
                                IdMembresia = Convert.ToInt32(dr["IdMembresia"]),
                                NombreMembresia = dr["NombreMembresia"].ToString(),
                                Comentarios = dr["Comentarios"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"]),
                                AprobadoPor = Convert.ToInt32(dr["AprobadoPor"]),
                                FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"])
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

        // Método para aprobar una membresía
        public bool AprobarMembresia(E_AprobacionMembresias aprobacion, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_AprobarMembresia", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdMembresia", aprobacion.IdMembresia);
                    cmd.Parameters.AddWithValue("@Comentarios", aprobacion.Comentarios);
                    cmd.Parameters.AddWithValue("@Estado", aprobacion.Estado);
                    cmd.Parameters.AddWithValue("@AprobadoPor", aprobacion.AprobadoPor);
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

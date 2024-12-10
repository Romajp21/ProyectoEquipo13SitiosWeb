using CapaEntidades;
using System;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_RegistroMembresias
    {
        private SqlConnection ObtenerConexion()
        {
            SqlConnection conn = new SqlConnection(Conexion.cn);
            conn.Open();
            return conn;
        }

        public bool RegistrarCompraMembresia(E_RegistroMembresias registro, decimal? monto, string numeroDeTarjeta, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection conn = ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistroCompraMembresias", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Accion", 1); // Acción para registrar compra
                    cmd.Parameters.AddWithValue("@IdCliente", registro.IdCliente);
                    cmd.Parameters.AddWithValue("@IdMembresia", registro.IdMembresia);
                    cmd.Parameters.AddWithValue("@Monto", monto.HasValue ? (object)monto.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@NumeroDeTarjeta", !string.IsNullOrEmpty(numeroDeTarjeta) ? (object)numeroDeTarjeta : DBNull.Value);

                    cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["@Resultado"].Value);
                    mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                mensaje = $"Error: {ex.Message}.";
                resultado = false;
            }

            return resultado;
        }

        public E_RegistroMembresias ObtenerMembresiaCliente(int idCliente, out string mensaje)
        {
            E_RegistroMembresias membresia = null;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection conn = ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistroCompraMembresias", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Accion", 2); // Acción para obtener información
                    cmd.Parameters.AddWithValue("@IdCliente", idCliente);

                    cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            membresia = new E_RegistroMembresias
                            {
                                IdRegistroMembresia = Convert.ToInt32(dr["IdRegistroMembresia"]),
                                IdCliente = idCliente,
                                IdMembresia = Convert.ToInt32(dr["IdMembresia"]),
                                FechaCompra = Convert.ToDateTime(dr["FechaCompra"]),
                                FechaExpiracion = Convert.ToDateTime(dr["FechaExpiracion"]),
                                NombreMembresia = dr["NombreMembresia"].ToString(),
                                Precio = Convert.ToDecimal(dr["Precio"]),
                                Contenidos = new List<string>() // Beneficios
                            };

                            do
                            {
                                if (!dr.IsDBNull(dr.GetOrdinal("NombreContenido")))
                                {
                                    membresia.Contenidos.Add(dr["NombreContenido"].ToString());
                                }
                            } while (dr.Read());
                        }
                    }

                    mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                mensaje = $"Error: {ex.Message}.";
            }

            return membresia;
        }
    }
}

using CapaEntidades;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System;

namespace CapaDatos
{
    public class CD_Membresias
    {
        // Método para centralizar la obtención y apertura de la conexión
        private SqlConnection ObtenerConexion()
        {
            SqlConnection conn = new SqlConnection(Conexion.cn);
            conn.Open();
            return conn;
        }

        public List<E_Membresias> ListarMembresias(out string mensaje)
        {
            List<E_Membresias> lista = new List<E_Membresias>();
            mensaje = string.Empty;

            try
            {
                using (SqlConnection conn = ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_CRUDMembresias", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Accion", 2);
                    cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new E_Membresias
                            {
                                IdMembresia = Convert.ToInt32(dr["IdMembresia"]),
                                NombreMembresia = dr["NombreMembresia"].ToString(),
                                Precio = Convert.ToDecimal(dr["Precio"]),
                                Duracion = Convert.ToInt32(dr["Duracion"]),
                                CreadoPor = Convert.ToInt32(dr["CreadoPor"]),
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

        public bool RegistrarMembresia(E_Membresias membresia, out string mensaje)
        {
            mensaje = string.Empty;
            bool resultado = false;

            try
            {
                using (SqlConnection conn = ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_CRUDMembresias", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Accion", 1);
                    cmd.Parameters.AddWithValue("@NombreMembresia", membresia.NombreMembresia);
                    cmd.Parameters.AddWithValue("@Precio", membresia.Precio);
                    cmd.Parameters.AddWithValue("@Duracion", membresia.Duracion);
                    cmd.Parameters.AddWithValue("@CreadoPor", membresia.CreadoPor);
                    cmd.Parameters.AddWithValue("@Estado", membresia.Estado);
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

        public bool ActualizarMembresia(E_Membresias membresia, out string mensaje)
        {
            mensaje = string.Empty;
            bool resultado = false;

            try
            {
                using (SqlConnection conn = ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_CRUDMembresias", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Accion", 3);
                    cmd.Parameters.AddWithValue("@IdMembresia", membresia.IdMembresia);
                    cmd.Parameters.AddWithValue("@NombreMembresia", membresia.NombreMembresia);
                    cmd.Parameters.AddWithValue("@Precio", membresia.Precio);
                    cmd.Parameters.AddWithValue("@Duracion", membresia.Duracion);
                    cmd.Parameters.AddWithValue("@Estado", membresia.Estado);
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

        public bool EliminarMembresia(int id, out string mensaje)
        {
            mensaje = string.Empty;
            bool resultado = false;

            try
            {
                using (SqlConnection conn = ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_CRUDMembresias", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Accion", 4);
                    cmd.Parameters.AddWithValue("@IdMembresia", id);
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

        public E_Membresias ObtenerMembresia(int id, out string mensaje)
        {
            E_Membresias membresia = null;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection conn = ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_CRUDMembresias", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Accion", 5); // Acción para obtener membresía específica
                    cmd.Parameters.AddWithValue("@IdMembresia", id);
                    cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            membresia = new E_Membresias
                            {
                                IdMembresia = Convert.ToInt32(dr["IdMembresia"]),
                                NombreMembresia = dr["NombreMembresia"].ToString(),
                                Precio = Convert.ToDecimal(dr["Precio"]),
                                Duracion = Convert.ToInt32(dr["Duracion"]),
                                CreadoPor = Convert.ToInt32(dr["CreadoPor"]),
                                Estado = Convert.ToBoolean(dr["Estado"])
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

            return membresia;
        }

        // Registrar una compra de membresía
        public bool RegistrarCompraMembresia(E_RegistroMembresias registro, E_PagoMembresias pago, out string mensaje)
        {
            mensaje = string.Empty;
            bool resultado = false;

            try
            {
                using (SqlConnection conn = ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistroCompraMembresias", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdCliente", registro.IdCliente);
                    cmd.Parameters.AddWithValue("@IdMembresia", registro.IdMembresia);
                    cmd.Parameters.AddWithValue("@Monto", pago.Monto);
                    cmd.Parameters.AddWithValue("@NumeroDeTarjeta", string.IsNullOrEmpty(pago.NumeroDeTarjeta) ? DBNull.Value : pago.NumeroDeTarjeta);

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

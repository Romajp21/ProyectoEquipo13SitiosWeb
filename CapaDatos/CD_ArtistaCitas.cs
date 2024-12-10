using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_ArtistaCitas
    {
        private SqlConnection ObtenerConexion()
        {
            SqlConnection conn = new SqlConnection(Conexion.cn);
            conn.Open();
            return conn;
        }

        // Método para listar citas por artista
        public List<E_ArtistaCitas> ListarCitasPorArtista(int idArtista, out string mensaje)
        {
            List<E_ArtistaCitas> lista = new List<E_ArtistaCitas>();
            mensaje = string.Empty;

            try
            {
                using (SqlConnection conn = ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_CRUDArtistaCitas", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Agregar los parámetros requeridos
                    cmd.Parameters.AddWithValue("@Accion", 1); // Acción para listar citas
                    cmd.Parameters.AddWithValue("@IdArtista", idArtista);

                    // Parámetros opcionales no necesarios en esta acción
                    // Por lo tanto, no se deben agregar

                    cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new E_ArtistaCitas
                            {
                                IdCita = Convert.ToInt32(dr["IdCita"]),
                                IdCliente = Convert.ToInt32(dr["IdCliente"]),
                                NombreCliente = dr["NombreCliente"].ToString(),
                                IdArtista = Convert.ToInt32(dr["IdArtista"]),
                                NombreArtista = dr["NombreArtista"].ToString(),
                                Fecha = Convert.ToDateTime(dr["Fecha"]),
                                HoraInicio = TimeSpan.Parse(dr["HoraInicio"].ToString()),
                                HoraFin = TimeSpan.Parse(dr["HoraFin"].ToString()),
                                Descripcion = dr["Descripcion"].ToString(),
                                Estado = dr["Estado"].ToString(),
                                IdFormulario = dr["IdFormulario"] as int?
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

        // Método para actualizar el estado de una cita
        public bool ActualizarEstadoCita(int idCita, out string mensaje)
        {
            mensaje = string.Empty;
            bool resultado = false;

            try
            {
                using (SqlConnection conn = ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_CRUDArtistaCitas", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Accion", 2); // Acción para actualizar el estado
                    cmd.Parameters.AddWithValue("@IdCita", idCita);
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

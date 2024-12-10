using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_ClienteMembresia
    {
        // Método para centralizar la obtención y apertura de la conexión
        private SqlConnection ObtenerConexion()
        {
            SqlConnection conn = new SqlConnection(Conexion.cn);
            conn.Open();
            return conn;
        }

        // Método para listar membresías y su contenido aprobado
        public List<E_ClienteMembresia> ListarMembresias(out string mensaje)
        {
            List<E_ClienteMembresia> lista = new List<E_ClienteMembresia>();
            mensaje = string.Empty;

            try
            {
                using (SqlConnection conn = ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("spCrudClienteMembresias", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Accion", 1); // Acción 1: Consulta general
                    cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            // Buscar si la membresía ya está en la lista
                            var membresia = lista.Find(m => m.IdMembresia == Convert.ToInt32(dr["IdMembresia"]));

                            if (membresia == null)
                            {
                                // Crear nueva membresía si no existe
                                membresia = new E_ClienteMembresia
                                {
                                    IdMembresia = Convert.ToInt32(dr["IdMembresia"]),
                                    NombreMembresia = dr["NombreMembresia"].ToString(),
                                    Precio = Convert.ToDecimal(dr["Precio"]),
                                    Duracion = Convert.ToInt32(dr["Duracion"]),
                                    Contenidos = new List<E_ContenidoMembresia>()
                                };
                                lista.Add(membresia);
                            }

                            // Agregar contenido a la membresía
                            membresia.Contenidos.Add(new E_ContenidoMembresia
                            {
                                IdMembresContenido = Convert.ToInt32(dr["IdMembresContenido"]),
                                NombreContenido = dr["NombreContenido"].ToString(),
                                ComentarioAprobacion = dr["ComentarioAprobacion"].ToString(),
                                FechaAprobacion = Convert.ToDateTime(dr["FechaAprobacion"]),
                                EstadoAprobacion = Convert.ToBoolean(dr["EstadoAprobacion"])
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

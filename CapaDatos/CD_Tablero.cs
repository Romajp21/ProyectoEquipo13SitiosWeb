using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_Tablero
    {
        // Obtener datos del tablero
        public Dictionary<string, object> ObtenerDatosTablero(string rol, int? idUsuario, out string mensaje)
        {
            var datos = new Dictionary<string, object>();
            mensaje = "";

            try
            {
                using (SqlConnection con = new SqlConnection(Conexion.cn))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_TableroGeneral", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Rol", rol);
                        cmd.Parameters.AddWithValue("@IdUsuario", idUsuario ?? (object)DBNull.Value);

                        con.Open();

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                // Leer cada columna de la fila devuelta
                                for (int i = 0; i < dr.FieldCount; i++)
                                {
                                    datos[dr.GetName(i)] = dr.IsDBNull(i) ? 0 : dr.GetValue(i);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = $"Error al obtener datos del tablero: {ex.Message}";
            }

            return datos;
        }
    }
}

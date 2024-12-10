using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_Recursos
    {
        private string conexion = "tu_cadena_de_conexion";

        /// <summary>
        /// Ejecuta el stored procedure para obtener los correos de acuerdo al módulo.
        /// </summary>
        /// <param name="modulo">El módulo (Artistas, Asistente, Jefatura, Clientes)</param>
        /// <returns>Lista de correos electrónicos</returns>
        public List<string> ObtenerCorreos(string modulo)
        {
            List<string> correos = new List<string>();

            try
            {
                using (SqlConnection con = new SqlConnection(conexion))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ObtenerCorreos", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Modulo", modulo);

                        con.Open();

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                correos.Add(dr["Correo"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw new Exception("Error al obtener correos", ex);
            }

            return correos;
        }
    }
}

using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CapaEntidades;
using Microsoft.Extensions.Configuration;

namespace CapaDatos
{
    public class CD_CuidadosDelTatuaje
    {
        private readonly string connectionString;

        public CD_CuidadosDelTatuaje(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<E_CuidadosDelTatuaje> ObtenerCuidadosDelTatuaje()
        {
            var cuidados = new List<E_CuidadosDelTatuaje>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM CuidadosDelTatuaje";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cuidados.Add(new E_CuidadosDelTatuaje
                            {
                                IdCuidadosDelTatuaje = (int)reader["IdCuidadosDelTatuaje"],
                                DescripcionCuidadosDelTatuaje = reader["DescripcionCuidadosDelTatuaje"].ToString(),
                                Estado = (bool)reader["Estado"],
                                Imagen = reader["Imagen"]?.ToString()
                            });
                        }
                    }
                }
            }

            return cuidados;
        }

        public int InsertarCuidadoDelTatuaje(E_CuidadosDelTatuaje cuidado)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    INSERT INTO CuidadosDelTatuaje (DescripcionCuidadosDelTatuaje, Estado, Imagen)
                    OUTPUT INSERTED.IdCuidadosDelTatuaje
                    VALUES (@DescripcionCuidadosDelTatuaje, 0, @Imagen)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@DescripcionCuidadosDelTatuaje", cuidado.DescripcionCuidadosDelTatuaje);
                    cmd.Parameters.AddWithValue("@Imagen", string.IsNullOrEmpty(cuidado.Imagen) ? (object)DBNull.Value : cuidado.Imagen);

                    return (int)cmd.ExecuteScalar();
                }
            }
        }

        public void EditarCuidadoDelTatuaje(E_CuidadosDelTatuaje cuidado)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    UPDATE CuidadosDelTatuaje
                    SET DescripcionCuidadosDelTatuaje = @DescripcionCuidadosDelTatuaje,
                        Imagen = @Imagen
                    WHERE IdCuidadosDelTatuaje = @IdCuidadosDelTatuaje";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IdCuidadosDelTatuaje", cuidado.IdCuidadosDelTatuaje);
                    cmd.Parameters.AddWithValue("@DescripcionCuidadosDelTatuaje", cuidado.DescripcionCuidadosDelTatuaje);
                    cmd.Parameters.AddWithValue("@Imagen", string.IsNullOrEmpty(cuidado.Imagen) ? (object)DBNull.Value : cuidado.Imagen);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void EliminarCuidadoDelTatuaje(int idCuidadosDelTatuaje)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM CuidadosDelTatuaje WHERE IdCuidadosDelTatuaje = @IdCuidadosDelTatuaje";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IdCuidadosDelTatuaje", idCuidadosDelTatuaje);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void EditarCuidadoDelTatuajeJefatura(E_CuidadosDelTatuaje cuidado)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
            UPDATE CuidadosDelTatuaje
            SET DescripcionCuidadosDelTatuaje = @DescripcionCuidadosDelTatuaje,
                Imagen = @Imagen,
                Estado = @Estado
            WHERE IdCuidadosDelTatuaje = @IdCuidadosDelTatuaje";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IdCuidadosDelTatuaje", cuidado.IdCuidadosDelTatuaje);
                    cmd.Parameters.AddWithValue("@DescripcionCuidadosDelTatuaje", cuidado.DescripcionCuidadosDelTatuaje);
                    cmd.Parameters.AddWithValue("@Imagen", string.IsNullOrEmpty(cuidado.Imagen) ? (object)DBNull.Value : cuidado.Imagen);
                    cmd.Parameters.AddWithValue("@Estado", cuidado.Estado);

                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}

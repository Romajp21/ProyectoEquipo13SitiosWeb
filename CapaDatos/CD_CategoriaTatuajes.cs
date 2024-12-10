using CapaEntidades;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public class CD_CategoriaTatuajes
{
    private readonly string connectionString;

    public CD_CategoriaTatuajes(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public List<E_CategoriaTatuajes> ObtenerCategorias()
    {
        var categorias = new List<E_CategoriaTatuajes>();

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM CategoriaTatuajes", conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        categorias.Add(new E_CategoriaTatuajes
                        {
                            IdCategoriaTatuajes = (int)reader["IdCategoriaTatuajes"],
                            NombreCategoria = reader["NombreCategoria"].ToString(),
                            Estado = (bool)reader["Estado"]
                        });
                    }
                }
            }
        }

        return categorias; // Asegúrate de que no devuelva null
    }


    public void InsertarCategoria(E_CategoriaTatuajes categoria)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            using (SqlCommand cmd = new SqlCommand("INSERT INTO CategoriaTatuajes (NombreCategoria, Estado) VALUES (@NombreCategoria, 0)", conn))
            {
                cmd.Parameters.AddWithValue("@NombreCategoria", categoria.NombreCategoria);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public void EditarCategoria(E_CategoriaTatuajes categoria)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            using (SqlCommand cmd = new SqlCommand("UPDATE CategoriaTatuajes SET NombreCategoria = @NombreCategoria WHERE IdCategoriaTatuajes = @IdCategoria", conn))
            {
                cmd.Parameters.AddWithValue("@NombreCategoria", categoria.NombreCategoria);
                cmd.Parameters.AddWithValue("@IdCategoria", categoria.IdCategoriaTatuajes);
                cmd.ExecuteNonQuery();
            }
        }
    }


    public void EliminarCategoria(int idCategoria)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            using (SqlCommand cmd = new SqlCommand("DELETE FROM CategoriaTatuajes WHERE IdCategoriaTatuajes = @IdCategoria", conn))
            {
                cmd.Parameters.AddWithValue("@IdCategoria", idCategoria);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public void EditarCategoriaJefatura(E_CategoriaTatuajes categoria)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            using (SqlCommand cmd = new SqlCommand("UPDATE CategoriaTatuajes SET NombreCategoria = @Nombre, Estado = @Estado WHERE IdCategoriaTatuajes = @IdCategoria", conn))
            {
                cmd.Parameters.AddWithValue("@Nombre", categoria.NombreCategoria);
                cmd.Parameters.AddWithValue("@Estado", categoria.Estado);
                cmd.Parameters.AddWithValue("@IdCategoria", categoria.IdCategoriaTatuajes);
                cmd.ExecuteNonQuery();
            }
        }
    }



}

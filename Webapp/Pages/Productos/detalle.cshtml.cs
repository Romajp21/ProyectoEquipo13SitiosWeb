using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace Webapp.Pages.Productos
{
    public class DetalleModel : PageModel
    {
        private readonly string connectionString;

        public DetalleModel(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public Producto Producto { get; set; }

        public IActionResult OnGet(int id)
        {
            Producto = ObtenerProducto(id);

            if (Producto == null)
            {
                return RedirectToPage("/Productos");
            }

            return Page();
        }

        private Producto ObtenerProducto(int id)
        {
            using (SqlConnection conn = new(connectionString))
            {
                conn.Open();
                string query = "SELECT p.IdProducto, p.NombreProducto, p.Precio, p.Descripcion, c.CategoriaProducto, p.ImagenUrl " +
                               "FROM Producto p " +
                               "JOIN CategoriaProductos c ON p.IdCategoriaProducto = c.IdCategoriaProducto " +
                               "WHERE p.IdProducto = @IdProducto";

                SqlCommand cmd = new(query, conn);
                cmd.Parameters.AddWithValue("@IdProducto", id);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Producto
                        {
                            IdProducto = (int)reader["IdProducto"],
                            NombreProducto = reader["NombreProducto"].ToString(),
                            Precio = (decimal)reader["Precio"],
                            Descripcion = reader["Descripcion"].ToString(),
                            Categoria = reader["CategoriaProducto"].ToString(),
                            ImagenUrl = reader["ImagenUrl"].ToString()
                        };
                    }
                }
            }

            return null;
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Webapp.Pages.Productos
{
    public class ProductosModel : PageModel
    {
        private readonly string connectionString;

        public ProductosModel(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Producto> Productos { get; set; } = new List<Producto>();
        public List<CategoriaProducto> Categorias { get; set; } = new List<CategoriaProducto>();

        [BindProperty(SupportsGet = true)]
        public int? CategoriaSeleccionada { get; set; }

        [BindProperty(SupportsGet = true)]
        public string OrdenSeleccionado { get; set; }

        public void OnGet()
        {
            Categorias = ObtenerCategorias();
            Productos = ObtenerProductos(CategoriaSeleccionada, OrdenSeleccionado);
        }

        private List<Producto> ObtenerProductos(int? categoriaId, string orden)
        {
            var productos = new List<Producto>();

            using (SqlConnection conn = new(connectionString))
            {
                conn.Open();
                string query = @"SELECT p.IdProducto, p.NombreProducto, p.Precio, p.Descripcion, c.CategoriaProducto, p.ImagenUrl 
                         FROM Producto p 
                         JOIN CategoriaProductos c ON p.IdCategoriaProducto = c.IdCategoriaProducto";

                if (categoriaId.HasValue)
                {
                    query += " WHERE p.IdCategoriaProducto = @CategoriaId";
                }

                switch (orden)
                {
                    case "low":
                        query += " ORDER BY p.Precio ASC";
                        break;
                    case "high":
                        query += " ORDER BY p.Precio DESC";
                        break;
                    default:
                        query += " ORDER BY p.NombreProducto ASC";
                        break;
                }

                SqlCommand cmd = new(query, conn);

                if (categoriaId.HasValue)
                {
                    cmd.Parameters.AddWithValue("@CategoriaId", categoriaId.Value);
                }

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        productos.Add(new Producto
                        {
                            IdProducto = (int)reader["IdProducto"],
                            NombreProducto = reader["NombreProducto"].ToString(),
                            Precio = (decimal)reader["Precio"],
                            Descripcion = reader["Descripcion"].ToString(),
                            Categoria = reader["CategoriaProducto"].ToString(),
                            ImagenUrl = reader["ImagenUrl"].ToString()
                        });
                    }
                }
            }

            return productos;
        }


        private List<CategoriaProducto> ObtenerCategorias()
        {
            var categorias = new List<CategoriaProducto>();

            using (SqlConnection conn = new(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM CategoriaProductos WHERE Estado = 1";
                SqlCommand cmd = new(query, conn);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        categorias.Add(new CategoriaProducto
                        {
                            IdCategoriaProducto = (int)reader["IdCategoriaProducto"],
                            Categoriaproducto = reader["CategoriaProducto"].ToString()
                        });
                    }
                }
            }

            return categorias;
        }
    }

    public class Producto
    {
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public decimal Precio { get; set; }
        public string Descripcion { get; set; }
        public string Categoria { get; set; }
        public string ImagenUrl { get; set; }
    }

    public class CategoriaProducto
    {
        public int IdCategoriaProducto { get; set; }
        public string Categoriaproducto { get; set; }
    }
}

using CapaEntidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AsistenteAdministrativo.Pages.CRUDS
{
    public class CategoriaProductos
    {
        public int IdCategoriaProducto { get; set; }
        public string CategoriaProducto { get; set; }
        public bool Estado { get; set; }
    }
    public class Producto
    {
        public int IdProducto { get; set; }
        public int IdCategoriaProducto { get; set; }
        public string NombreProducto { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public bool Estado { get; set; }
        public string Categoria { get; set; } // Para mostrar el nombre de la categoría
    }
    public class ProductosModel : PageModel
    {
        private readonly string connectionString;

        public ProductosModel(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Producto> ListaProductos { get; set; } = new();
        public List<CategoriaProductos> ListaCategorias { get; set; } = new();

        [BindProperty]
        public Producto NuevoProducto { get; set; }

        [BindProperty]
        public Producto ProductoEditar { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? IdProductoEliminar { get; set; }

        public string Mensaje { get; set; }

        public void OnGet()
        {
            ListaProductos = ObtenerProductos();
            ListaCategorias = ObtenerCategorias();
        }

        public IActionResult OnPostCrear()
        {
            if (NuevoProducto == null || string.IsNullOrWhiteSpace(NuevoProducto.NombreProducto) || NuevoProducto.Precio <= 0 || NuevoProducto.Cantidad < 0)
            {
                Mensaje = "Error: Datos inválidos para crear un producto.";
                ListaProductos = ObtenerProductos();
                ListaCategorias = ObtenerCategorias();
                return Page();
            }

            try
            {
                CrearProducto(NuevoProducto);
                Mensaje = "Producto creado exitosamente.";
                return RedirectToPage();
            }
            catch (SqlException ex)
            {
                Mensaje = $"Error al crear el producto: {ex.Message}";
                ListaProductos = ObtenerProductos();
                ListaCategorias = ObtenerCategorias();
                return Page();
            }
        }

        public IActionResult OnPostEditar()
        {
            if (ProductoEditar == null || string.IsNullOrWhiteSpace(ProductoEditar.NombreProducto) || ProductoEditar.Precio <= 0 || ProductoEditar.Cantidad < 0)
            {
                Mensaje = "Error: Datos inválidos para editar el producto.";
                ListaProductos = ObtenerProductos();
                return Page();
            }

            try
            {
                EditarProducto(ProductoEditar);
                Mensaje = "Producto editado exitosamente.";
                return RedirectToPage();
            }
            catch (SqlException ex)
            {
                Mensaje = $"Error al editar el producto: {ex.Message}";
                ListaProductos = ObtenerProductos();
                return Page();
            }
        }

        public IActionResult OnPostEliminar()
        {
            if (!IdProductoEliminar.HasValue)
            {
                Mensaje = "Error: No se pudo identificar el producto a eliminar.";
                ListaProductos = ObtenerProductos();
                return Page();
            }

            try
            {
                EliminarProducto(IdProductoEliminar.Value);
                Mensaje = "Producto eliminado exitosamente.";
                return RedirectToPage();
            }
            catch (SqlException ex)
            {
                Mensaje = $"Error al eliminar el producto: {ex.Message}";
                ListaProductos = ObtenerProductos();
                return Page();
            }
        }

        private List<Producto> ObtenerProductos()
        {
            var lista = new List<Producto>();

            using (SqlConnection conn = new(connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT 
                        p.IdProducto, p.NombreProducto, p.Descripcion, p.Cantidad, p.Precio, p.Estado, 
                        c.CategoriaProducto AS Categoria 
                    FROM Producto p
                    JOIN CategoriaProductos c ON p.IdCategoriaProducto = c.IdCategoriaProducto";

                SqlCommand cmd = new(query, conn);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Producto
                        {
                            IdProducto = (int)reader["IdProducto"],
                            NombreProducto = reader["NombreProducto"].ToString(),
                            Descripcion = reader["Descripcion"].ToString(),
                            Cantidad = (int)reader["Cantidad"],
                            Precio = (decimal)reader["Precio"],
                            Estado = (bool)reader["Estado"],
                            Categoria = reader["Categoria"].ToString()
                        });
                    }
                }
            }

            return lista;
        }

        private List<CategoriaProductos> ObtenerCategorias()
        {
            var lista = new List<CategoriaProductos>();

            using (SqlConnection conn = new(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM CategoriaProductos WHERE Estado = 1";

                SqlCommand cmd = new(query, conn);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new CategoriaProductos
                        {
                            IdCategoriaProducto = (int)reader["IdCategoriaProducto"],
                            CategoriaProducto = reader["CategoriaProducto"].ToString()
                        });
                    }
                }
            }

            return lista;
        }

        private void CrearProducto(Producto producto)
        {
            using (SqlConnection conn = new(connectionString))
            {
                conn.Open();
                string query = @"
                    INSERT INTO Producto (IdCategoriaProducto, NombreProducto, Descripcion, Cantidad, Precio, Estado) 
                    VALUES (@IdCategoriaProducto, @NombreProducto, @Descripcion, @Cantidad, @Precio, @Estado)";

                SqlCommand cmd = new(query, conn);
                cmd.Parameters.AddWithValue("@IdCategoriaProducto", producto.IdCategoriaProducto);
                cmd.Parameters.AddWithValue("@NombreProducto", producto.NombreProducto);
                cmd.Parameters.AddWithValue("@Descripcion", producto.Descripcion ?? string.Empty);
                cmd.Parameters.AddWithValue("@Cantidad", producto.Cantidad);
                cmd.Parameters.AddWithValue("@Precio", producto.Precio);
                cmd.Parameters.AddWithValue("@Estado", producto.Estado);

                cmd.ExecuteNonQuery();
            }
        }

        private void EditarProducto(Producto producto)
        {
            using (SqlConnection conn = new(connectionString))
            {
                conn.Open();
                string query = @"
                    UPDATE Producto 
                    SET IdCategoriaProducto = @IdCategoriaProducto, NombreProducto = @NombreProducto, 
                        Descripcion = @Descripcion, Cantidad = @Cantidad, Precio = @Precio, Estado = @Estado 
                    WHERE IdProducto = @IdProducto";

                SqlCommand cmd = new(query, conn);
                cmd.Parameters.AddWithValue("@IdProducto", producto.IdProducto);
                cmd.Parameters.AddWithValue("@IdCategoriaProducto", producto.IdCategoriaProducto);
                cmd.Parameters.AddWithValue("@NombreProducto", producto.NombreProducto);
                cmd.Parameters.AddWithValue("@Descripcion", producto.Descripcion ?? string.Empty);
                cmd.Parameters.AddWithValue("@Cantidad", producto.Cantidad);
                cmd.Parameters.AddWithValue("@Precio", producto.Precio);
                cmd.Parameters.AddWithValue("@Estado", producto.Estado);

                cmd.ExecuteNonQuery();
            }
        }

        private void EliminarProducto(int idProducto)
        {
            using (SqlConnection conn = new(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Producto WHERE IdProducto = @IdProducto";

                SqlCommand cmd = new(query, conn);
                cmd.Parameters.AddWithValue("@IdProducto", idProducto);

                cmd.ExecuteNonQuery();
            }
        }
    }
}

using System.Data.SqlClient;
using System.Collections.Generic;
using CapaEntidades;
using Microsoft.Extensions.Configuration;

namespace CapaDatos
{
    public class CD_Productos
    {
        private readonly Conexion _conexion;

        public CD_Productos(Conexion conexion)
        {
            _conexion = conexion;
        }

        // Constructor para inicializar la conexión desde la configuración
    
        public SqlConnection ObtenerConexion()
        {
            return _conexion.ObtenerConexion();
        }

        public List<E_Producto> ObtenerProductosConDetalles()
        {
            var productos = new List<E_Producto>();

            using (var conn = _conexion.ObtenerConexion()) // Usar el método para obtener la conexión
            {
                conn.Open();
                using (var cmd = new SqlCommand("sp_ObtenerProductos", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            productos.Add(new E_Producto
                            {
                                IdProducto = (int)reader["IdProducto"],
                                NombreProducto = reader["NombreProducto"].ToString(),
                                Descripcion = reader["Descripcion"].ToString(),
                                Cantidad = (int)reader["Cantidad"],
                                Precio = (decimal)reader["Precio"],
                                CategoriaProducto = reader["CategoriaProducto"].ToString(),
                                ImagenUrl = reader["ImagenUrl"].ToString(),
                                Estado = (bool)reader["Estado"]
                            });
                        }
                    }
                }
            }

            return productos;
        }

        public void CambiarEstadoProducto(int idProducto, bool nuevoEstado)
        {
            using (var conn = _conexion.ObtenerConexion()) // Usar el método para obtener la conexión
            {
                conn.Open();
                using (var cmd = new SqlCommand("UPDATE Producto SET Estado = @NuevoEstado WHERE IdProducto = @IdProducto", conn))
                {
                    cmd.Parameters.AddWithValue("@IdProducto", idProducto);
                    cmd.Parameters.AddWithValue("@NuevoEstado", nuevoEstado);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}

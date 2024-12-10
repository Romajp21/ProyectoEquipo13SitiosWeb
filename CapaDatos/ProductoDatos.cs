using CapaEntidades;
using System.Collections.Generic;
using System.Data.SqlClient;

using CapaDatos;

namespace CapaDatos
{
    public class ProductoDatos
    {
        private readonly Conexion _conexion;

        public ProductoDatos(Conexion conexion)
        {
            _conexion = conexion;
        }

        public List<E_Producto> ListarProductos()
        {
            var lista = new List<E_Producto>();
            using (var conn = _conexion.ObtenerConexion())
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM Producto", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new E_Producto
                        {
                            IdProducto = (int)reader["IdProducto"],
                            //Nombre = reader["Nombre"].ToString(),
                            Precio = (decimal)reader["Precio"],
                           // Stock = (int)reader["Stock"]
                        });
                    }
                }
            }
            return lista;

        }



    }
}

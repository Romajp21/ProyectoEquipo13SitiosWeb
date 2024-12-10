using System.Data.SqlClient;

namespace CapaDatos
{
    public class Conexion
    {
        // Cadena de conexión estática
        private static string _connectionString = "Data Source=ROMAJP\\DEVELOPER1;Initial Catalog=sitios_equipo13;User ID=sa;Password=12345678";
        //private static string _connectionString = "Data Source=tiusr11pl.cuc-carrera-ti.ac.cr;Initial Catalog=sitios_equipo13;User ID=Grupo13;Password=Progra2024";
        // Método para obtener la cadena de conexión de forma estática
        public static string cn => _connectionString;

        public SqlConnection ObtenerConexion()
        {
            return new SqlConnection(_connectionString);
        }
    }
}

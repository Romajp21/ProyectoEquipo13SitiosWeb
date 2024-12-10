namespace CapaEntidades
{
    public class E_RegistroMembresias
    {
        public int IdRegistroMembresia { get; set; } // Clave primaria
        public int IdCliente { get; set; }          // ID del cliente
        public int IdMembresia { get; set; }        // ID de la membresía
        public DateTime FechaCompra { get; set; }   // Fecha de compra
        public DateTime FechaExpiracion { get; set; } // Fecha de expiración
        public bool Estado { get; set; }           // Estado (1: Activo, 0: Inactivo)

        // Propiedades adicionales para referencias (si se necesitan)
        public string NombreCliente { get; set; }   // Opcional: Nombre del cliente
        public string NombreMembresia { get; set; } // Opcional: Nombre de la membresía
        // Propiedades adicionales para referencias
        
        public decimal Precio { get; set; }         // Precio de la membresía
        public List<string> Contenidos { get; set; } = new List<string>(); // Lista de beneficios

    }
}

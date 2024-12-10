namespace CapaEntidades
{
    public class E_Testimonio
    {
        public int IdTestimonio { get; set; }
        public int IdCliente { get; set; }
        public int IdCita { get; set; }
        public string Testimonio { get; set; }
        public int Calificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int? RevisadoPor { get; set; }
        public bool Estado { get; set; }

        // Propiedad extra para mostrar el nombre del cliente (opcional)
        public string NombreCliente { get; set; }
        // Propiedad extra para mostrar información sobre la cita (opcional)
        public string DescripcionCita { get; set; }
        public string Cliente { get; set; } // Nombre completo del cliente
        public string Artista { get; set; } // Nombre completo del artista
    }
}

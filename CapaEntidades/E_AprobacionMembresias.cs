public class E_AprobacionMembresias
{
    public int IdAprobacion { get; set; }
    public int IdMembresia { get; set; }
    public string Comentarios { get; set; }
    public int AprobadoPor { get; set; }
    public bool Estado { get; set; }
    public DateTime FechaCreacion { get; set; }
    public string NombreMembresia { get; set; } // Si es necesario, agrega esta propiedad
}

using System;

namespace CapaEntidades
{
    public class E_AprobacionMembresiaContenido
    {
        // Campos de la tabla AprobacionMembresiaContenido
        public int? IdAprobacion { get; set; }               // Puede ser nulo si aún no existe una aprobación
        public int IdMembresContenido { get; set; }          // ID del contenido de la membresía
        public bool? Estado { get; set; }                    // Estado de aprobación (1 = Aprobado, 0 = Rechazado)
        public string Comentario { get; set; }               // Comentario del jefe
        public int? AprobadoPor { get; set; }                // ID del jefe que aprueba, puede ser nulo
        public DateTime? FechaAprobacion { get; set; }       // Fecha de aprobación, puede ser nulo

        // Campos adicionales de la tabla MembresiaContenido y Membresias
        public int IdMembresia { get; set; }                 // ID de la membresía
        public string NombreMembresia { get; set; }          // Nombre de la membresía
        public string NombreContenido { get; set; }          // Nombre del contenido de la membresía
        public string ComentarioAsistente { get; set; }      // Comentario del asistente administrativo
    }
}

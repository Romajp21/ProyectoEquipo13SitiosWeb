using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class E_Notificacion
    {
        public int IdNotificacion { get; set; } // ID único de la notificación
        public string Modulo { get; set; } // Módulo al que pertenece (Artistas, Clientes, etc.)
        public string TipoEvento { get; set; } // Tipo de evento (Nueva Membresía, Aprobación, etc.)
        public string Correo { get; set; } // Correo del destinatario
        public string Asunto { get; set; } // Asunto del correo
        public string Mensaje { get; set; } // Mensaje del correo
        public DateTime FechaCreacion { get; set; } // Fecha de creación de la notificación
        public DateTime? FechaEnvio { get; set; } // Fecha en la que se envió (puede ser nula)
        public string Estado { get; set; } // Estado de la notificación (Pendiente, Enviada, etc.)
    }
}

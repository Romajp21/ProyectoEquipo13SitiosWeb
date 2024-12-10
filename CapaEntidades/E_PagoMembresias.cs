namespace CapaEntidades
{
    public class E_PagoMembresias
    {
        public int IdPago { get; set; }             // Clave primaria
        public int IdRegistroMembresia { get; set; } // ID del registro de membresía
        public DateTime Fecha { get; set; }        // Fecha del pago
        public decimal Monto { get; set; }         // Monto del pago
        public string NumeroDeTarjeta { get; set; } // Número de tarjeta (enmascarado o no)
        public bool Estado { get; set; }           // Estado del pago (1: Completo, 0: Pendiente)
        public string ReferenciaPago { get; set; } // Referencia del pago
    }
}

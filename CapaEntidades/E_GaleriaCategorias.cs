using System;

namespace CapaEntidades
{
    public class E_GaleriaCategorias
    {
        // Clave primaria y referencia a la tabla Galeria
        public int IdImagen { get; set; }

        // Clave primaria y referencia a la tabla CategoriaTatuajes
        public int IdCategoriaTatuajes { get; set; }

        // Estado del registro en GaleriaCategorias (true: Activo, false: Inactivo)
        public bool Estado { get; set; }

        public string DescripcionTatuaje { get; set; }
        public string RutaImagen { get; set; }

        // Constructor por defecto
        public E_GaleriaCategorias() { }

        // Constructor con parámetros para inicialización rápida
        public E_GaleriaCategorias(int idImagen, int idCategoriaTatuajes, bool estado)
        {
            IdImagen = idImagen;
            IdCategoriaTatuajes = idCategoriaTatuajes;
            Estado = estado;
        }

        // Método para mostrar información de la entidad (opcional, útil para depuración)
        public override string ToString()
        {
            return $"IdImagen: {IdImagen}, IdCategoriaTatuajes: {IdCategoriaTatuajes}, Estado: {Estado}";
        }
    }
}

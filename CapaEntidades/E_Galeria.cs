using System;

namespace CapaEntidades
{
    public class E_Galeria
    {
        // Clave primaria
        public int IdImagen { get; set; }

        // Referencia al artista que subió la imagen
        public int IdArtista { get; set; }

        // Descripción del tatuaje para información adicional
        public string DescripcionTatuaje { get; set; }

        // Ruta del archivo de imagen en el servidor
        public string RutaImagen { get; set; }

        // Estado de la imagen (true: Activo, false: Inactivo)
        public bool Estado { get; set; }

        // Fecha de creación o última modificación
        public DateTime Fecha { get; set; }

        // Constructor por defecto
        public E_Galeria() { }

        // Constructor con parámetros para inicialización rápida
        public E_Galeria(int idImagen, int idArtista, string descripcionTatuaje, string rutaImagen, bool estado, DateTime fecha)
        {
            IdImagen = idImagen;
            IdArtista = idArtista;
            DescripcionTatuaje = descripcionTatuaje;
            RutaImagen = rutaImagen;
            Estado = estado;
            Fecha = fecha;
        }

        // Método para mostrar información de la entidad (opcional, útil para depuración)
        public override string ToString()
        {
            return $"IdImagen: {IdImagen}, IdArtista: {IdArtista}, Descripcion: {DescripcionTatuaje}, Estado: {Estado}, Fecha: {Fecha}";
        }

        public string RutaCompleta
        {
            get
            {
                return $"/Artistas/{RutaImagen}";
            }
        }

    }
}

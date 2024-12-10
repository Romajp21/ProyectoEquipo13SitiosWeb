using CapaDatos;
using CapaEntidades;
using Microsoft.Extensions.Configuration;

namespace CapaNegocios
{
    public class CN_Artista
    {
        private readonly CD_Artista cdArtista;

        // Constructor que recibe IConfiguration para manejar la cadena de conexión
        public CN_Artista(IConfiguration configuration)
        {
            cdArtista = new CD_Artista(configuration);
        }

        // Método para obtener todos los artistas activos
        public List<E_Artista> ObtenerArtistas()
        {
            return cdArtista.ObtenerArtistas();
        }
        public E_Artista ObtenerDetalleArtista(int idArtista)
        {
            return cdArtista.ObtenerDetalleArtista(idArtista);
        }

        public int CrearArtista(E_Artista artista)
        {
            return cdArtista.InsertarArtista(artista);
        }
        public int InsertarArtista(E_Artista artista)
        {
            return cdArtista.InsertarArtista(artista);
        }

        public void EditarArtista(E_Artista artista)
        {
            cdArtista.EditarArtista(artista);
        }

        public void EliminarArtista(int idArtista)
        {
            cdArtista.EliminarArtista(idArtista);
        }


        public List<E_Roles> ObtenerRolesPorArtista(int idArtista)
        {
            return cdArtista.ObtenerRolesPorArtista(idArtista);
        }

        public int CrearArtistaConEspecialidades(E_Artista artista, List<int> especialidadesIds)
        {
            return cdArtista.CrearArtistaConEspecialidades(artista, especialidadesIds);
        }

        // Obtener especialidades asignadas a un artista
        public List<E_Especialidades> ObtenerEspecialidadesPorArtista(int idArtista)
        {
            return cdArtista.ObtenerEspecialidadesPorArtista(idArtista);
        }

        // Asignar especialidades a un artista
        public void AsignarEspecialidadesAArtista(int idArtista, List<int> especialidadesIds)
        {
            cdArtista.AsignarEspecialidadesAArtista(idArtista, especialidadesIds);
        }


        public List<E_Artista> ObtenerArtistasPendientes()
        {
            return cdArtista.ObtenerArtistasPendientes();
        }

        public void AprobarArtista(int idArtista)
        {
            cdArtista.AprobarArtista(idArtista);
        }

        public void RechazarArtista(int idArtista)
        {
            cdArtista.RechazarArtista(idArtista);
        }


        public List<E_Especialidades> ObtenerEspecialidades()
        {
            return cdArtista.ObtenerEspecialidades();
        }

        public void ActualizarEstadoArtista(int idArtista, bool nuevoEstado)
        {
            cdArtista.ActualizarEstadoArtista(idArtista, nuevoEstado);
        }

        public List<E_Artista> ObtenerTodosLosArtistas()
        {
            return cdArtista.ObtenerTodosLosArtistas();
        }
        // VISTA WEBAPP
        public List<E_Artista> ObtenerArtistas1()
        {
            return cdArtista.ObtenerArtistas1();
        }

        public E_Artista ObtenerDetalleArtista1(int idArtista)
        {
            return cdArtista.ObtenerDetalleArtista1(idArtista);
        }


        public List<E_Portafolio> ObtenerPortafolio(int idArtista)
        {
            return cdArtista.ObtenerPortafolio(idArtista);
        }



        public void GuardarImagenPerfil(int idArtista, string imagenPerfil)
        {
            cdArtista.GuardarImagenPerfil(idArtista, imagenPerfil);
        }

        public void ActualizarBio(int idArtista, string bio)
        {
            cdArtista.ActualizarBio(idArtista, bio);
        }


        public string ObtenerImagenPerfil(int idArtista)
        {
            return cdArtista.ObtenerImagenPerfil(idArtista);
        }







    }
}

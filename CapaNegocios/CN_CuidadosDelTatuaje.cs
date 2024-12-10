using CapaDatos;
using CapaEntidades;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace CapaNegocios
{
    public class CN_CuidadosDelTatuaje
    {
        private readonly CD_CuidadosDelTatuaje cdCuidados;

        public CN_CuidadosDelTatuaje(IConfiguration configuration)
        {
            cdCuidados = new CD_CuidadosDelTatuaje(configuration);
        }

        public List<E_CuidadosDelTatuaje> ObtenerCuidadosDelTatuaje()
        {
            return cdCuidados.ObtenerCuidadosDelTatuaje();
        }

        public int InsertarCuidadoDelTatuaje(E_CuidadosDelTatuaje cuidado)
        {
            return cdCuidados.InsertarCuidadoDelTatuaje(cuidado);
        }

        public void EditarCuidadoDelTatuaje(E_CuidadosDelTatuaje cuidado)
        {
            cdCuidados.EditarCuidadoDelTatuaje(cuidado);
        }

        public void EliminarCuidadoDelTatuaje(int idCuidadosDelTatuaje)
        {
            cdCuidados.EliminarCuidadoDelTatuaje(idCuidadosDelTatuaje);
        }

        public void EditarCuidadoDelTatuajeJefatura(E_CuidadosDelTatuaje cuidado)
        {
            cdCuidados.EditarCuidadoDelTatuajeJefatura(cuidado);
        }

    }
}

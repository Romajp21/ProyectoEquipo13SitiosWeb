using CapaEntidades;

public class CN_CategoriaTatuajes
{
    private readonly CD_CategoriaTatuajes _cdCategoria;

    public CN_CategoriaTatuajes(string connectionString)
    {
        _cdCategoria = new CD_CategoriaTatuajes(connectionString);
    }

    public List<E_CategoriaTatuajes> ObtenerCategorias()
    {
        return _cdCategoria.ObtenerCategorias();
    }

    public void InsertarCategoria(E_CategoriaTatuajes categoria)
    {
        _cdCategoria.InsertarCategoria(categoria);
    }

    public void EditarCategoria(E_CategoriaTatuajes categoria)
    {
        _cdCategoria.EditarCategoria(categoria);
    }
    public void EliminarCategoria(int idCategoria)
    {
        _cdCategoria.EliminarCategoria(idCategoria);
    }

    public void EditarCategoriaJefatura(E_CategoriaTatuajes categoria)
    {
        _cdCategoria.EditarCategoriaJefatura(categoria);
    }


}

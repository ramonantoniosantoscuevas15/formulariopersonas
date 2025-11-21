using FormularioPersonas.Entidades;

namespace FormularioPersonas.Repositorios
{
    public interface IRepositorioCategorias
    {
        Task Actualizar(Categoria categoria);
        Task Borrar(int id);
        Task<int> Crear(Categoria categoria);
        Task<bool> Existe(int id);
        Task<List<Categoria>> ObtenerTodos();
    }
}
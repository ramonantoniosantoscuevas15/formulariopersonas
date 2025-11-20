using FormularioPersonas.Entidades;

namespace FormularioPersonas.Repositorios
{
    public interface IRepositorioDirreciones
    {
        Task Actualizar(Dirreciones dirreciones);
        Task Borrar(int id);
        Task<int> Crear(Dirreciones dirreciones);
        Task<bool> Existe(int id);
        Task<Dirreciones?> ObtenerPorId(int id);
        Task<List<Dirreciones>> ObtenerTodos();
    }
}
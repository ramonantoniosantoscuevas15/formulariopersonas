using FormularioPersonas.Entidades;

namespace FormularioPersonas.Repositorios
{
    public interface IRepositorioDirreciones
    {
        Task Actualizar(Dirrecion dirreciones);
        Task Borrar(int id);
        Task<int> Crear(Dirrecion dirreciones);
        Task<bool> Existe(int id);
        Task<Dirrecion?> ObtenerPorId(int id);
        Task<List<Dirrecion>> ObtenerTodos(int personaId);
    }
}
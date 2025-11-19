using FormularioPersonas.Entidades;

namespace FormularioPersonas.Repositorios
{
    public interface IRepositorioPersonas
    {
        Task<List<Personas>> ObtenerTodos();
        Task<Personas?> ObtenerPorId(int id);
        Task<int> Crear(Personas personas);
        Task<bool>Existe(int id);
        Task Actualizar(Personas personas);
        Task Borrar(int id);   
    }
}

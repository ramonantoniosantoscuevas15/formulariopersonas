using FormularioPersonas.DTOs;
using FormularioPersonas.Entidades;

namespace FormularioPersonas.Repositorios
{
    public interface IRepositorioPersonas
    {
        Task<List<Personas>> ObtenerTodos(PaginacionDTO paginacionDTO);
        Task<Personas?> ObtenerPorId(int id);
        Task<int> Crear(Personas personas);
        Task<bool>Existe(int id);
        Task Actualizar(Personas personas);
        Task Borrar(int id);
        Task<List<Personas>> BusquedaPorNombre(string nombre);
    }
}

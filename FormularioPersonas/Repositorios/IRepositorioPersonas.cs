using FormularioPersonas.DTOs;
using FormularioPersonas.Entidades;

namespace FormularioPersonas.Repositorios
{
    public interface IRepositorioPersonas
    {
        Task<List<Persona>> ObtenerTodos(PaginacionDTO paginacionDTO);
        Task<Persona?> ObtenerPorId(int id);
        Task<int> Crear(Persona personas);
        Task<bool>Existe(int id);
        Task Actualizar(Persona personas);
        Task Borrar(int id);
        Task<List<Persona>> BusquedaPorNombre(string nombre);
    }
}

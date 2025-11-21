using FormularioPersonas.Entidades;

namespace FormularioPersonas.Repositorios
{
    public interface IRepositorioCorreos
    {
        Task Actualizar(Correos correos);
        Task Borrar(int id);
        Task<int> Crear(Correos correos);
        Task<bool> Existe(int id);
        Task<List<Correos>> ObtenerTodos();
    }
}
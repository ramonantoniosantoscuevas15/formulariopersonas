using FormularioPersonas.Entidades;

namespace FormularioPersonas.Repositorios
{
    public interface IRepositorioCorreos
    {
        Task Actualizar(Correo correos);
        Task Borrar(int id);
        Task<int> Crear(Correo correos);
        Task<bool> Existe(int id);
        Task<List<Correo>> ObtenerTodos();
    }
}
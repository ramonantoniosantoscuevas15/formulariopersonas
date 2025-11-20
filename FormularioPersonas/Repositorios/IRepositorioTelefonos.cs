using FormularioPersonas.Entidades;

namespace FormularioPersonas.Repositorios
{
    public interface IRepositorioTelefonos
    {
        Task Actualizar(Telefonos telefonos);
        Task<Telefonos?> ObtenerPorId(int id);
        Task Borrar(int id);
        Task<int> Crear(Telefonos telefonos);
        Task<bool> Existe(int id);
    }
}
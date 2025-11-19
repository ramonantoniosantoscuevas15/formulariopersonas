using FormularioPersonas.Entidades;

namespace FormularioPersonas.Repositorios
{
    public interface IRepositorioPersonas
    {
        Task<int> Crear(Personas personas);
    }
}

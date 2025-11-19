using FormularioPersonas.Entidades;

namespace FormularioPersonas.Repositorios
{
    public class RepositoriosPersonas : IRepositorioPersonas
    {
        private readonly AplicationDbContext context;

        public RepositoriosPersonas(AplicationDbContext context) 
        {
            this.context = context;
        }
        public async Task<int> Crear(Personas personas)
        {
            context.Add(personas);
            await context.SaveChangesAsync();
            return personas.Id;
        }
    }
}

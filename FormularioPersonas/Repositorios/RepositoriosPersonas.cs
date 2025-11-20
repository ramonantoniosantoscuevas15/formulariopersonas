using FormularioPersonas.Entidades;
using Microsoft.EntityFrameworkCore;

namespace FormularioPersonas.Repositorios
{
    public class RepositoriosPersonas : IRepositorioPersonas
    {
        private readonly AplicationDbContext context;

        public RepositoriosPersonas(AplicationDbContext context) 
        {
            this.context = context;
        }
        public async Task<bool> Existe(int id)
        {
            return await context.Personas.AnyAsync(p => p.Id == id);
        }

        public async Task<Personas?> ObtenerPorId(int id)
        {
            return await context.Personas.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Personas>> ObtenerTodos()
        {
            //orden de los nombres por apellido de forma decendente
            //return await context.Personas.OrderBy(p => p.Apellido).ToListAsync();
            return await context.Personas.OrderByDescending(p => p.Apellido).ToListAsync();
        }

        public async Task<int> Crear(Personas personas)
        {
            context.Add(personas);
            await context.SaveChangesAsync();
            return personas.Id;
        }
        public async Task Actualizar(Personas personas)
        {
            context.Update(personas);
            await context.SaveChangesAsync();

        }

        public async Task Borrar(int id)
        {
            await context.Personas.Where(p => p.Id == id).ExecuteDeleteAsync();
        }
    }
}

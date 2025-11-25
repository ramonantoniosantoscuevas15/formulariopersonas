using FormularioPersonas.Entidades;
using Microsoft.EntityFrameworkCore;

namespace FormularioPersonas.Repositorios
{
    public class RepositorioDirreciones : IRepositorioDirreciones
    {
        private readonly AplicationDbContext context;

        public RepositorioDirreciones(AplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Dirrecion>> ObtenerTodos(int personaId)
        {
            return await context.Dirrecion.Where(d=> d.PersonaId == personaId).ToListAsync();
        }

        public async Task<Dirrecion?> ObtenerPorId(int id)
        {
            return await context.Dirrecion.AsNoTracking().FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<int> Crear(Dirrecion dirreciones)
        {
            context.Add(dirreciones);
            await context.SaveChangesAsync();
            return dirreciones.Id;
        }

        public async Task<bool> Existe(int id)
        {
            return await context.Dirrecion.AnyAsync(d => d.Id == id);
        }

        public async Task Actualizar(Dirrecion dirreciones)
        {
            context.Update(dirreciones);
            await context.SaveChangesAsync();

        }

        public async Task Borrar(int id)
        {
            await context.Dirrecion.Where(d => d.Id == id).ExecuteDeleteAsync();
        }
    }
}

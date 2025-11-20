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

        public async Task<List<Dirreciones>> ObtenerTodos()
        {
            return await context.Dirreciones.OrderBy(d => d.Ciudad).ToListAsync();
        }

        public async Task<Dirreciones?> ObtenerPorId(int id)
        {
            return await context.Dirreciones.AsNoTracking().FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<int> Crear(Dirreciones dirreciones)
        {
            context.Add(dirreciones);
            await context.SaveChangesAsync();
            return dirreciones.Id;
        }

        public async Task<bool> Existe(int id)
        {
            return await context.Dirreciones.AnyAsync(d => d.Id == id);
        }

        public async Task Actualizar(Dirreciones dirreciones)
        {
            context.Update(dirreciones);
            await context.SaveChangesAsync();

        }

        public async Task Borrar(int id)
        {
            await context.Dirreciones.Where(d => d.Id == id).ExecuteDeleteAsync();
        }
    }
}

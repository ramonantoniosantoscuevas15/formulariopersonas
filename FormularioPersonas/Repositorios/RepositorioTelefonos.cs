using FormularioPersonas.Entidades;
using Microsoft.EntityFrameworkCore;

namespace FormularioPersonas.Repositorios
{
    public class RepositorioTelefonos : IRepositorioTelefonos
    {
        private readonly AplicationDbContext context;

        public RepositorioTelefonos(AplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<Telefonos?>ObtenerPorId(int id)
        {
            return await context.Telefonos.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<int> Crear(Telefonos telefonos)
        {
            context.Add(telefonos);
            await context.SaveChangesAsync();
            return telefonos.Id;
        }

        public async Task<bool> Existe(int id)
        {
            return await context.Telefonos.AnyAsync(t => t.Id == id);
        }

        public async Task Actualizar(Telefonos telefonos)
        {
            context.Update(telefonos);
            await context.SaveChangesAsync();
        }

        public async Task Borrar(int id)
        {
            await context.Telefonos.Where(t => t.Id == id).ExecuteDeleteAsync();
        }
    }
}

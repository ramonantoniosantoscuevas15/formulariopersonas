using FormularioPersonas.Entidades;
using Microsoft.EntityFrameworkCore;

namespace FormularioPersonas.Repositorios
{
    public class RepositorioCorreos : IRepositorioCorreos
    {
        private readonly AplicationDbContext context;

        public RepositorioCorreos(AplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<List<Correos>> ObtenerTodos()
        {
            return await context.Correos.ToListAsync();
        }

        public async Task<int> Crear(Correos correos)
        {
            context.Add(correos);
            await context.SaveChangesAsync();
            return correos.Id;
        }

        public async Task<bool> Existe(int id)
        {
            return await context.Correos.AnyAsync(c => c.Id == id);
        }

        public async Task Actualizar(Correos correos)
        {
            context.Update(correos);
            await context.SaveChangesAsync();
        }

        public async Task Borrar(int id)
        {
            await context.Correos.Where(c => c.Id == id).ExecuteDeleteAsync();
        }
    }
}

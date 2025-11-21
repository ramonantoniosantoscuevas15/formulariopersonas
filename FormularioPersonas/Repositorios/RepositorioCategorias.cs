using FormularioPersonas.Entidades;
using Microsoft.EntityFrameworkCore;

namespace FormularioPersonas.Repositorios
{
    public class RepositorioCategorias : IRepositorioCategorias
    {
        private readonly AplicationDbContext context;

        public RepositorioCategorias(AplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Categoria>> ObtenerTodos()
        {
            return await context.Categorias.ToListAsync();
        }

        public async Task<int> Crear(Categoria categoria)
        {
            context.Add(categoria);
            await context.SaveChangesAsync();
            return categoria.Id;
        }

        public async Task<bool> Existe(int id)
        {
            return await context.Categorias.AnyAsync(c => c.Id == id);
        }

        public async Task Actualizar(Categoria categoria)
        {
            context.Update(categoria);
            await context.SaveChangesAsync();
        }

        public async Task Borrar(int id)
        {
            await context.Categorias.Where(c => c.Id == id).ExecuteDeleteAsync();
        }
    }
}

using FormularioPersonas.DTOs;
using FormularioPersonas.Entidades;
using FormularioPersonas.Utilidades;
using Microsoft.EntityFrameworkCore;

namespace FormularioPersonas.Repositorios
{
    public class RepositoriosPersonas : IRepositorioPersonas
    {
        private readonly AplicationDbContext context;
        private readonly HttpContext httpContext;

        public RepositoriosPersonas(AplicationDbContext context,IHttpContextAccessor httpContextAccessor) 
        {
            this.context = context;
            httpContext = httpContextAccessor.HttpContext!;
        }
        public async Task<bool> Existe(int id)
        {
            return await context.Personas.AnyAsync(p => p.Id == id);
        }

        public async Task<Personas?> ObtenerPorId(int id)
        {
            return await context.Personas.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Personas>> ObtenerTodos(PaginacionDTO paginacionDTO)
        {
            //orden de los nombres por apellido de forma decendente
            //return await context.Personas.OrderBy(p => p.Apellido).ToListAsync();
            var queryable = context.Personas.AsQueryable();
            await httpContext.InsertarParametrosPaginacionEncabecera(queryable);
            return await queryable.OrderByDescending(p => p.Apellido).Paginar(paginacionDTO).ToListAsync();
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

        public async Task<List<Personas>> BusquedaPorNombre(string nombre)
        {
            return await context.Personas.Where(p => p.Nombre.Contains(nombre)).
                OrderBy(p => p.Nombre).ToListAsync();
        }

        
    }
}

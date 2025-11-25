using FormularioPersonas.Entidades;
using Microsoft.EntityFrameworkCore;

namespace FormularioPersonas
{
    public class AplicationDbContext : DbContext
    {
        public AplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           // modelBuilder.Entity<Personas>().Property(p => p.Nombre).HasMaxLength(50);
        }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Dirrecion> Dirrecion {  get; set; }
        public DbSet<Telefonos> Telefonos { get; set; } 
        public DbSet<Correo> Correos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        protected AplicationDbContext()
        {
        }
    }
}

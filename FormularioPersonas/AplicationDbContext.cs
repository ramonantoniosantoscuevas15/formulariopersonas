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
        public DbSet<Personas> Personas { get; set; }
        public DbSet<Dirreciones> Dirreciones {  get; set; }

        protected AplicationDbContext()
        {
        }
    }
}

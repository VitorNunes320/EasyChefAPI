using Domain.Entities;
using Domain.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Data.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Perfil> Perfis { get; set; }
        public DbSet<PerfilUsuario> PerfisUsuarios { get; set; }
        public DbSet<TokenUsuario> TokensUsuarios { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Mesa> Mesas { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<PedidoEntrega> PedidosEntregas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new PerfilUsuarioMapping());
        }
    }
}

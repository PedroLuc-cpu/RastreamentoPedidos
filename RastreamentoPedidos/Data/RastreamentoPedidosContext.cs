using Microsoft.EntityFrameworkCore;
using RastreamentoPedidos.Model;

namespace RastreamentoPedidos.Data
{
    public class RastreamentoPedidosContext : DbContext
    {
        public RastreamentoPedidosContext(DbContextOptions<RastreamentoPedidosContext> options) : base(options) { }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Encomenda> encomendas { get; set; }
        public DbSet<StatusEntrega> statusEntregas { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RastreamentoPedido.Core.Data;
using RastreamentoPedido.Core.Model;
using RastreamentoPedido.Core.Model.Clientes;
using RastreamentoPedidos.Model;
using RastreamentoPedidos.Model.Encomenda;

namespace RastreamentoPedidos.Data
{
    public class RastreamentoPedidosContext : IdentityDbContext<ApplicationUser>, IUnitOfWork
    {
        public RastreamentoPedidosContext(DbContextOptions<RastreamentoPedidosContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTrackingWithIdentityResolution;
            ChangeTracker.AutoDetectChangesEnabled = false;
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Encomendas>? encomendas { get; set; }
        public DbSet<StatusEntrega>? statusEntregas { get; set; }
        public DbSet<Endereco>? enderecos { get; set; }
        public DbSet<Cidade> cidades { get; set; }
        public DbSet<Telefone>? telefones { get; set; }
        public DbSet<TpLogradouro>? tpLogradouros { get; set; }
        public DbSet<UF>? uFs { get; set; }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}

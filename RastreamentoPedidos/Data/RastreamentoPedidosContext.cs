using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RastreamentoPedido.Core.Data;
using RastreamentoPedido.Core.Model.Clientes;
using RastreamentoPedido.Core.Model.Encomenda;
using RastreamentoPedidos.Model;

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
        public DbSet<EstadoCivil> EstadoCivil { get; set; }
        public DbSet<Encomendas>? Encomendas { get; set; }
        public DbSet<EncomendaAuditoria>? EncomendaAuditorias { get; set; }
        public DbSet<Rota>? Rotas { get; set; }
        public DbSet<PontoParada>? PontosParada { get; set; }
        public DbSet<StatusEntrega>? StatusEntregas { get; set; }
        public DbSet<Endereco>? Enderecos { get; set; }
        public DbSet<Cidade> Cidades { get; set; }
        public DbSet<Telefone>? Telefones { get; set; }
        public DbSet<TpLogradouro>? TpLogradouros { get; set; }
        public DbSet<UF>? UFs { get; set; }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}

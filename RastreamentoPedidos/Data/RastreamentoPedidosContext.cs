using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RastreamentoPedido.Core.Data;
using RastreamentoPedido.Core.Model.Encomenda;
using RastreamentoPedido.Core.Model.ProdutoModel;
using RastreamentoPedidos.Model;

namespace RastreamentoPedidos.API.Data
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

            modelBuilder.Entity<Produto>()
                .HasOne(p => p.ProdutoCategoria)
                .WithMany()
                .HasForeignKey(p => p.IdCategoria);
            modelBuilder.Entity<Produto>()
                .HasOne(p => p.ProdutoMarca)
                .WithMany()
                .HasForeignKey(p => p.IdMarca);
            modelBuilder.Entity<Produto>()
                .HasOne(p => p.ProdutoPeso)
                .WithMany()
                .HasForeignKey(p => p.Id);
            modelBuilder.Entity<Produto>()
                .HasOne(p => p.ProdutoEncargos)
                .WithMany()
                .HasForeignKey(p => p.Id);
        }
        public DbSet<StatusEncomenda>? StatusEncomendas { get; set; }
        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}

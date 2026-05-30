using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Categoria> Categorias => Set<Categoria>();
    public DbSet<Lojista> Lojistas => Set<Lojista>();
    public DbSet<Produto> Produtos => Set<Produto>();
    public DbSet<Avaliacao> Avaliacoes => Set<Avaliacao>();
    public DbSet<Pedido> Pedidos => Set<Pedido>();
    public DbSet<PedidoItem> PedidoItens => Set<PedidoItem>();
    public DbSet<ListaDesejosItem> ListaDesejosItens => Set<ListaDesejosItem>();
    public DbSet<ComissaoCategoria> ComissoesCategoria => Set<ComissaoCategoria>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Produto>()
            .HasOne(p => p.Lojista)
            .WithMany(l => l.Produtos)
            .HasForeignKey(p => p.LojistaId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Produto>()
            .HasOne(p => p.CategoriaRelacionada)
            .WithMany(c => c.Produtos)
            .HasForeignKey(p => p.CategoriaId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.Entity<Produto>().Property(p => p.Preco).HasPrecision(10, 2);
        builder.Entity<Categoria>().Property(c => c.PercentualComissao).HasPrecision(5, 2);
        builder.Entity<ComissaoCategoria>().Property(c => c.Percentual).HasPrecision(5, 2);
        builder.Entity<Pedido>().Property(p => p.Subtotal).HasPrecision(10, 2);
        builder.Entity<Pedido>().Property(p => p.Frete).HasPrecision(10, 2);
        builder.Entity<Pedido>().Property(p => p.Total).HasPrecision(10, 2);
        builder.Entity<PedidoItem>().Property(p => p.PrecoUnitario).HasPrecision(10, 2);
        builder.Entity<PedidoItem>().Property(p => p.PercentualComissao).HasPrecision(5, 2);
        builder.Entity<PedidoItem>().Property(p => p.ValorComissao).HasPrecision(10, 2);
        builder.Entity<PedidoItem>().Property(p => p.ValorRepasseLojista).HasPrecision(10, 2);

        builder.Entity<ListaDesejosItem>()
            .HasIndex(i => new { i.UsuarioEmail, i.ProdutoId })
            .IsUnique();
    }
}

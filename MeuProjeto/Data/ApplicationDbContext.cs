// Importa namespaces necessários para o Entity Framework Core e os modelos do projeto
using Microsoft.EntityFrameworkCore;
using MeuProjeto.Models;

namespace MeuProjeto.Data
{
    // Define a classe ApplicationDbContext que herda de DbContext
    public class ApplicationDbContext : DbContext
    {
        // Define uma propriedade DbSet para a entidade Pedido
        public DbSet<Pedido> Pedidos { get; set; }
        // Define uma propriedade DbSet para a entidade Produto
        public DbSet<Produto> Produtos { get; set; }
        // Define uma propriedade DbSet para a entidade Cliente
        public DbSet<Cliente> Clientes { get; set; }

        // Construtor que recebe opções de configuração para o contexto
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Método que configura a criação do modelo usando o ModelBuilder
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Chama o método base OnModelCreating
            base.OnModelCreating(modelBuilder);

            // Configuração da entidade Pedido
            modelBuilder.Entity<Pedido>(entity =>
            {
                // Define a chave primária da entidade
                entity.HasKey(p => p.Id);
                // Define que a propriedade Documento é obrigatória
                entity.Property(p => p.Documento).IsRequired();
                // Define que a propriedade NomeRazaoSocial é obrigatória
                entity.Property(p => p.NomeRazaoSocial).IsRequired();
                // Define que a propriedade Cep é obrigatória
                entity.Property(p => p.Cep).IsRequired();
                // Define que a propriedade Produto é obrigatória
                entity.Property(p => p.Produto).IsRequired();
                // Define que a propriedade NumeroDoPedido é obrigatória
                entity.Property(p => p.NumeroDoPedido).IsRequired();
                // Define que a propriedade Data é obrigatória
                entity.Property(p => p.Data).IsRequired();
                // Define que a propriedade DataEntrega é obrigatória
                entity.Property(p => p.DataEntrega).IsRequired();
                // Define que a propriedade ValorFinal é obrigatória
                entity.Property(p => p.ValorFinal).IsRequired();
                // Define que a propriedade Regiao é obrigatória
                entity.Property(p => p.Regiao).IsRequired();
            });

            // Configuração da entidade Produto
            modelBuilder.Entity<Produto>(entity =>
            {
                // Define a chave primária da entidade
                entity.HasKey(p => p.Id);
                // Define que a propriedade Nome é obrigatória
                entity.Property(p => p.Nome).IsRequired();
                // Define que a propriedade Preco é obrigatória
                entity.Property(p => p.Preco).IsRequired();
            });

            // Configuração da entidade Cliente
            modelBuilder.Entity<Cliente>(entity =>
            {
                // Define a chave primária da entidade
                entity.HasKey(c => c.CpfCnpj);
                // Define que a propriedade CpfCnpj é obrigatória e define o comprimento máximo
                entity.Property(c => c.CpfCnpj)
                      .IsRequired()
                      .HasMaxLength(14);
                // Define que a propriedade NomeRazaoSocial é obrigatória
                entity.Property(c => c.NomeRazaoSocial).IsRequired();
            });

            // Adiciona dados iniciais (seed data) para a entidade Produto
            modelBuilder.Entity<Produto>().HasData(
                new Produto { Id = 1, Nome = "Celular", Preco = 1000 },
                new Produto { Id = 2, Nome = "Notebook", Preco = 3000 },
                new Produto { Id = 3, Nome = "Televisão", Preco = 5000 }
            );
        }
    }
}

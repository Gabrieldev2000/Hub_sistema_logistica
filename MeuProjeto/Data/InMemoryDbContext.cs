// Importa o namespace Microsoft.EntityFrameworkCore, que é necessário para trabalhar com o Entity Framework Core.
using Microsoft.EntityFrameworkCore;
// Importa o namespace MeuProjeto.Models, que contém as classes de modelo do projeto.
using MeuProjeto.Models;

namespace MeuProjeto.Data
{
    // Define uma classe InMemoryDbContext que herda de DbContext, representando o contexto do banco de dados.
    public class InMemoryDbContext : DbContext
    {
        // Define uma propriedade DbSet para a entidade Pedido, representando a coleção de pedidos no banco de dados.
        public DbSet<Pedido> Pedidos { get; set; }
        // Define uma propriedade DbSet para a entidade Produto, representando a coleção de produtos no banco de dados.
        public DbSet<Produto> Produtos { get; set; }
        // Define uma propriedade DbSet para a entidade Cliente, representando a coleção de clientes no banco de dados.
        public DbSet<Cliente> Clientes { get; set; }

        // Construtor da classe InMemoryDbContext que recebe opções específicas do contexto e as passa para a classe base DbContext.
        public InMemoryDbContext(DbContextOptions<InMemoryDbContext> options)
            : base(options)
        {
        }
    }
}

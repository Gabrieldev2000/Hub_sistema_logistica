// Importa namespaces necessários para o Entity Framework Core e configuração
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace MeuProjeto.Data
{
    // Define uma fábrica de contexto de banco de dados para uso em tempo de design
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        // Método para criar uma instância do contexto de banco de dados
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            // Cria uma configuração raiz a partir do arquivo appsettings.json
            IConfigurationRoot configuration = new ConfigurationBuilder()
                // Define o diretório base para a busca do arquivo de configuração
                .SetBasePath(Directory.GetCurrentDirectory())
                // Adiciona o arquivo de configuração appsettings.json
                .AddJsonFile("appsettings.json")
                // Constrói a configuração
                .Build();

            // Cria um construtor de opções para o contexto de banco de dados
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            // Obtém a string de conexão do arquivo de configuração
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            // Configura o construtor para usar o SQLite com a string de conexão obtida
            builder.UseSqlite(connectionString);

            // Retorna uma nova instância do contexto de banco de dados com as opções configuradas
            return new ApplicationDbContext(builder.Options);
        }
    }
}

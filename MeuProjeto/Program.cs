// Importa os namespaces necessários para a configuração do Entity Framework Core.
using Microsoft.EntityFrameworkCore;
// Importa o namespace do projeto para o contexto do banco de dados.
using MeuProjeto.Data;
// Importa o namespace dos serviços do projeto.
using MeuProjeto.Services;

var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços ao contêiner de dependência.
builder.Services.AddControllers();

// Configuração do CORS (Cross-Origin Resource Sharing).
builder.Services.AddCors(options =>
{
    // Adiciona uma política de CORS permitindo origens específicas.
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://localhost:3000") // Permite solicitações apenas desta origem.
                          .AllowAnyHeader() // Permite qualquer cabeçalho.
                          .AllowAnyMethod()); // Permite qualquer método HTTP (GET, POST, etc.).
});

// Adiciona o contexto do banco de dados SQLite apontando para "database.db".
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=database.db"));

// Adiciona o contexto do banco de dados em memória.
builder.Services.AddDbContext<InMemoryDbContext>(options =>
    options.UseInMemoryDatabase("InMemoryDb"));

// Adiciona serviços ao contêiner de dependência.
builder.Services.AddTransient<IViaCepService, ViaCepService>(); // Adiciona o serviço ViaCepService com a interface IViaCepService.
builder.Services.AddTransient<PedidoService>(); // Adiciona o serviço PedidoService.

// Adiciona HttpClient para IViaCepService.
builder.Services.AddHttpClient<IViaCepService, ViaCepService>();

// Configuração do logging.
builder.Services.AddLogging(config =>
{
    config.AddConsole(); // Adiciona provedor de logging para console.
    config.AddDebug(); // Adiciona provedor de logging para debug.
    config.SetMinimumLevel(LogLevel.Information); // Define o nível mínimo de log (pode ser ajustado conforme necessário: Trace, Debug, Information, Warning, Error, Critical).
});

// Configuração do Swagger para documentação da API.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configura o pipeline de solicitação HTTP.
if (app.Environment.IsDevelopment())
{
    // Usa a página de exceção de desenvolvedor para mostrar detalhes das exceções.
    app.UseDeveloperExceptionPage();
    // Usa o Swagger e a interface do usuário Swagger.
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Ativa o CORS usando a política definida anteriormente.
app.UseCors("AllowSpecificOrigin");

// Usa redirecionamento HTTPS para garantir que as solicitações sejam feitas por HTTPS.
app.UseHttpsRedirection();

// Usa roteamento para a definição dos endpoints.
app.UseRouting();

// Usa a autorização para proteger os endpoints.
app.UseAuthorization();

// Mapeia os controladores para os endpoints.
app.MapControllers();

// Inicia a aplicação.
app.Run();

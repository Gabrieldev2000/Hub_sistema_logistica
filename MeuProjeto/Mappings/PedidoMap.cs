// Importa o namespace CsvHelper.Configuration, necessário para configurar mapeamentos de classe para arquivos CSV.
using CsvHelper.Configuration;
// Importa o namespace MeuProjeto.Models, que contém as classes de modelo do projeto.
using MeuProjeto.Models;

// Define a classe PedidoMap que herda de ClassMap<Pedido>, usada para mapear propriedades da classe Pedido para colunas do CSV.
public class PedidoMap : ClassMap<Pedido>
{
    // Construtor da classe PedidoMap onde os mapeamentos são configurados.
    public PedidoMap()
    {
        // Mapeia a propriedade Documento da classe Pedido para a coluna "Documento" no CSV.
        Map(m => m.Documento).Name("Documento");
        // Mapeia a propriedade NomeRazaoSocial da classe Pedido para a coluna "Razão Social" no CSV.
        Map(m => m.NomeRazaoSocial).Name("Razão Social");
        // Mapeia a propriedade Cep da classe Pedido para a coluna "CEP" no CSV.
        Map(m => m.Cep).Name("CEP");
        // Mapeia a propriedade Produto da classe Pedido para a coluna "Produto" no CSV.
        Map(m => m.Produto).Name("Produto");
        // Mapeia a propriedade NumeroDoPedido da classe Pedido para a coluna "Numero do pedido" no CSV.
        Map(m => m.NumeroDoPedido).Name("Numero do pedido");
        // Mapeia a propriedade Data da classe Pedido para a coluna "Data" no CSV e define o formato da data como "dd/MM/yyyy".
        Map(m => m.Data).Name("Data").TypeConverterOption.Format("dd/MM/yyyy");
    }
}

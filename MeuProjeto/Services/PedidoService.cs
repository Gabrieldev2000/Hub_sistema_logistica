// Importa os namespaces necessários para o funcionamento do serviço.
using MeuProjeto.Data;
using MeuProjeto.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace MeuProjeto.Services
{
    // Define a classe PedidoService que gerencia os pedidos.
    public class PedidoService
    {
        // Declara uma variável para o contexto do banco de dados.
        private readonly ApplicationDbContext _context;
        // Declara uma variável para o serviço de obtenção de endereço via CEP.
        private readonly IViaCepService _viaCepService;

        // Construtor que inicializa o contexto de banco de dados e o serviço de CEP.
        public PedidoService(ApplicationDbContext context, IViaCepService viaCepService)
        {
            _context = context;
            _viaCepService = viaCepService;
        }

        // Método para processar uma lista de pedidos.
        public async Task ProcessarPedidosAsync(List<Pedido> pedidos)
        {
            // Itera sobre cada pedido na lista.
            foreach (var pedido in pedidos)
            {
                // Obtém o endereço a partir do CEP usando o serviço de CEP.
                var endereco = await _viaCepService.ObterEnderecoAsync(pedido.Cep);
                // Define a região do pedido com base no endereço obtido.
                pedido.Regiao = endereco.Regiao;
                // Calcula o valor final do pedido incluindo o frete.
                pedido.ValorFinal = CalcularValorFinal(pedido.Produto, endereco.Regiao);
                // Calcula a data de entrega do pedido.
                pedido.DataEntrega = CalcularDataEntrega(pedido.Data, endereco.Regiao);
            }

            // Atualiza os pedidos no contexto do banco de dados.
            _context.Pedidos.UpdateRange(pedidos);
            // Salva as alterações no banco de dados.
            await _context.SaveChangesAsync();
        }

        // Método para obter todos os pedidos do banco de dados.
        public async Task<List<Pedido>> ObterTodosPedidosAsync()
        {
            // Retorna a lista de pedidos do banco de dados.
            return await _context.Pedidos.ToListAsync();
        }

        // Método para calcular o valor final do pedido com base no produto e na região.
        private decimal CalcularValorFinal(string produto, string regiao)
        {
            // Define o valor do produto com base no seu tipo.
            decimal valorProduto = produto switch
            {
                "Celular" => 1000m,
                "Notebook" => 3000m,
                "Televisão" => 5000m,
                _ => 0m
            };

            // Calcula o valor do frete com base na região.
            decimal frete = regiao switch
            {
                "Norte" => valorProduto * 0.30m,
                "Nordeste" => valorProduto * 0.30m,
                "Centro-Oeste" => valorProduto * 0.20m,
                "Sul" => valorProduto * 0.20m,
                "Sudeste" => valorProduto * 0.10m,
                "São Paulo" => 0m, // Frete gratuito para São Paulo Capital.
                _ => 0m
            };

            // Retorna o valor total do produto mais o frete.
            return valorProduto + frete;
        }

        // Método para calcular a data de entrega do pedido com base na data do pedido e na região.
        private DateTime CalcularDataEntrega(DateTime dataPedido, string regiao)
        {
            // Define a quantidade de dias para a entrega com base na região.
            int dias = regiao switch
            {
                "Norte" => 10,
                "Nordeste" => 10,
                "Centro-Oeste" => 5,
                "Sul" => 5,
                "Sudeste" => 1,
                "São Paulo" => 0, // Entrega no mesmo dia para São Paulo Capital.
                _ => 0
            };

            // Retorna a data de entrega calculada adicionando os dias úteis ou corridos à data do pedido.
            return AdicionarDias(dataPedido, dias, regiao);
        }

        // Método auxiliar para adicionar dias úteis ou corridos a uma data.
        private DateTime AdicionarDias(DateTime data, int dias, string regiao)
        {
            DateTime dataEntrega = data;
            
            // Para Sudeste e São Paulo, adicionamos dias corridos.
            if (regiao == "Sudeste" || regiao == "São Paulo")
            {
                dataEntrega = dataEntrega.AddDays(dias);
            }
            else
            {
                // Para outras regiões, adicionamos apenas dias úteis.
                int diasAdicionados = 0;
                while (diasAdicionados < dias)
                {
                    dataEntrega = dataEntrega.AddDays(1);
                    // Incrementa o contador de dias úteis apenas se o dia não for sábado ou domingo.
                    if (dataEntrega.DayOfWeek != DayOfWeek.Saturday && dataEntrega.DayOfWeek != DayOfWeek.Sunday)
                    {
                        diasAdicionados++;
                    }
                }
            }

            // Retorna a data de entrega calculada.
            return dataEntrega;
        }
    }
}

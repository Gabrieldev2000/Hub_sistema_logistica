// Importa os namespaces necessários para o funcionamento do serviço.
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using MeuProjeto.Models;
using System;

namespace MeuProjeto.Services
{
    // Define a classe ViaCepService que implementa a interface IViaCepService para obter endereços a partir de um CEP.
    public class ViaCepService : IViaCepService
    {
        // Declara uma variável para o cliente HTTP.
        private readonly HttpClient _httpClient;
        // Declara uma variável para o logger.
        private readonly ILogger<ViaCepService> _logger;

        // Construtor que inicializa o cliente HTTP e o logger.
        public ViaCepService(HttpClient httpClient, ILogger<ViaCepService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        // Método assíncrono para obter um endereço a partir de um CEP.
        public async Task<Endereco> ObterEnderecoAsync(string cep)
        {
            // Registra uma informação de log indicando o início da busca pelo CEP.
            _logger.LogInformation($"Iniciando busca pelo CEP: {cep}");

            // Faz uma solicitação GET para a API ViaCep com o CEP fornecido.
            var response = await _httpClient.GetAsync($"https://viacep.com.br/ws/{cep}/json/");
            // Garante que a resposta tenha sido bem-sucedida, caso contrário lança uma exceção.
            response.EnsureSuccessStatusCode();

            // Lê o corpo da resposta como uma string.
            var responseBody = await response.Content.ReadAsStringAsync();
            // Registra uma informação de log com a resposta da API ViaCep.
            _logger.LogInformation($"Resposta da API ViaCep: {responseBody}");

            // Desserializa o corpo da resposta para um objeto do tipo Endereco.
            var endereco = JsonConvert.DeserializeObject<Endereco>(responseBody);

            // Verifica se o objeto endereço é nulo ou se alguns de seus campos essenciais estão vazios.
            if (endereco == null || string.IsNullOrEmpty(endereco.Cep) || 
                string.IsNullOrEmpty(endereco.Localidade) || 
                string.IsNullOrEmpty(endereco.Uf))
            {
                // Registra um erro de log se a resposta for inválida ou incompleta.
                _logger.LogError($"Resposta inválida ou incompleta para o CEP: {cep}");
                // Lança uma exceção indicando que a resposta foi inválida para o CEP fornecido.
                throw new Exception($"Resposta inválida para o CEP: {cep}");
            }

            // Adiciona lógica para determinar a região com base na UF (Unidade Federativa).
            endereco.Regiao = DeterminarRegiao(endereco.Uf);

            // Retorna o objeto endereço.
            return endereco;
        }

        // Método privado para determinar a região com base na UF (Unidade Federativa).
        private string DeterminarRegiao(string uf)
        {
            // Esta lógica pode ser expandida para incluir todas as UFs e suas respectivas regiões.
            return uf switch
            {
                "SP" => "Sudeste",
                "RJ" => "Sudeste",
                "MG" => "Sudeste",
                "ES" => "Sudeste",
                "PR" => "Sul",
                "SC" => "Sul",
                "RS" => "Sul",
                "DF" => "Centro-Oeste",
                "GO" => "Centro-Oeste",
                "MT" => "Centro-Oeste",
                "MS" => "Centro-Oeste",
                "BA" => "Nordeste",
                "SE" => "Nordeste",
                "AL" => "Nordeste",
                "PE" => "Nordeste",
                "PB" => "Nordeste",
                "RN" => "Nordeste",
                "CE" => "Nordeste",
                "PI" => "Nordeste",
                "MA" => "Nordeste",
                "PA" => "Norte",
                "AP" => "Norte",
                "RR" => "Norte",
                "AM" => "Norte",
                "AC" => "Norte",
                "RO" => "Norte",
                "TO" => "Norte",
                _ => throw new Exception("UF desconhecida")
            };
        }
    }
}

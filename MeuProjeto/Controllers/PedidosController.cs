// Importa namespaces para os modelos, serviços, e bibliotecas necessárias
using MeuProjeto.Models;
using MeuProjeto.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using CsvHelper;
using CsvHelper.Configuration;
using OfficeOpenXml;

namespace MeuProjeto.Controllers
{
    // Indica que esta classe é um controlador de API
    [ApiController]
    // Define a rota base para este controlador
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        // Declara uma variável para o serviço de pedidos
        private readonly PedidoService _pedidoService;
        // Declara uma variável para o logger
        private readonly ILogger<PedidosController> _logger;

        // Construtor que injeta o serviço de pedidos e o logger
        public PedidosController(PedidoService pedidoService, ILogger<PedidosController> logger)
        {
            _pedidoService = pedidoService;
            _logger = logger;
        }

        // Define um método HTTP POST para importar pedidos a partir de um arquivo
        [HttpPost("import")]
        public async Task<IActionResult> Import(IFormFile file)
        {
            // Verifica se o arquivo foi enviado
            if (file == null || file.Length == 0)
            {
                return BadRequest("Arquivo não enviado.");
            }

            // Lista para armazenar os pedidos importados
            var pedidos = new List<Pedido>();

            try
            {
                // Obtém a extensão do arquivo enviado
                var extension = Path.GetExtension(file.FileName).ToLower();

                // Verifica se a extensão do arquivo é CSV
                if (extension == ".csv")
                {
                    // Cria um stream de memória para o arquivo
                    using (var stream = new MemoryStream())
                    {
                        // Copia o arquivo para o stream de memória
                        await file.CopyToAsync(stream);
                        // Reseta a posição do stream para o início
                        stream.Position = 0;
                        // Lê o arquivo usando um StreamReader
                        using (var reader = new StreamReader(stream))
                        {
                            // Configurações para o CsvHelper
                            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
                            {
                                Delimiter = ",",
                                HasHeaderRecord = true,
                                MissingFieldFound = null,
                                HeaderValidated = null,
                                BadDataFound = null
                            };

                            // Cria um CsvReader para ler o arquivo CSV
                            using (var csv = new CsvReader(reader, csvConfig))
                            {
                                // Define os formatos de data esperados
                                csv.Context.TypeConverterOptionsCache.GetOptions<DateTime>().Formats = new[] { "dd/MM/yyyy", "d/M/yyyy", "yyyy-MM-dd" };

                                // Lê o cabeçalho do CSV
                                csv.Read();
                                csv.ReadHeader();
                                // Lê cada linha do CSV e adiciona à lista de pedidos
                                while (csv.Read())
                                {
                                    var pedido = new Pedido
                                    {
                                        Documento = csv.GetField<string>("Documento"),
                                        NomeRazaoSocial = csv.GetField<string>("Razão Social"),
                                        Cep = csv.GetField<string>("CEP"),
                                        Produto = csv.GetField<string>("Produto"),
                                        NumeroDoPedido = csv.GetField<int>("Número do pedido"),
                                        Data = csv.GetField<DateTime>("Data")
                                    };
                                    pedidos.Add(pedido);
                                }
                            }
                        }
                    }
                }
                // Verifica se a extensão do arquivo é XLSX (Excel)
                else if (extension == ".xlsx")
                {
                    // Cria um stream de memória para o arquivo
                    using (var stream = new MemoryStream())
                    {
                        // Copia o arquivo para o stream de memória
                        await file.CopyToAsync(stream);
                        // Usa a biblioteca EPPlus para ler o arquivo Excel
                        using (var package = new ExcelPackage(stream))
                        {
                            var worksheet = package.Workbook.Worksheets[0];
                            var rowCount = worksheet.Dimension.Rows;

                            // Lê cada linha da planilha e adiciona à lista de pedidos
                            for (int row = 2; row <= rowCount; row++)
                            {
                                var pedido = new Pedido
                                {
                                    Documento = worksheet.Cells[row, 1].Text,
                                    NomeRazaoSocial = worksheet.Cells[row, 2].Text,
                                    Cep = worksheet.Cells[row, 3].Text,
                                    Produto = worksheet.Cells[row, 4].Text,
                                    NumeroDoPedido = int.Parse(worksheet.Cells[row, 5].Text),
                                    Data = DateTime.Parse(worksheet.Cells[row, 6].Text)
                                };
                                pedidos.Add(pedido);
                            }
                        }
                    }
                }
                // Se a extensão do arquivo não for suportada, retorna um erro
                else
                {
                    return BadRequest("Formato de arquivo não suportado.");
                }

                // Processa os pedidos utilizando o serviço de pedidos
                await _pedidoService.ProcessarPedidosAsync(pedidos);
                return Ok("Pedidos importados com sucesso.");
            }
            catch (Exception ex)
            {
                // Loga o erro e retorna um status 500 com a mensagem de erro
                _logger.LogError(ex, "Erro ao importar pedidos.");
                return StatusCode(500, $"Erro ao importar pedidos: {ex.Message}");
            }
        }

        // Define um método HTTP GET para obter todos os pedidos
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                // Obtém todos os pedidos utilizando o serviço de pedidos
                var pedidos = await _pedidoService.ObterTodosPedidosAsync();
                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                // Loga o erro e retorna um status 500 com a mensagem de erro
                _logger.LogError(ex, "Erro ao obter pedidos.");
                return StatusCode(500, $"Erro ao obter pedidos: {ex.Message}");
            }
        }
    }
}

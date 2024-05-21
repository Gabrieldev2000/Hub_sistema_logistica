// Importa o namespace System.Threading.Tasks, necessário para trabalhar com tarefas assíncronas.
using System.Threading.Tasks;

namespace MeuProjeto.Services
{
    // Define uma interface IViaCepService que representa um serviço para obter endereços usando o CEP.
    public interface IViaCepService
    {
        // Declara um método assíncrono ObterEnderecoAsync que recebe um CEP como parâmetro e retorna um objeto Endereco.
        Task<Endereco> ObterEnderecoAsync(string cep);
    }
}

// Importa o namespace System.ComponentModel.DataAnnotations, necessário para usar atributos de validação de dados.
using System.ComponentModel.DataAnnotations;

namespace MeuProjeto.Models
{
    // Define a classe Cliente que representa um cliente no sistema.
    public class Cliente
    {
        // Atributo Key indica que a propriedade CpfCnpj é a chave primária da entidade.
        [Key]
        // Atributo MaxLength define um tamanho máximo de 14 caracteres para a propriedade CpfCnpj.
        [MaxLength(14)]
        // Propriedade CpfCnpj que armazena o CPF ou CNPJ do cliente.
        public string CpfCnpj { get; set; }

        // Atributo Required indica que a propriedade NomeRazaoSocial é obrigatória.
        [Required]
        // Propriedade NomeRazaoSocial que armazena o nome ou razão social do cliente.
        public string NomeRazaoSocial { get; set; }
    }
}

// Importa o namespace System, necessário para usar tipos básicos como DateTime.
using System;
// Importa o namespace System.ComponentModel.DataAnnotations, necessário para usar atributos de validação de dados.
using System.ComponentModel.DataAnnotations;

namespace MeuProjeto.Models
{
    // Define a classe Pedido que representa um pedido no sistema.
    public class Pedido
    {
        // Atributo Key indica que a propriedade Id é a chave primária da entidade.
        [Key]
        // Propriedade Id que armazena o identificador único do pedido.
        public int Id { get; set; }

        // Atributo Required indica que a propriedade Documento é obrigatória.
        [Required]
        // Propriedade Documento que armazena o documento do cliente relacionado ao pedido.
        public string Documento { get; set; }

        // Atributo Required indica que a propriedade NomeRazaoSocial é obrigatória.
        [Required]
        // Propriedade NomeRazaoSocial que armazena o nome ou razão social do cliente.
        public string NomeRazaoSocial { get; set; }

        // Atributo Required indica que a propriedade Cep é obrigatória.
        [Required]
        // Propriedade Cep que armazena o CEP do endereço de entrega do pedido.
        public string Cep { get; set; }

        // Atributo Required indica que a propriedade Produto é obrigatória.
        [Required]
        // Propriedade Produto que armazena a descrição do produto solicitado.
        public string Produto { get; set; }

        // Atributo Required indica que a propriedade NumeroDoPedido é obrigatória.
        [Required]
        // Propriedade NumeroDoPedido que armazena o número do pedido.
        public int NumeroDoPedido { get; set; }

        // Atributo Required indica que a propriedade Data é obrigatória.
        [Required]
        // Propriedade Data que armazena a data do pedido.
        public DateTime Data { get; set; }
        
        // Atributo Required indica que a propriedade Regiao é obrigatória.
        [Required]
        // Propriedade Regiao que armazena a região do pedido.
        public string Regiao { get; set; }
        
        // Propriedade ValorFinal que armazena o valor final do pedido.
        public decimal ValorFinal { get; set; }
        
        // Propriedade DataEntrega que armazena a data de entrega do pedido.
        public DateTime DataEntrega { get; set; }
    }
}

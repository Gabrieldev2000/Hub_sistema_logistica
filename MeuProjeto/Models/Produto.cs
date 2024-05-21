// Importa o namespace System.ComponentModel.DataAnnotations, necessário para usar atributos de validação de dados.
using System.ComponentModel.DataAnnotations;

namespace MeuProjeto.Models
{
    // Define a classe Produto que representa um produto no sistema.
    public class Produto
    {
        // Atributo Key indica que a propriedade Id é a chave primária da entidade.
        [Key]
        // Propriedade Id que armazena o identificador único do produto.
        public int Id { get; set; }

        // Atributo Required indica que a propriedade Nome é obrigatória.
        [Required]
        // Propriedade Nome que armazena o nome do produto.
        public string Nome { get; set; }

        // Atributo Required indica que a propriedade Preco é obrigatória.
        [Required]
        // Propriedade Preco que armazena o preço do produto.
        public decimal Preco { get; set; }
    }
}

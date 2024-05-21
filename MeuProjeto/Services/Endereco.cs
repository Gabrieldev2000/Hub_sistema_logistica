namespace MeuProjeto.Services
{
    // Define a classe Endereco que representa um endereço no sistema.
    public class Endereco
    {
        // Propriedade Cep que armazena o código postal do endereço.
        public string Cep { get; set; }
        
        // Propriedade Logradouro que armazena o nome da rua ou avenida do endereço.
        public string Logradouro { get; set; }
        
        // Propriedade Complemento que armazena informações adicionais sobre o endereço, como número, bloco, etc.
        public string Complemento { get; set; }
        
        // Propriedade Bairro que armazena o nome do bairro do endereço.
        public string Bairro { get; set; }
        
        // Propriedade Localidade que armazena a cidade do endereço.
        public string Localidade { get; set; }
        
        // Propriedade Uf que armazena a sigla do estado do endereço.
        public string Uf { get; set; }
        
        // Propriedade Ibge que armazena o código IBGE do município.
        public string Ibge { get; set; }
        
        // Propriedade Gia que armazena o código GIA (Guia de Informação e Apuração do ICMS) do município.
        public string Gia { get; set; }
        
        // Propriedade Ddd que armazena o código de Discagem Direta a Distância do telefone do endereço.
        public string Ddd { get; set; }
        
        // Propriedade Siafi que armazena o código SIAFI (Sistema Integrado de Administração Financeira) do município.
        public string Siafi { get; set; }
        
        // Propriedade Regiao que armazena a região do endereço.
        // Verifique se há apenas esta definição para 'Regiao' para evitar duplicidade.
        public string Regiao { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CadastroCliente.Models
{
    public class Fornecedor
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Nome { get; set; } = "";
        public long Cnpj { get; set; }
        public string Segmento { get; set; }
        public int Cep { get; set; }
        public string Endereco { get; set; } = "";
        public string Bairro { get; set; } = "";
        public string Numero { get; set; } = "";
        public string Localidade { get; set; } = "";
        public string Uf { get; set; } = "";
        public string Imagem { get; set; } = "";
    }
}

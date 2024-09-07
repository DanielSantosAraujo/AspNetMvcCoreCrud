using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CadastroCliente.Models
{
    public class FornecedorDto
    {
        [Required(ErrorMessage = "O campo nome é obrigatório.")]
        public string Nome { get; set; } = "";
        [Required(ErrorMessage = "O campo cnpj é obrigatório.")]
        public long Cnpj { get; set; }
        [Required(ErrorMessage = "O campo segmento é obrigatório.")]
        public string Segmento { get; set; }
        [Required(ErrorMessage = "O campo cep é obrigatório.")]
        public int Cep { get; set; }
        [Required(ErrorMessage = "O campo endereço é obrigatório.")]
        public string Endereco { get; set; }
        [Required(ErrorMessage = "O campo bairro é obrigatório.")]
        public string Bairro { get; set; } = "";
        [Required(ErrorMessage = "O campo numero é obrigatório.")]
        public string Numero { get; set; } = "";
        [Required(ErrorMessage = "O campo cidade é obrigatório.")]
        public string Localidade { get; set; }
        [Required(ErrorMessage = "O campo estado é obrigatório.")]
        public string Uf { get; set; }
        public IFormFile? ImagemFile { get; set; }
    }
}

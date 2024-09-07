using CadastroCliente.Models;
using CadastroCliente.Services;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Runtime.ConstrainedExecution;
using SelectList;

namespace CadastroCliente.Controllers
{
    public class FornecedoresController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment ambiente;

        public FornecedoresController(AppDbContext context, IWebHostEnvironment ambiente)
        {
            _context = context;
            this.ambiente = ambiente;
        }
        public IActionResult Index()
        {
            var fornecedores = _context.Fornecedores.ToList();
            return View(fornecedores);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(FornecedorDto fornecedorDto)
        {

           

            if (fornecedorDto.ImagemFile == null)
            {
                ModelState.AddModelError("ImagemFile", "A imagem é requerida");
            }

            if (!ModelState.IsValid)
            {
                return View(fornecedorDto);
            }

            //Salva arquivo imagem
            string novoArquivo = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            novoArquivo += Path.GetExtension(fornecedorDto.ImagemFile!.FileName);

            string imagemCaminho = ambiente.WebRootPath + "/fotos/" + novoArquivo;
            using (var stream = System.IO.File.Create(imagemCaminho))
            {
                fornecedorDto.ImagemFile.CopyTo(stream);
            }

            var segmentosData = Segmentos.GetAll();
            Console.WriteLine(segmentosData);

            //salvando novo fornecedor no banco de dados;
            Fornecedor fornecedor = new Fornecedor()
            {
                Nome = fornecedorDto.Nome,
                Cnpj = fornecedorDto.Cnpj,
                Segmento = fornecedorDto.Segmento,
                Cep = fornecedorDto.Cep,
                Endereco = fornecedorDto.Endereco,
                Bairro = fornecedorDto.Bairro,
                Numero = fornecedorDto.Numero,
                Localidade = fornecedorDto.Localidade,
                Uf = fornecedorDto.Uf,
                Imagem = novoArquivo,
            };

            _context.Fornecedores.Add(fornecedor);
            _context.SaveChanges();

            return RedirectToAction("Index", "Fornecedores");
        }

        public IActionResult Edit(int id)
        {
            var fornecedor = _context.Fornecedores.Find(id);
            if (fornecedor == null)
            {
                return RedirectToAction("Index", "Fornecedores");
            }

            var fornecedorDto = new FornecedorDto()
            {
                Nome = fornecedor.Nome,
                Cnpj = fornecedor.Cnpj,
                Segmento = fornecedor.Segmento,
                Cep = fornecedor.Cep,
                Endereco = fornecedor.Endereco,
                Bairro = fornecedor.Bairro,
                Numero = fornecedor.Numero,
                Localidade = fornecedor.Localidade,
                Uf = fornecedor.Uf,
            };

            ViewData["FornecedorId"] = fornecedor.Id;
            ViewData["Imagem"] = fornecedor.Imagem;


            return View(fornecedorDto);
        }

        [HttpPost]
        public IActionResult Edit(int id, FornecedorDto fornecedorDto)
        {
            var fornecedor = _context.Fornecedores.Find(id);
            if (fornecedor == null)
            {
                return RedirectToAction("Index", "Fornecedores");
            }

            if (!ModelState.IsValid)
            {
                ViewData["FornecedorId"] = fornecedor.Id;
                ViewData["Imagem"] = fornecedor.Imagem;
                return View(fornecedorDto);
            }

            //atualizando a imagem se tiver uma nova imagem
            string novoArquivo = fornecedor.Imagem;
            if(fornecedorDto.ImagemFile != null)
            {
                novoArquivo = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                novoArquivo += Path.GetExtension(fornecedorDto.ImagemFile.FileName);

                string imagemCaminho = ambiente.WebRootPath + "/fotos/" + novoArquivo;
                using (var stream = System.IO.File.Create(imagemCaminho))
                {
                    fornecedorDto.ImagemFile.CopyTo(stream);
                }

                string imagemVelha = ambiente.WebRootPath + "/fotos/" + fornecedor.Imagem;
                System.IO.File.Delete(imagemVelha);
            }

            //Atualizando o fornecedor no banco de dados;
            fornecedor.Nome = fornecedorDto.Nome;
            fornecedor.Cnpj = fornecedorDto.Cnpj;
            fornecedor.Segmento = fornecedorDto.Segmento;
            fornecedor.Cep = fornecedorDto.Cep;
            fornecedor.Endereco = fornecedorDto.Endereco;
            fornecedor.Bairro = fornecedorDto.Bairro;
            fornecedor.Numero = fornecedorDto.Numero;
            fornecedor.Localidade = fornecedorDto.Localidade;
            fornecedor.Uf = fornecedorDto.Uf;
            fornecedor.Imagem = novoArquivo;

            _context.SaveChanges();

            return RedirectToAction("Index", "Fornecedores");

        }

        public IActionResult Delete(int id) 
        {
            var fornecedor = _context.Fornecedores.Find(id);
            if (fornecedor == null)
            {
                return RedirectToAction("Index", "Fornecedores");
            }

            string imagemDeletada = ambiente.WebRootPath + "/fotos/" + fornecedor.Imagem;
            System.IO.File.Delete(imagemDeletada);

            _context.Fornecedores.Remove(fornecedor);
            _context.SaveChanges(true);

            return RedirectToAction("Index", "Fornecedores");

        }
    }
}

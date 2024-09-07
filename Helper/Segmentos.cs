using CadastroCliente.Models.DominioModels;

namespace SelectList
{
    public static class Segmentos
    {
        public static List<Segmento> GetAll()
        {
            return new List<Segmento>
            {
                new Segmento() { Id = "comercio", Nome="Comercio"},
                new Segmento() { Id = "servico", Nome="Serviço"},
                new Segmento() { Id = "industria", Nome="Indústria"}
            };
        }
    }
}

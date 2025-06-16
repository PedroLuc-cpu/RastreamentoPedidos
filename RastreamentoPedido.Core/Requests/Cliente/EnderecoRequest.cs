

namespace RastreamentoPedido.Core.Requests.Cliente
{
    public class EnderecoRequest
    {
        public long IdCliente { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public int IdCidade { get; set; }
        public string Cep { get; set; }
        public EnderecoRequest(string logradouro, string numero, string complemento, string bairro, int idCidade, string cep)
        {
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            IdCidade = idCidade;
            Cep = cep;
        }
    }
}

using RastreamentoPedido.Core.DomainObjects;

namespace RastreamentoPedido.Core.Model.Clientes
{
    public class Endereco : IAggregateRoot
    {
        public long idEnderecoCliente { get; set; }
        public TpLogradouro TpLogradouro { get; set; } = new TpLogradouro();
        public long idTpLogradouro { get; set; }
        public string Complemento { get; set; } = string.Empty;
        public string Bairro { get; set; } = string.Empty;
        public string Numero {  get; set; } = string.Empty;
        public string Rua {  get; set; } = string.Empty;
        public string CEP {  get; set; } = string.Empty;
        public int idCliente { get; set; }
        public Cidade Cidade {  get; set; } = new Cidade();
        public int EncomendaId { get; set; }
    }
}

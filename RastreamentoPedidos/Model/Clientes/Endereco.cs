using RastreamentoPedidos.DomainObjects;

namespace RastreamentoPedidos.Model.Clientes
{
    //[Table("endereco_cliente")]
    public class Endereco : IAggregateRoot
    {
        //[Key]
        public long idEnderecoCliente { get; set; }
        public TpLogradouro TpLogradouro { get; set; } = new TpLogradouro();
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

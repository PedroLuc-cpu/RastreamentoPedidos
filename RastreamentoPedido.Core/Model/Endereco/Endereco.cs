using RastreamentoPedido.Core.DomainObjects;
using RastreamentoPedido.Core.Model.Clientes;
using System.ComponentModel.DataAnnotations.Schema;

namespace RastreamentoPedido.Core.Model.Endereco
{
    [NotMapped]
    public class Enderecos : IAggregateRoot
    {
        public int Id { get; set; }
        public int IdPessoa { get; set; }
        public int IdTpLogradouro { get; set; }
        public int IdCidade { get; set; }
        public int IdEncomenda { get; set; }
        public Cidade Cidade { get; set; } = new Cidade();
        public TpLogradouro TpLogradouro { get; set; } = new TpLogradouro();
        public string Complemento { get; set; } = string.Empty;
        public string Bairro { get; set; } = string.Empty;
        public string Numero {  get; set; } = string.Empty;
        public string Rua {  get; set; } = string.Empty;
        public string CEP {  get; set; } = string.Empty;
    }
}

using RastreamentoPedido.Core.DomainObjects;

namespace RastreamentoPedido.Core.Model.Clientes
{
    //[Table("tp_logradouro")]
    public class TpLogradouro : IAggregateRoot
    {
        //[Key]
        public int IdTpLogradouro { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Sigla {  get; set; } = string.Empty;
    }
}

using RastreamentoPedido.Core.DomainObjects;
using System.ComponentModel.DataAnnotations;

namespace RastreamentoPedido.Core.Model.Clientes
{
    public class TpLogradouro : IAggregateRoot
    {
        [Key]
        public int IdTpLogradouro { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Sigla {  get; set; } = string.Empty;
    }
}

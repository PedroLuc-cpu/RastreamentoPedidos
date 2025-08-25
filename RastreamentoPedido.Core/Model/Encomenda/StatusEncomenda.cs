using RastreamentoPedido.Core.DomainObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace RastreamentoPedido.Core.Model.Encomenda
{
    [Table("statusEncomenda")]
    public class StatusEncomenda : IAggregateRoot
    {
        public int Id { get; set; }
        public string Status { get; set; } = string.Empty;
        public ICollection<Encomendas> Encomendas { get; set; } = new List<Encomendas>();
    }
}

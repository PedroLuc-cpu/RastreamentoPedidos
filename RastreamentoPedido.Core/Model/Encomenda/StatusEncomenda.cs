using RastreamentoPedido.Core.DomainObjects;

namespace RastreamentoPedido.Core.Model.Encomenda
{
    public class StatusEncomenda : IAggregateRoot
    {
        public int Id { get; set; }
        public string Status { get; set; } = string.Empty;
        public ICollection<Encomendas> Encomendas { get; set; } = new List<Encomendas>();
    }
}

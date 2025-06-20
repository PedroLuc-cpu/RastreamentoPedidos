using RastreamentoPedido.Core.DomainObjects;

namespace RastreamentoPedido.Core.Model.Encomenda
{
    public class StatusEncomenda : IAggregateRoot
    {
        public int Codigo { get; set; }
        public ICollection<Encomendas> Encomendas { get; set; } = new List<Encomendas>();
        public string Status { get; set; } = string.Empty;
    }
}

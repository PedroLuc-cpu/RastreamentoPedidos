using RastreamentoPedidos.DomainObjects;

namespace RastreamentoPedidos.Model
{
    public class StatusEntrega : IAggregateRoot
    {
        public int id_status_entrega { get; set; }
        public Encomenda encomenda { get; set; } = new Encomenda();
        public string status { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
    }
}

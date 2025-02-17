using RastreamentoPedidos.DomainObjects;

namespace RastreamentoPedidos.Model.DTO
{
    public class StatusEntregaDTO : IAggregateRoot
    {
        public int id_status_entrega { get; set; }
        public string status { get; set; } = string.Empty;

        public IList<Endereco> endereco = new List<Endereco>();
        public DateTime Timestamp { get; set; }
    }
}

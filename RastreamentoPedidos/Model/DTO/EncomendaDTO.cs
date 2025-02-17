using System.Collections.ObjectModel;
using RastreamentoPedidos.DomainObjects;

namespace RastreamentoPedidos.Model.DTO
{
    public class EncomendaDTO: IAggregateRoot
    {
        public int? id_encomenda { get; set; }
        public DateTime data_encomenda { get; set; }
        public string descricao { get; set; } = string.Empty;
        public ICollection<StatusEntrega> statusEntregas { get; set; } = new Collection<StatusEntrega>();
    }
}

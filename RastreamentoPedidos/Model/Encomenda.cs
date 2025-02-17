using System.Collections.ObjectModel;
using RastreamentoPedidos.DomainObjects;

namespace RastreamentoPedidos.Model
{
    public class Encomenda : IAggregateRoot
    {
        public int? id_encomenda { get; set; }
        public int id_cliente { get; set; }
        public Cliente cliente { get; set; } = new Cliente();
        public DateTime data_encomenda { get; set; }
        public string descricao { get; set; } = string.Empty;
        public ICollection<StatusEntrega>? statusEntregas { get; set; } = new Collection<StatusEntrega>();
    }
}

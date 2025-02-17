using System.Collections.ObjectModel;
using RastreamentoPedidos.DomainObjects;

namespace RastreamentoPedidos.Model.DTO
{
    public class ClienteDto : IAggregateRoot
    {
        public int id_cliente { get; set; }
        public string nome { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string telefone { get; set; } = string.Empty;
        public ICollection<EncomendaDTO>? encomendas { get; set; } = new Collection<EncomendaDTO>();
    }
}

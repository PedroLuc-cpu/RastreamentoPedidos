
using RastreamentoPedido.Core.DomainObjects;

namespace RastreamentoPedido.Core.Model.Clientes
{
    public class EstadoCivil : IAggregateRoot
    {
        public int Id { get; set; } = 0;
        public string EstadoCivilDescricao { get; set; } = string.Empty;
    }
}

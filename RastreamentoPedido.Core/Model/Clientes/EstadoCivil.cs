using RastreamentoPedido.Core.DomainObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace RastreamentoPedido.Core.Model.Clientes
{
    [NotMapped]
    public class EstadoCivil : IAggregateRoot
    {
        public int Id { get; set; } = 0;
        public string EstadoCivilDescricao { get; set; } = string.Empty;
    }
}

using RastreamentoPedido.Core.DomainObjects;

namespace RastreamentoPedido.Core.Model.Clientes
{
    public class UF : IAggregateRoot
    {
        public int idUF { get; set; }
        public string sigla { get; set; } = string.Empty;
    }
}

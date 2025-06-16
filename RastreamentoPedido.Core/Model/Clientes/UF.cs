using RastreamentoPedido.Core.DomainObjects;

namespace RastreamentoPedido.Core.Model.Clientes
{
    public class UF : IAggregateRoot
    {
        public int IdUF { get; set; }
        public string Sigla { get; set; } = string.Empty;
    }
}

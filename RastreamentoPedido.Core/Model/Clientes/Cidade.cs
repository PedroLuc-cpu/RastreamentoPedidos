using RastreamentoPedido.Core.DomainObjects;

namespace RastreamentoPedido.Core.Model.Clientes
{
    public class Cidade : IAggregateRoot
    {
        public int IdCidade { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int IdUF { get; set; }
        public UF UF { get; set; } = new UF();
    }
}

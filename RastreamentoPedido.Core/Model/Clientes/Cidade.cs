using RastreamentoPedido.Core.DomainObjects;

namespace RastreamentoPedido.Core.Model.Clientes
{
    public class Cidade : IAggregateRoot
    {
        public long idCidade { get; set; }
        public string nome { get; set; } = string.Empty;
        public int idUF { get; set; }
        public UF UF { get; set; } = new UF();
    }
}

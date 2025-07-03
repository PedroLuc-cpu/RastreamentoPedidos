using RastreamentoPedido.Core.DomainObjects;

namespace RastreamentoPedido.Core.Model.Endereco
{
    public class Pais : IAggregateRoot
    {
        public int IdPais { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Sigla { get; set; } = string.Empty;
        public string Cod_bcb { get; set; } = string.Empty;
    }
}

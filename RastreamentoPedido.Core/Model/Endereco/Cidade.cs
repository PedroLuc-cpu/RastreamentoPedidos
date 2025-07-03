using RastreamentoPedido.Core.DomainObjects;

namespace RastreamentoPedido.Core.Model.Endereco
{
    public class Cidade : IAggregateRoot
    {
        public int IdCidade { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Cep { get; set; } = string.Empty;
        public int IdUF { get; set; }
        public UF UF { get; set; } = new UF();
        public string CodIbge { get; set; } = string.Empty;
        
    }
}

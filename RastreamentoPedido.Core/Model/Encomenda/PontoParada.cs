using RastreamentoPedido.Core.DomainObjects;

namespace RastreamentoPedido.Core.Model.Encomenda
{
    public class PontoParada : IAggregateRoot
    {
        public int Id { get; set; }
        public int RotaId { get; set; }
        public Rota Rota { get; set; } = new();
        public string Nome { get; set; } = string.Empty;
        public string Localizacao { get; set; } = string.Empty;
        public int Ordem { get; set; }
    }
}

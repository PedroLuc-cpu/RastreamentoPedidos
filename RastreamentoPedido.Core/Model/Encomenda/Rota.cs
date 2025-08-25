using RastreamentoPedido.Core.DomainObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace RastreamentoPedido.Core.Model.Encomenda
{
    [NotMapped]
    public class Rota : IAggregateRoot
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public ICollection<PontoParada> PontosParada { get; set; } = new List<PontoParada>();
        public ICollection<Encomendas> Encomendas { get; set; } = new List<Encomendas>();
    }
}

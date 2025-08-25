using RastreamentoPedido.Core.DomainObjects;
using RastreamentoPedido.Core.Model.Clientes;
using System.ComponentModel.DataAnnotations.Schema;

namespace RastreamentoPedido.Core.Model.Encomenda
{
    [NotMapped]
    public class Encomendas : IAggregateRoot
    {
        public int Id { get; set; }
        public string CodigoRastreamento { get; set; } = string.Empty;
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; } = new();
        public DateTime DataEncomenda { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public int StatusEncomendaId { get; set; }
        public StatusEncomenda StatusEncomenda { get; set; } = new();
        public DateTime DataCriacao { get; set; }
        public DateTime DataPrevisao { get; set; }
        public int RotaId { get; set; }
        public Rota Rota { get; set; } = new();
        public ICollection<EncomendaAuditoria> Auditorias { get; set; } = new List<EncomendaAuditoria>();
    }
}

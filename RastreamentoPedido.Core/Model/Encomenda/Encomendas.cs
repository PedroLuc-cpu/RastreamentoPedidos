using RastreamentoPedido.Core.DomainObjects;
using RastreamentoPedido.Core.Model.Clientes;

namespace RastreamentoPedido.Core.Model.Encomenda
{
    public class Encomendas : IAggregateRoot
    {
        public int Id { get; set; }
        public string CodigoRastreamento { get; set; } = string.Empty;
        public int IdCliente { get; set; }
        public Cliente Cliente { get; set; } = new Cliente();
        public DateTime DataEncomenda { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public int IdStatusEncomenda { get; set; }
        public StatusEncomenda StatusEncomenda { get; set; } = new StatusEncomenda();
        public DateTime DataCriacao { get; set; }
        public DateTime DataPrevisao { get; set; }
        public int IdRota { get; set; }
        public Rota Rota { get; set; } = new Rota();
        public int IdEncomendaAuditoria { get; set; }
        public ICollection<EncomendaAuditoria> EncomendaAuditorias { get; set; } = new List<EncomendaAuditoria>();
    }
}

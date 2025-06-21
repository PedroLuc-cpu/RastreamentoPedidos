using RastreamentoPedido.Core.DomainObjects;

namespace RastreamentoPedido.Core.Model.Encomenda
{
    public class EncomendaAuditoria : IAggregateRoot
    {
        public int Id { get; set; }
        public int EncomendaId { get; set; }
        public Encomendas Encomenda { get; set; } = new();
        public DateTime DataHoraEvento { get; set; }
        public string LocalOrigem { get; set; } = string.Empty;
        public string LocalDestino { get; set; } = string.Empty;
        public string StatusEntrega { get; set; } = string.Empty;
        public string StatusAtual { get; set; } = string.Empty;
        public string DescricaoEvento { get; set; } = string.Empty;
        public string Responsavel { get; set; } = string.Empty;
        public string Observacoes { get; set; } = string.Empty;
        public DateTime DataRegistro { get; set; }
    }
}

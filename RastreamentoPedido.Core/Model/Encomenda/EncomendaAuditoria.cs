using RastreamentoPedido.Core.DomainObjects;

namespace RastreamentoPedido.Core.Model.Encomenda
{
    public class EncomendaAuditoria : IAggregateRoot
    {
        public int Id { get; set; }
        public int IdEncomenda { get; set; }
        public DateTime DataHoraEvento {  get; set; } = DateTime.UtcNow;
        public string LocalOrigem { get; set; } = string.Empty;
        public string LocalDestino { get; set; } = string.Empty;
        public string StatusEntregas { get; set; } = StatusEntrega.StatusEncomendaEnumToStr(StatusEntregaEnum.AguardandoPagamento);
        public string StatusAtual { get; set; } = string.Empty;
        public string DescricaoEvento { get; set; } = string.Empty;
        public string Responsavel { get; set; } = string.Empty;
        public string Observacoes { get; set; } = string.Empty;
        public DateTime DataRegistro { get; set; } = DateTime.UtcNow;
        public EncomendaAuditoria()
        {
            DataHoraEvento = DateTime.UtcNow;
            StatusEntregas = StatusEntrega.StatusEncomendaEnumToStr(StatusEntregaEnum.AguardandoPagamento);
            DataRegistro = DateTime.UtcNow;
        }




    }
}

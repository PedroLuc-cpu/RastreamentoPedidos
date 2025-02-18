using RastreamentoPedidos.DomainObjects;
using RastreamentoPedidos.Model.Encomenda;

namespace RastreamentoPedidos.Model
{
    public enum StatusEntregaEnum
    {
        AguardandoPagamento = 0,
        PagamentoConfirmado = 1,
        ProcessandoPedido = 2,
        EnviadoParaTransportadora = 3,
        EmTransito = 4,
        SaiuParaEntrega = 5,
        Entregue = 6,
        TentativaDeEntrega = 7,
        AguardandoRetirada = 8,
        Cancelado = 9,
        Devolvido = 10,
        Extraviado = 11
    }
    public class StatusEntrega : IAggregateRoot
    {
        public int codigo { get; set; } = 0;
        public string status { get; set; } = StatusEncomendaEnumToStr(StatusEntregaEnum.AguardandoPagamento);
        public string decricao { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public Encomendas encomenda { get; set; } = new Encomendas();
        public static string StatusEncomendaEnumToStr(StatusEntregaEnum value)
        {
            switch (value)
            {
                case StatusEntregaEnum.AguardandoPagamento:
                    return "O pagamento ainda não foi confirmado.";
                case StatusEntregaEnum.PagamentoConfirmado:
                    return "O pagamento foi recebido e confirmado.";
                case StatusEntregaEnum.ProcessandoPedido:
                    return "A encomenda está sendo separada e embalada.";
                case StatusEntregaEnum.EnviadoParaTransportadora:
                    return "A encomenda foi entregue à transportadora";
                case StatusEntregaEnum.EmTransito:
                    return "A encomenda está a caminho do destinatário.";
                case StatusEntregaEnum.SaiuParaEntrega:
                    return "O entregador está a caminho para entregar a encomenda";
                case StatusEntregaEnum.Entregue:
                    return "A encomenda foi entregue ao destinatário.";
                case StatusEntregaEnum.TentativaDeEntrega:
                    return "Foi realizada uma tentativa de entrega, mas não houve sucesso";
                case StatusEntregaEnum.AguardandoRetirada:
                    return "A encomenda está aguardando retirada em um ponto específico.";
                case StatusEntregaEnum.Cancelado:
                    return "O pedido foi cancelado pelo cliente ou pela loja";
                case StatusEntregaEnum.Devolvido:
                    return "O destinatário recusou a entrega ou houve problemas no transporte.";
                case StatusEntregaEnum.Extraviado:
                    return "A encomenda foi perdida durante o transporte.";
                default:
                    return "";
            }
        }
        public static StatusEntregaEnum StrToStatusEncomendaEnum(string value)
        {
            switch (value)
            {
                case "O pagamento ainda não foi confirmado.":
                    return StatusEntregaEnum.AguardandoPagamento;
                case "O pagamento foi recebido e confirmado.":
                    return StatusEntregaEnum.PagamentoConfirmado;
                case "A encomenda está sendo separada e embalada.":
                    return StatusEntregaEnum.ProcessandoPedido;
                case "A encomenda foi entregue à transportadora":
                    return StatusEntregaEnum.EnviadoParaTransportadora;
                case "A encomenda está a caminho do destinatário.":
                    return StatusEntregaEnum.EmTransito;
                case "O entregador está a caminho para entregar a encomenda":
                    return StatusEntregaEnum.SaiuParaEntrega;
                case "A encomenda foi entregue ao destinatário.":
                    return StatusEntregaEnum.Entregue;
                case "Foi realizada uma tentativa de entrega, mas não houve sucesso":
                    return StatusEntregaEnum.TentativaDeEntrega;
                case "A encomenda está aguardando retirada em um ponto específico.":
                    return StatusEntregaEnum.AguardandoRetirada;
                case "O pedido foi cancelado pelo cliente ou pela loja":
                    return StatusEntregaEnum.Cancelado;
                case "O destinatário recusou a entrega ou houve problemas no transporte.":
                    return StatusEntregaEnum.Devolvido;
                case "A encomenda foi perdida durante o transporte.":
                    return StatusEntregaEnum.Extraviado;
                default:
                    return StatusEntregaEnum.AguardandoPagamento;
            }
        }
    }
}

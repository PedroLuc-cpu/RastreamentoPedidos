namespace RastreamentoPedido.Domain.Common.Enum
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
}

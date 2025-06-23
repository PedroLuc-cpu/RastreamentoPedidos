namespace RastreamentoPedido.Core.Model.Produto
{
    public class ProdutoEncargos
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public double ValorFrete { get; set; } = 0.00;
        public double ValorSeguro { get; set; } = 0.00;
        public double ValorDespesas { get; set; } = 0.00;
        public double ValorOutros { get; set; } = 0.00;
        public double ValorTotal => ValorFrete + ValorSeguro + ValorDespesas + ValorOutros;
    }
}

namespace RastreamentoPedido.Core.Model.Produto
{
    public class ProdutoPeso
    {
        public int Id { get; set; }
        public int IdProduto { get; set; }
        public double PesoBruto { get; set; } = 0.00;
        public double PesoLiquido { get; set; } = 0.00;
    }
}

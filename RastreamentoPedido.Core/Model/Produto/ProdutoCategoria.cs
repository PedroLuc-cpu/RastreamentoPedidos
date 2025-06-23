namespace RastreamentoPedido.Core.Model.Produto
{
    public class ProdutoCategoria
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public string Nome { get; set; } = string.Empty;
    }
}

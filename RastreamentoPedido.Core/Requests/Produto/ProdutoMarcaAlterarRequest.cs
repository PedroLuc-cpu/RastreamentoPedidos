namespace RastreamentoPedido.Core.Requests.Produto
{
    public class ProdutoMarcaAlterarRequest
    {
        public int IdMarca { get; set; }
        public string Nome { get; set; } = string.Empty;
    }
}

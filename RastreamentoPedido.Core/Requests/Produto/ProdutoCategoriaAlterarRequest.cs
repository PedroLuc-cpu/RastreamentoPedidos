namespace RastreamentoPedido.Core.Requests.Produto
{
    public class ProdutoCategoriaAlterarRequest
    {
        public int IdCategoria { get; set; }
        public string Nome { get; set; } = string.Empty;
    }
}

using RastreamentoPedido.Core.Model.ProdutoModel;

namespace RastreamentoPedido.Core.Repositories.Produtos
{
    public interface IProdutoRepository
    {
        Task<Produto> Inserir(Produto produto);
        Task<Produto> Alterar(Produto produto);
        Task<Produto> CarregarPorId(int id);
        Task<Produto> CarregarPorCodigo(string codigo);
        Task<Produto> CarregarPorNome(string nome);
        Task<IList<Produto>> ListarTodos(int pagina = 1, int tamanhoPagina = 25);
    }
}

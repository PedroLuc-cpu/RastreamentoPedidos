using RastreamentoPedido.Core.Model.ProdutoModel;

namespace RastreamentoPedido.Core.Repositories.Produtos
{
    public interface IProdutoCategoriaRepository
    {
        Task<ProdutoCategoria> Inserir(ProdutoCategoria produtoCategoria);
        Task<ProdutoCategoria> Alterar(ProdutoCategoria produtoCategoria);
        Task<ProdutoCategoria> CarregarPorId(int id);
        Task<ProdutoCategoria> CarregarPorNome(string nome);
        Task<IList<ProdutoCategoria>> ListarTodos();
    }
}

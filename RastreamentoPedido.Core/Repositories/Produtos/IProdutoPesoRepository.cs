using RastreamentoPedido.Core.Model.ProdutoModel;

namespace RastreamentoPedido.Core.Repositories.Produtos
{
    public interface IProdutoPesoRepository
    {
        Task<ProdutoPeso> Inserir(ProdutoPeso produtoPeso);
        Task<ProdutoPeso> Alterar(ProdutoPeso produtoPeso);
        Task<ProdutoPeso> CarregarPorId(int id);
        Task<ProdutoPeso> CarregarPorIdProduto(int idProduto);
        Task<IList<ProdutoPeso>> ListarTodos(int idProduto);
    }
}

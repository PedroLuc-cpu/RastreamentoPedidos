using RastreamentoPedido.Core.Model.ProdutoModel;

namespace RastreamentoPedido.Core.Repositories.Produtos
{
    public interface IProdutoEncargoRepository
    {
        Task<ProdutoEncargos> Inserir(ProdutoEncargos produtoEncargo);
        Task<ProdutoEncargos> Alterar(ProdutoEncargos produtoEncargo);
        Task<ProdutoEncargos> CarregarPorIdProduto(int id);
        Task<IList<ProdutoEncargos>> ListarPorIdProduto(int id);
        Task<ProdutoEncargos> CarregarPorId(int id);
    }
}

using RastreamentoPedido.Core.Model.ProdutoModel;

namespace RastreamentoPedido.Core.Repositories.Produtos
{
    public interface IProdutoMarcaRepository
    {
        Task<ProdutoMarca> Inserir(ProdutoMarca produtoMarca);
        Task<ProdutoMarca> Alterar(ProdutoMarca produtoMarca);
        Task<ProdutoMarca> CarregarPorId(int id);
        Task<ProdutoMarca> CarregarPorNome(string nome);
        Task<IList<ProdutoMarca>> ListarTodos();
    }
}

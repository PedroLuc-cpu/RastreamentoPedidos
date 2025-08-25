using RastreamentoPedido.Core.Model.Produto;

namespace RastreamentoPedido.Core.Repositories.Produtos
{
    public interface IProdutoPesoRepository
    {
        Task<ProdutoPeso> Inserir(ProdutoPeso produtoPeso);
        Task<ProdutoPeso> Alterar(ProdutoPeso produtoPeso);
        Task<ProdutoPeso> CarregarPorId(int id);
        Task<ProdutoPeso> CarregarPorNome(string nome);
        Task<IList<ProdutoPeso>> ListarTodos();
    }
}

using RastreamentoPedido.Core.Model.ProdutoModel;

namespace RastreamentoPedido.Core.Repositories.Produtos
{
    public interface IProdutoRepository
    {
        Task<ProdutoModel> Inserir(ProdutoModel produto);
        Task<ProdutoModel> Alterar(ProdutoModel produto);
        Task<ProdutoModel> CarregarPorId(int id);
        Task<ProdutoModel> CarregarPorCodigo(string codigo);
        Task<ProdutoModel> CarregarPorCodigoBarra(string codigoBarra);
        Task<ProdutoModel> CarregarPorNome(string nome);
        Task<IList<ProdutoModel>> ListarTodos(int pagina, int tamanhoPagina, string nome, bool ativo);
    }
}

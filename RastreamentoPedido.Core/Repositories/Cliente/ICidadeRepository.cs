using RastreamentoPedido.Core.Model.Endereco;

namespace RastreamentoPedido.Core.Repositories.Clientes
{
    public interface ICidadeRepository : IRepository<Cidade>
    {
        Task<Cidade> CarregarPorId(int id);
        Task<IList<Cidade>> CarregarPorUF(string uf);
        Task<IList<Cidade>> CarregarPorIdUf(int idUf);
        Task<IList<Cidade>> CarregarTodos();
    }
}

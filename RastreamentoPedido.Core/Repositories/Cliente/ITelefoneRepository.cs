using RastreamentoPedido.Core.Model.Clientes;

namespace RastreamentoPedido.Core.Repositories.Clientes
{
    public interface ITelefoneRepository : IRepository<Telefone>
    {
        Task<IList<Telefone>> CarregarPorIdCliente(int idCliente);
        Task<Telefone> ObterTelefonePorIdCliente(int idCliente);
        Task<Telefone> ObterTelefonePorIdTelefone(int idTelefoneCliente);
        Task<Telefone> AtualizarTelefone(Telefone telefone);
        Task<Telefone> AdicionarTelefone(Telefone telefone);
    }
}

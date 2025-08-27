using RastreamentoPedido.Core.Model.Endereco;

namespace RastreamentoPedido.Core.Repositories.Clientes
{
    public interface IEnderecoRepository : IRepository<Enderecos>
    {
        Task<Enderecos> Inserir(Enderecos endereco);
        Task<Enderecos> Alterar(Enderecos endereco);
        Task<IList<Enderecos>> CarregarPorIdCliente(int idCliente);       
    }
}

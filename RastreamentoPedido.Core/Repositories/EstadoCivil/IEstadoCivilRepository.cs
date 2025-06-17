using RastreamentoPedido.Core.Model.Clientes;

namespace RastreamentoPedido.Core.Repositories.IEstadoCivilRepository
{
    public interface IEstadoCivilRepository : IRepository<EstadoCivil>
    {
        Task<EstadoCivil> AdicionarEstadoCivil(EstadoCivil estadoCivil);
        Task<EstadoCivil> AtualizarEstadoCivil(EstadoCivil estadoCivil);
        Task<EstadoCivil> CarregarEstadoCivilPorId(int id);
        Task<EstadoCivil> CarregarEstadoCivilPorDescricao(string descricao);
        Task<IEnumerable<EstadoCivil>> CarregarTodosEstadosCivis();
        Task<bool> DeletarEstadoCivil(int id);
    }
}

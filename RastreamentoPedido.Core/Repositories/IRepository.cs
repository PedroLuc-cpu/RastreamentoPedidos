using RastreamentoPedido.Core.DomainObjects;

namespace RastreamentoPedido.Core.Repositories
{
    public interface IRepository<T> where T : IAggregateRoot
    {
    }
}

using RastreamentoPedidos.DomainObjects;

namespace RastreamentoPedidos.Repositories.Interface
{
    public interface IRepository<T> where T : IAggregateRoot
    {
    }
}

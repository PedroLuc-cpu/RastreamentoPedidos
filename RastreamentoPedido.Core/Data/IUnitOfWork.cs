namespace RastreamentoPedido.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}

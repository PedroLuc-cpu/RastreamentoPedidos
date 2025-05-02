namespace RastreamentoPedidos.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}

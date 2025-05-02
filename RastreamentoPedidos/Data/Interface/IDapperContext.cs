using System.Data;

namespace RastreamentoPedidos.Data.Interface
{
    public interface IDapperContext
    {
        IDbConnection ConnectionCreate();
        IDbConnection ConnectionCreate(string ConnectionStringName);

    }
}
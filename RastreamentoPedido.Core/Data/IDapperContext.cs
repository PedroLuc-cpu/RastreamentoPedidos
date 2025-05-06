using System.Data;

namespace RastreamentoPedido.Core.Data
{
    public interface IDapperContext
    {
        IDbConnection ConnectionCreate();
        IDbConnection ConnectionCreate(string ConnectionStringName);

    }
}
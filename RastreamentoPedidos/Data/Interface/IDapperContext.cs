using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace RastreamentoPedidos.Data.Interface
{
    public interface IDapperContext
    {
        IDbConnection ConnectionCreate();
        IDbConnection ConnectionCreate(string ConnectionStringName);

    }
}
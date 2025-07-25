﻿using RastreamentoPedido.Core.Model.Endereco;

namespace RastreamentoPedido.Core.Repositories.Clientes
{
    public interface ICidadeRepository : IRepository<Cidade>
    {
        Task<Cidade> CarregarPorId(long id);
    }
}

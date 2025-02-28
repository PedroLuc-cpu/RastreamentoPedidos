﻿using Microsoft.EntityFrameworkCore;
using RastreamentoPedidos.Data;
using RastreamentoPedidos.Model.Clientes;
using RastreamentoPedidos.Repositories.Interface.ICliente;

namespace RastreamentoPedidos.Repositories.ClienteRepository
{
    public class TelefoneRepository : ITelefoneRepository
    {
        private readonly RastreamentoPedidosContext _context;
        public TelefoneRepository(RastreamentoPedidosContext context)
        {
            _context = context;
        }
        public async Task<IList<Telefone>> CarregarPorIdCliente(int idCliente)
        {
            IList<Telefone> telefones = new List<Telefone>();
            telefones.Clear();
            var registros = await _context.telefones.Where(e => e.idCliente == idCliente).ToListAsync();
            foreach (var item in registros)
            {
                telefones.Add(new Telefone
                {
                    idTelefoneCliente = item.idTelefoneCliente,
                    prefixo = item.prefixo,
                    numero = item.numero,
                    padrao = item.padrao,
                    idCliente = item.idCliente,
                });
            }
            return telefones;
        }
    }
}

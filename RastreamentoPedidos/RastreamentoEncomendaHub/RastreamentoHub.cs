﻿using Microsoft.AspNetCore.SignalR;

namespace RastreamentoPedidos.RastreamentoEncomendaHub
{
    public class RastreamentoHub : Hub
    {
        public async Task AtualizarStatusEntrega(int encomendaId, string status)
        {
            await Clients.All.SendAsync("receberAtualizacao", encomendaId, status);
        }
    }
}

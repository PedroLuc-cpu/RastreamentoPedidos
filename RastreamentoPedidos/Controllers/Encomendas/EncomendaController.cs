using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using RastreamentoPedido.WebApi.Core.Controllers;
using RastreamentoPedidos.RastreamentoEncomendaHub;

namespace RastreamentoPedidos.Controllers
{
    public class EncomendaController : MainController
    {
        private readonly IHubContext<RastreamentoHub> _hubContext;
        public EncomendaController(IHubContext<RastreamentoHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost("AtualizarStatus/{encomendaId}")]
        public async Task<ActionResult> AtualizarStatus(int encomendaId, [FromBody] string status)
        {
            await _hubContext.Clients.All.SendAsync("receberAtualizacao", encomendaId, status);
            return Ok(new { mensagem = "Status atualizado com sucesso!" });
        }
    }
}

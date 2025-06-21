using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using RastreamentoPedido.Core.Repositories.Encomenda;
using RastreamentoPedido.Core.Response;
using RastreamentoPedido.WebApi.Core.Controllers;
using RastreamentoPedidos.API.Hubs;

namespace RastreamentoPedidos.Controllers
{
    [Route("api/encomendas")]
    //[ApiExplorerSettings(GroupName = "cliente-v1")]
    [ApiController]
    public class EncomendaController : MainController
    {
        private readonly IHubContext<RastreamentoHub> _hubContext;
        private readonly IStatusEncomendaRepository _statusEncomendaRepository;
        public EncomendaController(IHubContext<RastreamentoHub> hubContext, IStatusEncomendaRepository statusEncomendaRepository)
        {
            _hubContext = hubContext;
            _statusEncomendaRepository = statusEncomendaRepository;
        }

        [HttpPost("AtualizarStatus/{encomendaId}")]
        public async Task<ActionResult> AtualizarStatus(int encomendaId, [FromBody] string status)
        {
            await _hubContext.Clients.All.SendAsync("receberAtualizacao", encomendaId, status);
            return Ok(new { mensagem = "Status atualizado com sucesso!" });
        }

        [HttpGet("status/{id}")]
        public async Task<ActionResult> ObterStatusPorId(int id)
        {
            var statusEncomenda = await _statusEncomendaRepository.ObterStatusPorId(id);
            if (statusEncomenda.Id == 0)
            {
                return CustomResponse(new { mensagem = "Status não encontrado." });
            }
            return Ok(statusEncomenda);
        }
        [HttpGet("todos-status")]
        public async Task<ActionResult> ObterTodosStatus()
        {
            var statusEncomendas = await _statusEncomendaRepository.ObterTodosStatus();
            IList<StatusEncomendaResponse> statusEncomendaResponse = new List<StatusEncomendaResponse>();

            foreach (var status in statusEncomendas)
            {
               if (statusEncomendas.Count == 0)
                {
                    return CustomResponse(new { mensagem = "Nenhum status encontrado." });
                }
                statusEncomendaResponse.Add(new StatusEncomendaResponse
                {
                    Id = status.Id,
                    Status = status.Status
                });
            }
            return Ok(statusEncomendaResponse);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using RastreamentoPedido.Core.Communication;
using RastreamentoPedido.Core.Model.Clientes;
using RastreamentoPedido.Core.Repositories.Cliente;
using RastreamentoPedido.Core.Repositories.Clientes;
using RastreamentoPedido.Core.Requests.Cliente;
using RastreamentoPedido.WebApi.Core.Controllers;

namespace RastreamentoPedidos.API.Controllers.TelefoneController
{
    [Produces("application/json")]
    [Route("api/telefone")]
    public class TelefoneController : MainController
    {
        private readonly ITelefoneRepository _telefoneRepository;
        private readonly IClienteRepository _clienteRepository;
        public TelefoneController(ITelefoneRepository telefoneRepository, IClienteRepository clienteRepository)
        {
            _telefoneRepository = telefoneRepository;
            _clienteRepository = clienteRepository;
        }
        [HttpGet("id/{idCliente:int}")]
        [ProducesResponseType(typeof(Telefone), 200)]
        [ProducesResponseType(typeof(ResponseResult), 400)]
        public async Task<IActionResult> ObterTelefonesPorIdCliente(int idCliente)
        {
            var telefones = await _telefoneRepository.CarregarPorIdCliente(idCliente);
            if (telefones.Count == 0)
            {
                return CustomResponse(new { mensagem = "Nenhum telefone encontrado para o cliente." });
            }
            return Ok(telefones);
        }

        [HttpGet("idTelefone/{idTelefone:int}")]
        [ProducesResponseType(typeof(Telefone), 200)]
        [ProducesResponseType(typeof(ResponseResult), 400)]
        public async Task<IActionResult> ObterTelefonePorId(int idTelefoneCliente)
        {
            var telefone = await _telefoneRepository.ObterTelefonePorIdTelefone(idTelefoneCliente);
            if (telefone.IdCliente <= 0)
            {
                return CustomResponse(new { mensagem = "Nenhum telefone encontrado" });
            }
            return Ok(telefone);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Telefone), 200)]
        [ProducesResponseType(typeof(ResponseResult), 400)]
        public async Task<IActionResult> AdicionarTelefone([FromBody] TelefoneRequest telefone)
        {
            var cliente = await _clienteRepository.CarregarPorId(telefone.idCliente);
            if (cliente == null)
            {
                return CustomResponse("Cliente não encontrado.");
            }

            if (telefone == null)
            {
                return CustomResponse("Telefone é obrigatório.");
            }

            var telefoneAdicionado = new Telefone
            {
                IdCliente = cliente.IdCliente,
                Numero = telefone.Numero,
                Prefixo = telefone.Prefixo,
                Padrao = telefone.Padrao
            };

            var novoTelefone = await _telefoneRepository.AdicionarTelefone(telefoneAdicionado);
            return Ok(novoTelefone);
        }
        [HttpPut]
        [ProducesResponseType(typeof(Telefone), 200)]
        [ProducesResponseType(typeof(ResponseResult), 400)]
        public async Task<IActionResult> AtualizarTelefone([FromBody] TelefoneRequest telefone)
        {
            var cliente = await _clienteRepository.CarregarPorId(telefone.idCliente);
            var telefoneAtualizar = new Telefone
            {
                IdCliente = cliente.IdCliente,
                Numero = telefone.Numero,
                Prefixo = telefone.Prefixo,
                Padrao = telefone.Padrao
            };
            if (cliente == null)
            {
                return CustomResponse("Cliente não encontrado.");
            }

            if (telefoneAtualizar == null)
            {
                return CustomResponse("Telefone é obrigatório.");
            }
            var telefoneAtualizado = await _telefoneRepository.AtualizarTelefone(telefoneAtualizar);
            return Ok(telefoneAtualizado);
        }
    }
}

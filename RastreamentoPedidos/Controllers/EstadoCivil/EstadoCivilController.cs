using Microsoft.AspNetCore.Mvc;
using RastreamentoPedido.Core.Communication;
using RastreamentoPedido.Core.Model.Clientes;
using RastreamentoPedido.Core.Repositories.IEstadoCivilRepository;
using RastreamentoPedido.Core.Requests.Cliente;
using RastreamentoPedido.WebApi.Core.Controllers;

namespace RastreamentoPedidos.API.Controllers.EstadoCivilController
{
    [Produces("application/json")]
    [Route("api/estado-civil")]
    public class EstadoCivilController : MainController
    {
        private readonly IEstadoCivilRepository _estadoCivilRepository;

        public EstadoCivilController(IEstadoCivilRepository estadoCivilRepository)
        {
            _estadoCivilRepository = estadoCivilRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodosEstadosCivis()
        {
            var estadosCivis = await _estadoCivilRepository.CarregarTodosEstadosCivis();
            return estadosCivis.Any() ? Ok(estadosCivis) : CustomResponse("Nenhum estado civil encontrado.");
        }
        [HttpPost]
        [ProducesResponseType(typeof(EstadoCivil), 200)]
        [ProducesResponseType(typeof(EstadoCivilRequest), 400)]
        public async Task<IActionResult> AdcionarEstadoCivil([FromBody] EstadoCivilRequest estadoCivilRequest)
        {
            EstadoCivil addCliente = new EstadoCivil();

            var estadoCivilExistente = await _estadoCivilRepository.CarregarEstadoCivilPorDescricao(estadoCivilRequest.EstadoCivil);

            if (estadoCivilExistente != null)
            {
                return CustomResponse("Estado civil já cadastrado.");
            }

            if (addCliente == null)
            {
                return CustomResponse("o estado civil é obrigatório");
            }
            if (estadoCivilRequest != null)
            {
                addCliente.EstadoCivilDescricao = estadoCivilRequest.EstadoCivil;
            }
            var estadoCivilAdicionado = await _estadoCivilRepository.AdicionarEstadoCivil(addCliente);

            return Ok(estadoCivilAdicionado);

        }
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(EstadoCivil), 200)]
        [ProducesResponseType(typeof(ResponseResult), 400)]
        public async Task<IActionResult> CarregarEstadoCivilPorId(int id)
        {
            var estadoCivil = await _estadoCivilRepository.CarregarEstadoCivilPorId(id);
            if (estadoCivil == null)
            {
                return CustomResponse("Estado civil não encontrado.");
            }
            return Ok(estadoCivil);
        }
        [HttpGet("{descricao}")]
        [ProducesResponseType(typeof(EstadoCivil), 200)]
        [ProducesResponseType(typeof(ResponseResult), 400)]
        public async Task<IActionResult> CarregarEstadoCivilPorDescricao(string descricao)
        {
            var estadoCivil = await _estadoCivilRepository.CarregarEstadoCivilPorDescricao(descricao);
            if (estadoCivil == null)
            {
                return CustomResponse("Estado civil não encontrado.");
            }
            return Ok(estadoCivil);
        }
        [HttpPut]
        [ProducesResponseType(typeof(EstadoCivil), 200)]
        [ProducesResponseType(typeof(ResponseResult), 400)]
        public async Task<IActionResult> AtualizarEstadoCivil([FromBody] EstadoCivil estadoCivilRequest)
        {
            var estadoCivilExistente = await _estadoCivilRepository.CarregarEstadoCivilPorId(estadoCivilRequest.Id);
            if (estadoCivilExistente == null)
            {
                return CustomResponse("Estado civil não encontrado.");
            }
            if (string.IsNullOrEmpty(estadoCivilRequest.EstadoCivilDescricao))
            {
                return CustomResponse("O estado civil é obrigatório.");
            }
            estadoCivilExistente.EstadoCivilDescricao = estadoCivilRequest.EstadoCivilDescricao;
            var estadoCivilAtualizado = await _estadoCivilRepository.AtualizarEstadoCivil(estadoCivilExistente);
            return Ok(estadoCivilAtualizado);
        }
        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(EstadoCivil), 200)]
        [ProducesResponseType(typeof(ResponseResult), 400)]
        public async Task<IActionResult> DeletarEstadoCivil(int id)
        {
            var estadoCivilExistente = await _estadoCivilRepository.CarregarEstadoCivilPorId(id);
            if (estadoCivilExistente == null)
            {
                return CustomResponse("Estado civil não encontrado.");
            }
            var resultado = await _estadoCivilRepository.DeletarEstadoCivil(id);
            if (!resultado)
            {
                return CustomResponse("Erro ao deletar estado civil.");
            }
            return Ok("Estado civil deletado com sucesso.");
        }
    }
}

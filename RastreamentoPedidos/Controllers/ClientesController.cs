using Microsoft.AspNetCore.Mvc;
using RastreamentoPedidos.Model;
using RastreamentoPedidos.Model.DTO;
using RastreamentoPedidos.Repositories.Interface;

namespace RastreamentoPedidos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;
        public ClientesController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ObterClientes()
        {
            var clientes = await _clienteRepository.CarregarTodos();
            return clientes.Any()
                ? Ok(clientes) : NotFound("Clientes não encontrado");
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarCliente(ClienteDto clienteDto)
        {
            if (clienteDto == null)
            {
                return BadRequest("O objeto clienteDTO é obrigatório.");
            }

            var cliente = new ClienteDto
            {
                nome = clienteDto.nome,
                email = clienteDto.email,
                telefone = clienteDto.telefone
            };

            var clienteAdicionado = await _clienteRepository.AdicionarClientes(cliente);

            if (clienteAdicionado == null)
            {
                return BadRequest("Erro ao cadastrar o cliente.");
            }

            return Ok(clienteAdicionado);
        }

    }
}

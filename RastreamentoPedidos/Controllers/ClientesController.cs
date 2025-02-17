using Microsoft.AspNetCore.Mvc;
using RastreamentoPedidos.Model.DTO;
using RastreamentoPedidos.Repositories.Interface;

namespace RastreamentoPedidos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : MainController
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
                ? Ok(clientes) : CustomResponse("Clientes não encontrado");
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarCliente(ClienteDto clienteDto)
        {
            var clientePorMail = await _clienteRepository.CarregarPorEmail(clienteDto.email);
            
            if (clienteDto == null)
            {
                return CustomResponse("o cliente é obrigatório.");
            }
            if (clienteDto.email ==clientePorMail.email)
            {
                return CustomResponse("esse email já se encontra cadastrado em outro cliente");
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
                return CustomResponse("Erro ao cadastrar o cliente.");
            }

            return Ok(clienteAdicionado);
        }

    }
}

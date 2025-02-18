using Microsoft.AspNetCore.Mvc;
using RastreamentoPedidos.Model.Clientes;
using RastreamentoPedidos.Model.DTO.ClienteDTO;
using RastreamentoPedidos.Repositories.Interface.ICliente;

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
        public async Task<IActionResult> AdicionarCliente(Cliente cliente)
        {
            Cliente addCliente = new Cliente();
            
            if (addCliente == null)
            {
                return CustomResponse("o cliente é obrigatório.");
            }
            if (cliente != null)
            {
                addCliente.idCliente = cliente.idCliente;
                addCliente.nome = cliente.nome;
                addCliente.email = cliente.email;
                addCliente.documento = cliente.documento;
                addCliente.encomendas = cliente.encomendas;
                addCliente.telefones = cliente.telefones;
                addCliente.enderecos = cliente.enderecos;
            }
            var clienteAdicionado = await _clienteRepository.Adicionar(addCliente);           

            return Ok(clienteAdicionado);
        }

    }
}

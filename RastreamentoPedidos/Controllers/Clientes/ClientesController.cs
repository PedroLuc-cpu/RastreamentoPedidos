using Microsoft.AspNetCore.Mvc;
using RastreamentoPedido.Core.Model.Clientes;
using RastreamentoPedido.Core.Repositories.Clientes;

namespace RastreamentoPedidos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : MainController
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly ICidadeRepository _cidadeRepository;
        public ClientesController(IClienteRepository clienteRepository, ICidadeRepository cidadeRepository)
        {
            _clienteRepository = clienteRepository;
            _cidadeRepository = cidadeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ObterClientes()
        {
            var clientes = await _clienteRepository.CarregarTodos();
            return clientes.Any()
                ? Ok(clientes) : CustomResponse("Clientes não encontrado");
        }

        [HttpGet("cidade/id/{id:int}")]
        public async Task<IActionResult> carregarCidadaPorId(int id)
        {
            var cidades = await _cidadeRepository.CarregarPorId(id);
            if (cidades.idCidade == 0)
            {
                return CustomResponse("id da cidade deve ser obrigatório para realizar a buscar");
            }
            return Ok(cidades);
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

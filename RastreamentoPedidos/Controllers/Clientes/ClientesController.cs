using Microsoft.AspNetCore.Mvc;
using RastreamentoPedido.Core.Communication;
using RastreamentoPedido.Core.Model.Clientes;
using RastreamentoPedido.Core.Repositories.Clientes;
using RastreamentoPedido.Core.Requests.Cliente;
using RastreamentoPedido.Core.Service;
using RastreamentoPedido.Core.ViewModels.Cidade;
using RastreamentoPedido.WebApi.Core.Controllers;

namespace RastreamentoPedidos.Controllers
{
    [Route("api/v1/cliente")]
    //[ApiExplorerSettings(GroupName = "cliente-v1")]
    [ApiController]
    public class ClientesController : MainController
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly ICidadeRepository _cidadeRepository;
        private readonly ICidadeService _cidadeService;
        public ClientesController(IClienteRepository clienteRepository, ICidadeRepository cidadeRepository, ICidadeService cidadeService)
        {
            _clienteRepository = clienteRepository;
            _cidadeRepository = cidadeRepository;
            _cidadeService = cidadeService;
        }

        [HttpGet]
        public async Task<IActionResult> ObterClientes()
        {
            var clientes = await _clienteRepository.CarregarTodos();
            return clientes.Any()
                ? Ok(clientes) : CustomResponse("Clientes não encontrado");
        }

        [HttpGet("cidade/id/{id:int}")]
        [ProducesResponseType(typeof(Cidade), 200)]
        [ProducesResponseType(typeof(ResponseResult), 400)]
        public async Task<IActionResult> carregarCidadaPorId(int id)
        {
            var cidades = await _cidadeRepository.CarregarPorId(id);
            if (cidades.IdCidade == 0)
            {
                return CustomResponse("id da cidade deve ser obrigatório para realizar a buscar");
            }
            return Ok(cidades);
        }

        [HttpGet("cidade/{sigla}")]
        [ProducesResponseType(typeof(CidadeViewModel), 200)]
        [ProducesResponseType(typeof(ResponseResult), 400)]
        public async Task<IActionResult> carregarCidadePorUf(string sigla)
        {
            if (sigla  == null)
            {
                return CustomResponse("A sigla da cidade é obrigatória");
            }
            var cidades = await _cidadeService.BuscarCidadePorEstado(sigla.ToUpper());
            return Ok(cidades);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Cliente), 200)]
        [ProducesResponseType(typeof(ResponseResult), 400)]
        public async Task<IActionResult> AdicionarCliente([FromBody]ClienteRequest clienteRequest)
        {
            Cliente addCliente = new Cliente();

            if (addCliente == null)
            {
                return CustomResponse("o cliente é obrigatório.");
            }
            if (clienteRequest != null)
            {   
                addCliente.Nome = clienteRequest.Nome;
                addCliente.Email = clienteRequest.Email;
                addCliente.Documento = clienteRequest.Documento;
                addCliente.Ativo = clienteRequest.Ativo;
                addCliente.Sexo = clienteRequest.Sexo;
                addCliente.DataNascimento = clienteRequest.DataNascimento;
                if (clienteRequest.EstadoCivil != null)
                {
                    addCliente.EstadoCivil = new EstadoCivil
                    {
                        EstadoCivilDescricao = clienteRequest.EstadoCivil.EstadoCivil
                    };
                }

            }
            var clienteAdicionado = await _clienteRepository.Adicionar(addCliente);

            return Ok(clienteAdicionado);
        }

    }
}

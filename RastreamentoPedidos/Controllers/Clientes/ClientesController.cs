using Microsoft.AspNetCore.Mvc;
using RastreamentoPedido.Core.Model.Clientes;
using RastreamentoPedido.Core.Repositories.Clientes;
using RastreamentoPedido.Core.Requests;
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
        public async Task<IActionResult> AdicionarCliente(ClienteRequest clienteRequest)
        {
            Cliente addCliente = new Cliente();

            if (addCliente == null)
            {
                return CustomResponse("o cliente é obrigatório.");
            }
            if (clienteRequest != null)
            {
                addCliente.nome = clienteRequest.Nome;
                addCliente.email = clienteRequest.Email;
                if (addCliente.telefones != null)
                {
                    addCliente.telefones = new List<Telefone>
                    {
                        new Telefone
                        {
                            numero = clienteRequest.Telefone[0].numero,
                            padrao = clienteRequest.Telefone[0].padrao,
                            prefixo = clienteRequest.Telefone[0].prefixo

                        }
                    };
                }
                if (addCliente.enderecos != null)
                {
                    addCliente.enderecos = new List<Endereco>
                    {
                        new Endereco
                        {
                            Bairro = clienteRequest.Endereco[0].Bairro,
                            CEP = clienteRequest.Endereco[0].CEP,
                            Cidade = clienteRequest.Endereco[0].Cidade,
                            Numero = clienteRequest.Endereco[0].Numero,
                            Complemento = clienteRequest.Endereco[0].Complemento
                          
                        }
                    };
                }
            }
            var clienteAdicionado = await _clienteRepository.Adicionar(addCliente);

            return Ok(clienteAdicionado);
        }

    }
}

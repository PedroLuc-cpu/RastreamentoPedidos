using Microsoft.AspNetCore.Mvc;
using RastreamentoPedido.Core.Communication;
using RastreamentoPedido.Core.Model.Clientes;
using RastreamentoPedido.Core.Repositories.Clientes;
using RastreamentoPedido.Core.Repositories.IEstadoCivilRepository;
using RastreamentoPedido.Core.Requests.Cliente;
using RastreamentoPedido.WebApi.Core.Controllers;

namespace RastreamentoPedidos.Controllers
{
    [Route("api/cliente")]
    //[ApiExplorerSettings(GroupName = "cliente-v1")]
    [ApiController]
    public class ClientesController : MainController
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IEstadoCivilRepository _estadoCivilRepository;
        public ClientesController(IClienteRepository clienteRepository, IEstadoCivilRepository estadoCivilRepository)
        {
            _clienteRepository = clienteRepository;
            _estadoCivilRepository = estadoCivilRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ObterClientes()
        {
            var clientes = await _clienteRepository.CarregarTodos();
            if (clientes.Count == 0)
            {
                return CustomResponse(new { mensagem = "Nenhum cliente encontrado." });
            }
            return Ok(clientes);

        }
        [HttpPost]
        [ProducesResponseType(typeof(Cliente), 200)]
        [ProducesResponseType(typeof(ResponseResult), 400)]
        public async Task<IActionResult> AdicionarCliente([FromBody]ClienteRequest clienteRequest)
        {
            Cliente addCliente = new Cliente();

            var clienteExistente = await _clienteRepository.CarregarPorEmail(clienteRequest.Email);
            var clienteExistenteDocumento = await _clienteRepository.CarregarPorDocumento(clienteRequest.Documento);
            var carregarEstadoCivil = await _estadoCivilRepository.CarregarEstadoCivilPorDescricao(clienteRequest.EstadoCivil.EstadoCivil);

            if (carregarEstadoCivil == null)
            {
                return CustomResponse("O estado civil informado não existe.");
            }

            if (clienteRequest.Email == clienteExistente.Email)
            {
                return CustomResponse("Já existe um cliente cadastrado com este e-mail.");
            }
            
            if (clienteRequest.Documento == clienteExistenteDocumento.Documento)
            {
                return CustomResponse("Já existe um cliente cadastrado com este documento.");
            }


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
                        EstadoCivilDescricao = carregarEstadoCivil.EstadoCivilDescricao
                    };
                }

            }
            var clienteAdicionado = await _clienteRepository.Adicionar(addCliente);

            return Ok(clienteAdicionado);
        }

    }
}

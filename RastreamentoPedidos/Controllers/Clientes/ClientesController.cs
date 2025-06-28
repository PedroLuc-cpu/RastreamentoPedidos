using Microsoft.AspNetCore.Mvc;
using RastreamentoPedido.Core.Communication;
using RastreamentoPedido.Core.Model.Clientes;
using RastreamentoPedido.Core.Repositories.Clientes;
using RastreamentoPedido.Core.Repositories.IEstadoCivilRepository;
using RastreamentoPedido.Core.Requests.Cliente;
using RastreamentoPedido.Core.Utils;
using RastreamentoPedido.Core.Utils.ValidacaoStrings;
using RastreamentoPedido.WebApi.Core.Controllers;

namespace RastreamentoPedidos.Controllers
{
    [Produces("application/json")]
    [Route("api/cliente")]
    //[ApiExplorerSettings(GroupName = "cliente-v1")]
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
        [ProducesResponseType(typeof(Cliente), 200)]
        [ProducesResponseType(typeof(ResponseResult), 400)]
        public async Task<IActionResult> ObterClientes()
        {
            var clientes = await _clienteRepository.CarregarTodos();
            if (clientes.Count == 0)
            {
                return CustomResponse("Nenhum cliente encontrado.");
            }
            return Ok(clientes);

        }

        [HttpGet("id/{idCliente:int}")]
        [ProducesResponseType(typeof(Cliente), 200)]
        [ProducesResponseType(typeof(ResponseResult), 400)]
        public async Task<IActionResult> ObterClientePorId(int idCliente)
        {
            if (idCliente <= 0)
            {
                return CustomResponse("O ID do cliente deve ser deve ser informado");
            }
            var cliente = await _clienteRepository.CarregarPorId(idCliente);
            if (cliente.IdCliente < 0)
            {
                return CustomResponse("Nenhum cliente foi encontrado");
            }
            return Ok(cliente);
        }

        [HttpGet("email/{email}")]
        [ProducesResponseType(typeof(Cliente), 200)]
        [ProducesResponseType(typeof(ResponseResult), 400)]
        public async Task<IActionResult> ObterClientePorEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return CustomResponse("O e-mail não foi informado.");
            }

            if (!ValidaEmail.isEmail(email))
            {
                return CustomResponse("O e-mail informado é inválido.");
            }
            
            var cliente = await _clienteRepository.CarregarPorEmail(email);

            if (cliente.IdCliente <= 0)
            {
                return CustomResponse("Nenhum cliente encontrado com o e-mail informado.");
            }
            return Ok(cliente);
        }

        [HttpGet("doc/{doc}")]
        [ProducesResponseType(typeof(Cliente), 200)]
        [ProducesResponseType(typeof(ResponseResult), 400)]
        public async Task<IActionResult> CarregarPorDocValido(string doc)
        {
            if (doc == null)
            {
                return CustomResponse("O documento não informado");
            }

            var apenasNumeros = doc.Replace("%2F", "").ApenasNumeros();

            if (apenasNumeros.Length == 11)
            {
                if (!ValidaCPF.IsCpf(apenasNumeros))
                {
                    return CustomResponse("O CPF informado é inválido.");
                }
            }
            else if (apenasNumeros.Length == 14)
            {
                if (!ValidaCNPJ.IsCnpj(apenasNumeros))
                {
                    return CustomResponse("O CNPJ informado é inválido.");
                }
            }
            else
            {
                return CustomResponse("O documento informado não é válido.");
            }

            var cliente = await _clienteRepository.CarregarPorDocumento(apenasNumeros);
            if (cliente.IdCliente < 0)
            {
                return CustomResponse("Nenhum cliente encontrado com o documento informado.");
            }
            return Ok(cliente);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Cliente), 200)]
        [ProducesResponseType(typeof(ResponseResult), 400)]
        public async Task<IActionResult> AdicionarCliente([FromBody]ClienteRequest clienteRequest)
        {
            Cliente addCliente = new Cliente();

            if (!clienteRequest.ValidationResult.IsValid)
            {
                return CustomResponse(clienteRequest.ValidationResult);
            }

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
                        Id = carregarEstadoCivil.Id,
                        EstadoCivilDescricao = carregarEstadoCivil.EstadoCivilDescricao
                    };
                }

            }
            var clienteAdicionado = await _clienteRepository.Adicionar(addCliente);

            return Ok(clienteAdicionado);
        }

    }
}

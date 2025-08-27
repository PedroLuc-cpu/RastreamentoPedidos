using Microsoft.AspNetCore.Mvc;
using RastreamentoPedido.Core.Communication;
using RastreamentoPedido.Core.Model.Clientes;
using RastreamentoPedido.Core.Repositories.Cliente;
using RastreamentoPedido.Core.Repositories.IEstadoCivilRepository;
using RastreamentoPedido.Core.Requests.Cliente;
using RastreamentoPedido.Core.Utils;
using RastreamentoPedido.Core.Utils.ValidacaoStrings;
using RastreamentoPedido.WebApi.Core.Controllers;

namespace RastreamentoPedidos.API.Controllers.Clientes
{
    [Produces("application/json")]
    [Route("api/cliente")]
    //[ApiExplorerSettings(GroupName = "cliente-v1")]
    public class ClientesController(IClienteRepository clienteRepository, IEstadoCivilRepository estadoCivilRepository) : MainController
    {
        private readonly IClienteRepository _clienteRepository = clienteRepository;
        private readonly IEstadoCivilRepository _estadoCivilRepository = estadoCivilRepository;

        [HttpGet]
        [ProducesResponseType(typeof(ClienteModel), 200)]
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
        [ProducesResponseType(typeof(ClienteModel), 200)]
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
        [ProducesResponseType(typeof(ClienteModel), 200)]
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
        [ProducesResponseType(typeof(ClienteModel), 200)]
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

        [HttpPost("adicionar")]
        [ProducesResponseType(typeof(ClienteModel), 200)]
        [ProducesResponseType(typeof(ResponseResult), 400)]
        public async Task<IActionResult> AdicionarCliente([FromBody]ClienteInserirRequest clienteRequest)
        {
            

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

            ClienteModel cliente = await ClienteRequestToClienteInserir(clienteRequest);

            var clienteAdicionado = await _clienteRepository.Inserir(cliente);

            return Ok(clienteAdicionado);
        }
        [HttpPut("alterar")]
        [ProducesResponseType(typeof(ClienteModel), 200)]
        [ProducesResponseType(typeof(ResponseResult), 400)]
        public async Task<IActionResult> AlterarCliente([FromBody] ClienteAlterarResquest clienteRequest)
        {
            if (!clienteRequest.ValidationResult.IsValid)
            {
                return CustomResponse(clienteRequest.ValidationResult);
            }
            var clienteExistente = await _clienteRepository.CarregarPorId(clienteRequest.IdCliente);
            var clienteExistenteEmail = await _clienteRepository.CarregarPorEmail(clienteRequest.Email);
            var clienteExistenteDocumento = await _clienteRepository.CarregarPorDocumento(clienteRequest.Documento);
            var carregarEstadoCivil = await _estadoCivilRepository.CarregarEstadoCivilPorDescricao(clienteRequest.EstadoCivil.EstadoCivil);
            if (clienteExistente.IdCliente <= 0)
            {
                return CustomResponse("Nenhum cliente foi encontrado para o ID informado.");
            }
            if (carregarEstadoCivil == null)
            {
                return CustomResponse("O estado civil informado não existe.");
            }
            if (clienteExistenteEmail.Email == clienteRequest.Email && clienteExistenteEmail.IdCliente != clienteRequest.IdCliente)
            {
                return CustomResponse("Já existe um cliente cadastrado com este e-mail.");
            }
            if (clienteExistenteDocumento.Documento == clienteRequest.Documento && clienteExistenteDocumento.IdCliente != clienteRequest.IdCliente)
            {
                return CustomResponse("Já existe um cliente cadastrado com este documento.");
            }
            ClienteModel cliente = await ClienteRequestToClienteAlterar(clienteRequest);
            cliente.IdCliente = clienteRequest.IdCliente;
            var clienteAlterado = await _clienteRepository.Alterar(cliente);
            return Ok(clienteAlterado);
        }

        private async Task<ClienteModel> ClienteRequestToClienteAlterar(ClienteAlterarResquest request)
        {
            return new ClienteModel
            {
                IdCliente = request.IdCliente,
                Nome = request.Nome,
                Email = request.Email,
                Documento = request.Documento,
                Ativo = request.Ativo,
                Sexo = request.Sexo,
                DataNascimento = request.DataNascimento,
                EstadoCivil = await _estadoCivilRepository.CarregarEstadoCivilPorDescricao(request.EstadoCivil.EstadoCivil),
            };
        }

        private async Task<ClienteModel> ClienteRequestToClienteInserir(ClienteInserirRequest request)
        {
            return new ClienteModel
            {
                Nome = request.Nome,
                Email = request.Email,
                Documento = request.Documento,
                Ativo = request.Ativo,
                Sexo = request.Sexo,
                DataNascimento = request.DataNascimento,
                EstadoCivil = await _estadoCivilRepository.CarregarEstadoCivilPorDescricao(request.EstadoCivil.EstadoCivil),
            };
        }

    }
}

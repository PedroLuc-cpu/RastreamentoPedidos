using Microsoft.AspNetCore.Mvc;
using RastreamentoPedido.Core.Communication;
using RastreamentoPedido.Core.Model.Endereco;
using RastreamentoPedido.Core.Repositories.Cliente;
using RastreamentoPedido.Core.Repositories.Clientes;
using RastreamentoPedido.Core.Requests.Cliente;
using RastreamentoPedido.WebApi.Core.Controllers;

namespace RastreamentoPedidos.API.Controllers.Clientes
{
    [Route("api/endereco")]
    [Produces("application/json")]
    public class EnderecoController(IEnderecoRepository enderecoRepository, IPaisRepository paisRepository, ICidadeRepository cidadeRepository, ITpLogradouroRepository tpLogradouroRepository) : MainController
    {
        private readonly IEnderecoRepository _enderecoRepository = enderecoRepository;
        private readonly IPaisRepository _paisRepository = paisRepository;
        private readonly ICidadeRepository _cidadeRepository = cidadeRepository;
        private readonly ITpLogradouroRepository _tpLogradouroRepository = tpLogradouroRepository;

        [HttpPost("inserir")]
        [ProducesResponseType(typeof(Enderecos), 201)]
        [ProducesResponseType(typeof(ResponseResult), 400)]
        public async Task<IActionResult> InserirEndereco([FromBody] EnderecoRequest endereco)
        {   
            LimparErrosProcessamento();
            try
            {
                if (!endereco.ValidationResult.IsValid)
                {
                    return CustomResponse(endereco.ValidationResult);
                }
                var tpLougradouro = await _tpLogradouroRepository.CarregarPorId(endereco.IdTpLogradouro);
                if (tpLougradouro.IdTpLogradouro <= 0)
                {
                    return CustomResponse("Tipo de Lougradouro não foi encontrado");
                }

                var cidade = await _cidadeRepository.CarregarPorId(endereco.IdCidade);
                if (cidade.IdCidade <= 0)
                {
                    return CustomResponse("Cidade não foi encontrado");
                }
                Enderecos enderecos = await EnderecoRequestToEndereco(endereco);
                var enderecoInserido = await _enderecoRepository.Inserir(enderecos);
                return CustomResponse(enderecoInserido);
            }
            catch (Exception ex)
            {
                return CustomResponse($"Ocorreu um erro ao inserir o endereço: {ex.Message}");
            }
        }

        [HttpGet("cliente/{idCliente:int}")]
        [ProducesResponseType(typeof(IList<Enderecos>), 200)]
        [ProducesResponseType(typeof(ResponseResult), 400)]
        public async Task<IActionResult> CarregarEnderecoPorIdCliente(int id)
        {
           LimparErrosProcessamento();
            try
            {
                if (id <= 0)
                {
                    return CustomResponse("O ID do cliente deve ser informado");
                }
                var enderecos = await _enderecoRepository.CarregarPorIdCliente(id);
                if (enderecos.Count == 0)
                {
                    return CustomResponse("Nenhum endereço foi encontrado para esse cliente");
                }
                return Ok(enderecos);
            }
            catch (Exception ex)
            {
                return CustomResponse($"Ocorreu um erro ao carregar os endereços: {ex.Message}");
            }
        }

        [HttpPut("alterar")]
        [ProducesResponseType(typeof(Enderecos), 200)]
        [ProducesResponseType(typeof(ResponseResult), 400)]
        public async Task<IActionResult> AlterarEndereco([FromBody] EnderecoRequest endereco)
        {
            LimparErrosProcessamento();
            try
            {
                if (!endereco.ValidationResult.IsValid)
                {
                    return CustomResponse(endereco.ValidationResult);
                }
                var tpLougradouro = await _tpLogradouroRepository.CarregarPorId(endereco.IdTpLogradouro);
                if (tpLougradouro.IdTpLogradouro <= 0)
                {
                    return CustomResponse("Tipo de Lougradouro não foi encontrado");
                }
                var cidade = await _cidadeRepository.CarregarPorId(endereco.IdCidade);
                if (cidade.IdCidade <= 0)
                {
                    return CustomResponse("Cidade não foi encontrado");
                }
                Enderecos enderecos = await EnderecoRequestToEndereco(endereco);
                var enderecoAlterado = await _enderecoRepository.Alterar(enderecos);
                return CustomResponse(enderecoAlterado);
            }
            catch (Exception ex)
            {
                return CustomResponse($"Ocorreu um erro ao alterar o endereço: {ex.Message}");
            }
        }


        private async Task<Enderecos> EnderecoRequestToEndereco(EnderecoRequest enderecoRequest)
        {
            return new Enderecos
            {
                IdPessoa = enderecoRequest.IdCliente,
                TpLogradouro = await _tpLogradouroRepository.CarregarPorId(enderecoRequest.IdTpLogradouro),
                Cidade = await _cidadeRepository.CarregarPorId(enderecoRequest.IdCidade),
                Complemento = enderecoRequest.Complemento,
                Bairro = enderecoRequest.Bairro,
                Numero = enderecoRequest.Numero,
                CEP = enderecoRequest.Cep
            };
        }    
    }
}

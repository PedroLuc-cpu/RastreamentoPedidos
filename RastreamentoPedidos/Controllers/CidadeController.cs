using Microsoft.AspNetCore.Mvc;
using RastreamentoPedido.Core.Communication;
using RastreamentoPedido.Core.Model.Endereco;
using RastreamentoPedido.Core.Repositories.Clientes;
using RastreamentoPedido.Core.Service;
using RastreamentoPedido.Core.ViewModels.Cidade;
using RastreamentoPedido.Core.ViewModels.Cidade.ViaCep;
using RastreamentoPedido.WebApi.Core.Controllers;
using System.Text.RegularExpressions;

namespace RastreamentoPedidos.API.Controllers
{
    [Route("api/cidade")]
    [Produces("application/json")]
    public class CidadeController : MainController
    {
        private readonly ICidadeRepository _cidadeRepository;
        private readonly ICidadeService _cidadeService;

        public CidadeController(ICidadeRepository cidadeRepository, ICidadeService cidadeService)
        {
            _cidadeRepository = cidadeRepository;
            _cidadeService = cidadeService;
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
            if (sigla == null)
            {
                return CustomResponse("A sigla da cidade é obrigatória");
            }
            var cidades = await _cidadeService.BuscarCidadePorEstado(sigla.ToUpper());
            return Ok(cidades);
        }

        [HttpGet("estados")]
        [ProducesResponseType(typeof(UFViewModel), 200)]
        [ProducesResponseType(typeof(ResponseResult), 400)]
        public async Task<IActionResult> carregarTodosEstados()
        { 
            var estados = await _cidadeService.BuscarTodosEstados();
            if (estados.Count <= 0)
            {
                return CustomResponse("Nenhum estado encontrado");
            }
            return Ok(estados);

        }

        [HttpGet("cep/{cep}")]
        [ProducesResponseType(typeof(ViaCepViewModel), 200)]
        [ProducesResponseType(typeof(ResponseResult), 400)]
        public async Task<IActionResult> BuscarEnderecoViaCep(string cep)
        {
            if (cep == null)
            {
                return CustomResponse("O CEP é obrigatório.");
            }
            if (cep.Length < 7 && Regex.IsMatch(cep, @"^\d+$") && cep.Length >= 9)
            {
                return CustomResponse("O CEP é deve ter 8 numero ");
            }
            if (!Regex.IsMatch(cep, @"^\d+$"))
            {
                return CustomResponse("O CEP deve conter apenas números.");
            }

            var endereco = await _cidadeService.BuscarEnderecoViaCep(cep);

            if (string.IsNullOrEmpty(endereco.Cep))
            {
                return CustomResponse("Não existe nenhum endereço com esse CEP");
            }

            return Ok(endereco);
        }
    }
}

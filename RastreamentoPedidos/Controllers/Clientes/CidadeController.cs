using Microsoft.AspNetCore.Mvc;
using RastreamentoPedido.Core.Communication;
using RastreamentoPedido.Core.Model.Endereco;
using RastreamentoPedido.Core.Repositories.Cliente;
using RastreamentoPedido.Core.Repositories.Clientes;
using RastreamentoPedido.WebApi.Core.Controllers;

namespace RastreamentoPedidos.API.Controllers.Clientes
{
    [Route("api/cidade")]
    [Produces("application/json")]
    public class CidadeController(ICidadeRepository cidadeRepository, IPaisRepository paisRepository) : MainController
    {
        private readonly ICidadeRepository _cidadeRepository = cidadeRepository;
        private readonly IPaisRepository _paisRepository = paisRepository;

        [HttpGet("cidade/id/{id:int}")]
        [ProducesResponseType(typeof(Cidade), 200)]
        [ProducesResponseType(typeof(ResponseResult), 400)]
        public async Task<IActionResult> CarregarCidadaPorId(int id)
        {
            try
            {
                var cidades = await _cidadeRepository.CarregarPorId(id);
                if (cidades.IdCidade == 0)
                {
                    return CustomResponse("id da cidade deve ser obrigatório para realizar a buscar");
                }
                return Ok(cidades);
            }
            catch (Exception ex)
            {
                return CustomResponse($"Ocorreu um erro ao carregar a cidade: {ex.Message}");
            }
        }
        [HttpGet("cidade/todos")]
        [ProducesResponseType(typeof(IList<Cidade>), 200)]
        [ProducesResponseType(typeof(ResponseResult), 400)]
        public async Task<IActionResult> CarregarTodasCidades()
        {
            try
            {
                var cidades = await _cidadeRepository.CarregarTodos();
                return Ok(cidades);
            }
            catch (Exception ex)
            {
                return CustomResponse($"Ocorreu um erro ao carregar as cidades: {ex.Message}");
            }
        }

        [HttpGet("cidade/estado/{idEstado:int}")]
        [ProducesResponseType(typeof(IList<Cidade>), 200)]
        [ProducesResponseType(typeof(ResponseResult), 400)]
        public async Task<IActionResult> CarregarCidadesPorIdEstado(int idEstado)
        {
            try
            {
                if (idEstado <= 0)
                {
                    return CustomResponse("id do estado deve ser obrigatório para realizar a buscar");
                }
                var cidades = await _cidadeRepository.CarregarPorIdUf(idEstado);
                if (cidades.Count > 0)
                {
                    return Ok(cidades);
                }
                return CustomResponse("Não foi encontrado nenhuma cidade para o estado informado");
            }
            catch (Exception ex)
            {
                return CustomResponse($"Ocorreu um erro ao carregar as cidades: {ex.Message}");
            }
        }
        [HttpGet("cidade/estado/sigla/{siglaEstado}")]
        [ProducesResponseType(typeof(IList<Cidade>), 200)]
        [ProducesResponseType(typeof(ResponseResult), 400)]
        public async Task<IActionResult> CarregarCidadesPorSiglaEstado(string siglaEstado)
        {
            try
            {
                if (string.IsNullOrEmpty(siglaEstado))
                {
                    return CustomResponse("Sigla do estado deve ser obrigatório para realizar a buscar");
                }
                var cidades = await _cidadeRepository.CarregarPorUF(siglaEstado);
                if (cidades.Count > 0)
                {
                    return Ok(cidades);
                }
                return CustomResponse("Não foi encontrado nenhuma cidade para o estado informado");
            }
            catch (Exception ex)
            {
                return CustomResponse($"Ocorreu um erro ao carregar as cidades: {ex.Message}");
            }
        }

        [HttpGet("pais/id/{id:int}")]
        [ProducesResponseType(typeof(Pais), 200)]
        [ProducesResponseType(typeof(ResponseResult), 400)]
        public async Task<IActionResult> CarregarPaisPorId(int id)
        {
            try
            {
                var pais = await _paisRepository.CarregarPorId(id);
                if (pais.IdPais > 0)
                {
                    return Ok(pais);
                }
                return CustomResponse("Não foi encontrado nenhum pais");
            }
            catch (Exception ex)
            {
                return CustomResponse($"Ocorreu um erro ao carregar o país: {ex.Message}");
            }
        }

        [HttpGet("pais/todos")]
        [ProducesResponseType(typeof(IList<Pais>), 200)]
        [ProducesResponseType(typeof(ResponseResult), 400)]
        public async Task<IActionResult> CarregarTodosPaises()
        {
            try
            {
                var paises = await _paisRepository.CarregarTodos();
                return Ok(paises);
            }
            catch (Exception ex)
            {
                return CustomResponse($"Ocorreu um erro ao carregar os países: {ex.Message}");
            }
        }
    }
}

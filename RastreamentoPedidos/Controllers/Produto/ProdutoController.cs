using Microsoft.AspNetCore.Mvc;
using RastreamentoPedido.Core.Communication;
using RastreamentoPedido.Core.Model.ProdutoModel;
using RastreamentoPedido.Core.Repositories.Produtos;
using RastreamentoPedido.Core.Requests.Produto;
using RastreamentoPedido.Core.Response;
using RastreamentoPedido.Core.Response.Produto;
using RastreamentoPedido.WebApi.Core.Controllers;

namespace RastreamentoPedidos.API.Controllers.Produto
{
    [Route("api/produto")]
    [Produces("application/json")]
    public class ProdutoController(
        IProdutoRepository produtoRepository,
        IProdutoMarcaRepository produtoMarcaRepository,
        IProdutoPesoRepository produtoPesoRepository,
        IProdutoEncargoRepository produtoEncargoRepository,
        IProdutoCategoriaRepository produtoCategoriaRepository) : MainController
    {
        private readonly IProdutoRepository _produtoRepository = produtoRepository;
        private readonly IProdutoMarcaRepository _produtoMarcaRepository = produtoMarcaRepository;
        private readonly IProdutoPesoRepository _produtoPesoRepository = produtoPesoRepository;
        private readonly IProdutoEncargoRepository _produtoEncargoRepository = produtoEncargoRepository;
        private readonly IProdutoCategoriaRepository _produtoCategoriaRepository = produtoCategoriaRepository;

        [HttpPost("adicionar")]
        [ProducesResponseType(typeof(ProdutoModel), 201)]
        [ProducesResponseType(typeof(ProdutoInserirRequest), 400)]
        public async Task<IActionResult> AdicionarProduto([FromBody] ProdutoInserirRequest produto)
        {
            LimparErrosProcessamento();
            try
            {
                if (!produto.ValidationResult.IsValid)
                {
                    return CustomResponse(produto.ValidationResult);
                }
                var produtoCodigoBarraExitente = await _produtoRepository.CarregarPorCodigoBarra(produto.CodigoBarras);
                if (produtoCodigoBarraExitente.Id > 0)
                {
                    return CustomResponse("Já existe um produto cadastrado com o código de barras informado.");
                }

                var produtoCodigoExistente = await _produtoRepository.CarregarPorCodigo(produto.Codigo);
                if (produtoCodigoExistente.Id > 0)
                {
                    return CustomResponse("Já existe um produto cadastrado com o código informado.");
                }

                var produtoNomeExistente = await _produtoRepository.CarregarPorNome(produto.Nome);
                if (produtoNomeExistente.Id > 0)
                {
                    return CustomResponse("Já existe um produto cadastrado com o nome informado.");
                }

                ProdutoModel produtoMapeado = await MapearProduto(produto);
                var produtoAdicionado = await _produtoRepository.Inserir(produtoMapeado);
                return Ok(produtoAdicionado);
            }
            catch (Exception ex)
            {

                return CustomResponse(ex);
            }
        }

        [HttpPut("alterar")]
        [ProducesResponseType(typeof(ProdutoModel), 200)]
        [ProducesResponseType(typeof(ResponseResult), 400)]
        public async Task<IActionResult> AlterarProduto([FromBody] ProdutoAlterarRequest produto)
        {
            LimparErrosProcessamento();
            try
            {
                if (produto.IdProduto <= 0)
                {
                    return CustomResponse("O ID do produto deve ser informado.");
                }
                if (!produto.ValidationResult.IsValid)
                {
                    return CustomResponse(produto.ValidationResult);
                }
                var produtoExistente = await _produtoRepository.CarregarPorId(produto.IdProduto);
                if (produtoExistente.Id <= 0)
                {
                    return CustomResponse("Nenhum produto foi encontrado com o ID informado.");
                }
                var produtoCodigoBarraExitente = await _produtoRepository.CarregarPorCodigoBarra(produto.CodigoBarras);
                if (produtoCodigoBarraExitente.Id > 0 && produtoCodigoBarraExitente.Id != produto.IdProduto)
                {
                    return CustomResponse("Já existe um produto cadastrado com o código de barras informado.");
                }
                var produtoCodigoExistente = await _produtoRepository.CarregarPorCodigo(produto.Codigo);
                if (produtoCodigoExistente.Id > 0 && produtoCodigoExistente.Id != produto.IdProduto)
                {
                    return CustomResponse("Já existe um produto cadastrado com o código informado.");
                }
                var produtoNomeExistente = await _produtoRepository.CarregarPorNome(produto.Nome);
                if (produtoNomeExistente.Id > 0 && produtoNomeExistente.Id != produto.IdProduto)
                {
                    return CustomResponse("Já existe um produto cadastrado com o nome informado.");
                }
                ProdutoModel produtoMapeado = await MapearProduto(produto);
                produtoMapeado.Id = produto.IdProduto;
                var produtoAlterado = await _produtoRepository.Alterar(produtoMapeado);
                return Ok(produtoAlterado);
            }
            catch (Exception ex)
            {
                return CustomResponse(ex);
            }
        }

        [HttpGet("listar")]
        [ProducesResponseType(typeof(IEnumerable<ProdutoModel>), 200)]
        [ProducesResponseType(typeof(ResponseResult), 400)]
        public async Task<IActionResult> ListarProdutos([FromQuery] int pagina = 1, [FromQuery] int tamanhoPagina = 10, [FromQuery] string nome = "", [FromQuery] bool ativo = true)
        {
            LimparErrosProcessamento();
            try
            {
                if (pagina <= 0 || tamanhoPagina <= 0)
                {
                    return CustomResponse("Os parâmetros de paginação devem ser maiores que zero.");
                }
                var produtos = await _produtoRepository.ListarTodos(pagina, tamanhoPagina, nome, ativo);
                if (produtos.Count == 0)
                {
                    return CustomResponse("Nenhum produto encontrado.");
                }
                return Ok(produtos);
            }
            catch (Exception ex)
            {
                return CustomResponse(ex);
            }
        }

        [HttpGet("id/{idProduto:int}")]
        [ProducesResponseType(typeof(ProdutoModel), 200)]
        [ProducesResponseType(typeof(ResponseResult), 400)]
        public async Task<IActionResult> CarregarProdutoPorId(int idProduto)
        {
            LimparErrosProcessamento();
            try
            {
                if (idProduto <= 0)
                {
                    return CustomResponse("O ID do produto deve ser deve ser informado");
                }
                var produto = await _produtoRepository.CarregarPorId(idProduto);
                if (produto.Id > 0)
                {
                    return Ok(produto);                   
                }
                return CustomResponse("Nenhum produto foi encontrado");
            }
            catch (Exception ex)
            {
                return CustomResponse(ex);
            }
        }

        [HttpGet("codigo/{codigo}")]
        [ProducesResponseType(typeof(ProdutoModel), 200)]
        [ProducesResponseType(typeof(ResponseResult), 400)]
        public async Task<IActionResult> CarregarProdutoPorCodigo(string codigo)
        {
            LimparErrosProcessamento();
            try
            {
                if (string.IsNullOrEmpty(codigo))
                {
                    return CustomResponse("O código do produto deve ser deve ser informado");
                }
                var produto = await _produtoRepository.CarregarPorCodigo(codigo);
                if (produto.Id > 0)
                {
                    return Ok(produto);
                }

                return CustomResponse("Nenhum produto foi encontrado");
            }
            catch (Exception ex)
            {
                return CustomResponse(ex);
            }
        }

        [HttpGet("codigobarras/{codigoBarras}")]
        [ProducesResponseType(typeof(ProdutoModel), 200)]
        [ProducesResponseType(typeof(ResponseResult), 400)]
        public async Task<IActionResult> CarregarProdutoPorCodigoBarras(string codigoBarras)
        {
            LimparErrosProcessamento();
            try
            {
                if (string.IsNullOrEmpty(codigoBarras))
                {
                    return CustomResponse("O código de barras do produto deve ser deve ser informado");
                }
                var produto = await _produtoRepository.CarregarPorCodigoBarra(codigoBarras);
                if (produto.Id > 0)
                {
                    return Ok(produto);
                }
                return CustomResponse("Nenhum produto foi encontrado");
            }
            catch (Exception ex)
            {
                return CustomResponse(ex);
            }
        }

        [HttpGet("nome/{nome}")]
        [ProducesResponseType(typeof(ProdutoModel), 200)]
        [ProducesResponseType(typeof(ResponseResult), 400)]
        public async Task<IActionResult> CarregarProdutoPorNome(string nome)
        {
            LimparErrosProcessamento();
            try
            {
                if (string.IsNullOrEmpty(nome))
                {
                    return CustomResponse("O nome do produto deve ser deve ser informado");
                }
                var produto = await _produtoRepository.CarregarPorNome(nome);
                if (produto.Id > 0)
                {
                    return Ok(produto);
                }
                return CustomResponse("Nenhum produto foi encontrado");
            }
            catch (Exception ex)
            {
                return CustomResponse(ex);
            }
        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpPost("marca/adicionar")]
        [ProducesResponseType(typeof(ProdutoMarcaResponse), 201)]
        [ProducesResponseType(typeof(ResponseResult), 400)]
        public async Task<IActionResult> AdicionarMarca([FromBody] ProdutoMarcaRequest produtoMarca)
        {
            LimparErrosProcessamento();
            try
            {
                if (string.IsNullOrEmpty(produtoMarca.Nome))
                {
                    return CustomResponse("O nome da marca deve ser informado.");
                }
                var marcaExistente = await _produtoMarcaRepository.CarregarPorNome(produtoMarca.Nome);
                if (marcaExistente.Id > 0)
                {
                    return CustomResponse("Já existe uma marca cadastrada com o nome informado.");
                }
                var marcaAdicionada = await _produtoMarcaRepository.Inserir(new ProdutoMarca 
                {
                    Id = 0, 
                    Nome = produtoMarca.Nome 
                });
                return Ok(marcaAdicionada);
            }
            catch (Exception ex)
            {
                return CustomResponse(ex);
            }
        }
        [HttpGet("marca/listar")]
        [ProducesResponseType(typeof(IEnumerable<ProdutoMarcaResponse>), 200)]
        [ProducesResponseType(typeof(ResponseResult), 400)]
        public async Task<IActionResult> ListarMarcas()
        {
            LimparErrosProcessamento();
            try
            {
                var marcas = await _produtoMarcaRepository.ListarTodos();
                if (marcas.Count == 0)
                {
                    return CustomResponse("Nenhuma marca encontrada.");
                }
                return Ok(marcas);
            }
            catch (Exception ex)
            {
                return CustomResponse(ex);
            }
        }
        [HttpGet("marca/id/{idMarca:int}")]
        [ProducesResponseType(typeof(ProdutoMarcaResponse), 200)]
        [ProducesResponseType(typeof(ResponseResult), 400)]
        public async Task<IActionResult> CarregarMarcaPorId(int idMarca)
        {
            LimparErrosProcessamento();
            try
            {
                if (idMarca <= 0)
                {
                    return CustomResponse("O ID da marca deve ser deve ser informado");
                }
                var marca = await _produtoMarcaRepository.CarregarPorId(idMarca);
                if (marca.Id > 0)
                {
                    return Ok(marca);
                }
                return CustomResponse("Nenhuma marca foi encontrada");
            }
            catch (Exception ex)
            {
                return CustomResponse(ex);
            }
        }
        [HttpGet("marca/nome/{nome}")]
        [ProducesResponseType(typeof(ProdutoMarcaResponse), 200)]
        [ProducesResponseType(typeof(ResponseResult), 400)]
        public async Task<IActionResult> CarregarMarcaPorNome(string nome)
        {
            LimparErrosProcessamento();
            try
            {
                if (string.IsNullOrEmpty(nome))
                {
                    return CustomResponse("O nome da marca deve ser deve ser informado");
                }
                var marca = await _produtoMarcaRepository.CarregarPorNome(nome);
                if (marca.Id < 0)
                {
                    return Ok(marca);
                }
                return CustomResponse("Nenhuma marca foi encontrada");
            }
            catch (Exception ex)
            {
                return CustomResponse(ex);
            }
        }
        [HttpPut("marca/alterar")]
        [ProducesResponseType(typeof(ProdutoMarcaResponse), 200)]
        [ProducesResponseType(typeof(ResponseResult), 400)]
        public async Task<IActionResult> AlterarMarca([FromBody] ProdutoMarcaRequest produtoMarca)
        {
            LimparErrosProcessamento();
            try
            {
                if (produtoMarca.IdMarca <= 0)
                {
                    return CustomResponse("O ID da marca deve ser informado.");
                }
                if (string.IsNullOrEmpty(produtoMarca.Nome))
                {
                    return CustomResponse("O nome da marca deve ser informado.");
                }
                var marcaExistente = await _produtoMarcaRepository.CarregarPorId(produtoMarca.IdMarca);
                if (marcaExistente.Id <= 0)
                {
                    return CustomResponse("Nenhuma marca foi encontrada com o ID informado.");
                }
                var marcaNomeExistente = await _produtoMarcaRepository.CarregarPorNome(produtoMarca.Nome);
                if (marcaNomeExistente.Id > 0 && marcaNomeExistente.Id != produtoMarca.IdMarca)
                {
                    return CustomResponse("Já existe uma marca cadastrada com o nome informado.");
                }
                var marcaAlterada = await _produtoMarcaRepository.Alterar(new ProdutoMarca { Id = produtoMarca.IdMarca, Nome = produtoMarca.Nome});
                return Ok(marcaAlterada);
            }
            catch (Exception ex)
            {
                return CustomResponse(ex);
            }
        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpPost("categoria/adicionar")]
        [ProducesResponseType(typeof(ProdutoCategoriaResponse), 201)]
        [ProducesResponseType(typeof(ResponseResult), 400)]
        public async Task<IActionResult> AdicionarCategoria([FromBody] ProdutoCategoriaRequest produtoCategoria)
        {
            LimparErrosProcessamento();
            try
            {
                if (string.IsNullOrEmpty(produtoCategoria.Nome))
                {
                    return CustomResponse("O nome da categoria deve ser informado.");
                }
                var categoriaExistente = await _produtoCategoriaRepository.CarregarPorNome(produtoCategoria.Nome);
                if (categoriaExistente.Id > 0)
                {
                    return CustomResponse("Já existe uma categoria cadastrada com o nome informado.");
                }
                var categoriaAdicionada = await _produtoCategoriaRepository.Inserir(new ProdutoCategoria
                {
                    Id = 0,
                    Nome = produtoCategoria.Nome
                });
                return Ok(categoriaAdicionada);
            }
            catch (Exception ex)
            {
                return CustomResponse(ex);
            }
        }
        [HttpGet("categoria/listar")]
        [ProducesResponseType(typeof(IEnumerable<ProdutoCategoriaResponse>), 200)]
        [ProducesResponseType(typeof(ResponseResult), 400)]
        public async Task<IActionResult> ListarCategorias()
        {
            LimparErrosProcessamento();
            try
            {
                var categorias = await _produtoCategoriaRepository.ListarTodos();
                if (categorias.Count == 0)
                {
                    return Ok(categorias);
                }
                return CustomResponse("Nenhuma categoria encontrada.");
            }
            catch (Exception ex)
            {
                return CustomResponse(ex);
            }
        }
        [HttpGet("categoria/id/{idCategoria:int}")]
        [ProducesResponseType(typeof(ProdutoCategoriaResponse), 200)]
        [ProducesResponseType(typeof(ResponseResult), 400)]
        public async Task<IActionResult> CarregarCategoriaPorId(int idCategoria)
        {
            LimparErrosProcessamento();
            try
            {
                if (idCategoria <= 0)
                {
                    return CustomResponse("O ID da categoria deve ser deve ser informado");
                }
                var categoria = await _produtoCategoriaRepository.CarregarPorId(idCategoria);
                if (categoria.Id > 0)
                {
                    return Ok(categoria);
                }
                return CustomResponse("Nenhuma categoria foi encontrada");
            }
            catch (Exception ex)
            {
                return CustomResponse(ex);
            }
        }
        [HttpGet("categoria/nome/{nome}")]
        [ProducesResponseType(typeof(ProdutoCategoriaResponse), 200)]
        [ProducesResponseType(typeof(ResponseResult), 400)]
        public async Task<IActionResult> CarregarCategoriaPorNome(string nome)
        {
            LimparErrosProcessamento();
            try
            {
                if (string.IsNullOrEmpty(nome))
                {
                    return CustomResponse("O nome da categoria deve ser deve ser informado");
                }
                var categoria = await _produtoCategoriaRepository.CarregarPorNome(nome);
                if (categoria.Id < 0)
                {
                    return Ok(categoria);
                }
                return CustomResponse("Nenhuma categoria foi encontrada");
            }
            catch (Exception ex)
            {
                return CustomResponse(ex);
            }
        }
        [HttpPut("categoria/alterar")]
        [ProducesResponseType(typeof(ProdutoCategoriaResponse), 200)]
        [ProducesResponseType(typeof(ResponseResult), 400)]
        public async Task<IActionResult> AlterarCategoria([FromBody] ProdutoCategoriaRequest produtoCategoria)
        {
            LimparErrosProcessamento();
            try
            {
                if (produtoCategoria.IdCategoria <= 0)
                {
                    return CustomResponse("O ID da categoria deve ser informado.");
                }
                if (string.IsNullOrEmpty(produtoCategoria.Nome))
                {
                    return CustomResponse("O nome da categoria deve ser informado.");
                }
                var categoriaExistente = await _produtoCategoriaRepository.CarregarPorId(produtoCategoria.IdCategoria);
                if (categoriaExistente.Id <= 0)
                {
                    return CustomResponse("Nenhuma categoria foi encontrada com o ID informado.");
                }
                var categoriaNomeExistente = await _produtoCategoriaRepository.CarregarPorNome(produtoCategoria.Nome);
                if (categoriaNomeExistente.Id > 0 && categoriaNomeExistente.Id != produtoCategoria.IdCategoria)
                {
                    return CustomResponse("Já existe uma categoria cadastrada com o nome informado.");
                }
                var categoriaAlterada = await _produtoCategoriaRepository.Alterar(new ProdutoCategoria { Id = produtoCategoria.IdCategoria, Nome = produtoCategoria.Nome });
                return Ok(categoriaAlterada);
            }
            catch (Exception ex)
            {
                return CustomResponse(ex);
            }
        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpPost("peso/adicionar")]
        [ProducesResponseType(typeof(ProdutoPeso), 201)]
        [ProducesResponseType(typeof(ResponseResult), 400)]
        public async Task<IActionResult> AdicionarPeso([FromBody] ProdutoPeso produtoPeso)
        {
            LimparErrosProcessamento();
            try
            {
                if (produtoPeso.IdProduto <= 0)
                {
                    return CustomResponse("O ID do produto deve ser informado.");
                }
                var produtoExistente = await _produtoRepository.CarregarPorId(produtoPeso.IdProduto);
                if (produtoExistente.Id <= 0)
                {
                    return CustomResponse("Nenhum produto foi encontrado com o ID informado.");
                }
                var pesoAdicionado = await _produtoPesoRepository.Inserir(produtoPeso);
                return Ok(pesoAdicionado);
            }
            catch (Exception ex)
            {
                return CustomResponse(ex);
            }
        }
        [HttpPut("peso/alterar")]
        [ProducesResponseType(typeof(ProdutoPeso), 200)]
        [ProducesResponseType(typeof(ResponseResult), 400)]
        public async Task<IActionResult> AlterarPeso([FromBody] ProdutoPeso produtoPeso)
        {
            LimparErrosProcessamento();
            try
            {
                if (produtoPeso.Id <= 0)
                {
                    return CustomResponse("O ID do peso deve ser informado.");
                }
                if (produtoPeso.IdProduto <= 0)
                {
                    return CustomResponse("O ID do produto deve ser informado.");
                }
                var pesoExistente = await _produtoPesoRepository.CarregarPorId(produtoPeso.Id);
                if (pesoExistente.Id <= 0)
                {
                    return CustomResponse("Nenhum peso foi encontrado com o ID informado.");
                }
                var produtoExistente = await _produtoRepository.CarregarPorId(produtoPeso.IdProduto);
                if (produtoExistente.Id <= 0)
                {
                    return CustomResponse("Nenhum produto foi encontrado com o ID informado.");
                }
                var pesoAlterado = await _produtoPesoRepository.Alterar(produtoPeso);
                return Ok(pesoAlterado);
            }
            catch (Exception ex)
            {
                return CustomResponse(ex);
            }
        }
        [HttpGet("peso/produto/{idProduto:int}")]
        [ProducesResponseType(typeof(ProdutoPeso), 200)]
        [ProducesResponseType(typeof(ResponseResult), 400)]
        public async Task<IActionResult> CarregarPesoPorIdProduto(int idProduto)
        {
            LimparErrosProcessamento();
            try
            {
                if (idProduto <= 0)
                {
                    return CustomResponse("O ID do produto deve ser deve ser informado");
                }
                var peso = await _produtoPesoRepository.CarregarPorIdProduto(idProduto);
                if (peso.Id < 0)
                {
                    return CustomResponse("Nenhum peso foi encontrado");
                }
                return Ok(peso);
            }
            catch (Exception ex)
            {
                return CustomResponse(ex);
            }
        }
        [HttpGet("peso/id/{idPeso:int}")]
        [ProducesResponseType(typeof(ProdutoPeso), 200)]
        [ProducesResponseType(typeof(ResponseResult), 400)]
        public async Task<IActionResult> CarregarPesoPorId(int idPeso)
        {
            LimparErrosProcessamento();
            try
            {
                if (idPeso <= 0)
                {
                    return CustomResponse("O ID do peso deve ser deve ser informado");
                }
                var peso = await _produtoPesoRepository.CarregarPorId(idPeso);
                if (peso.Id < 0)
                {
                    return CustomResponse("Nenhum peso foi encontrado");
                }
                return Ok(peso);
            }
            catch (Exception ex)
            {
                return CustomResponse(ex);
            }
        }
        [HttpGet("peso/listar")]
        [ProducesResponseType(typeof(IEnumerable<ProdutoPeso>), 200)]
        [ProducesResponseType(typeof(ResponseResult), 400)]
        public async Task<IActionResult> ListarPesos([FromQuery] int idProduto = 0)
        {
            LimparErrosProcessamento();
            try
            {
                var pesos = await _produtoPesoRepository.ListarTodos(idProduto);
                if (pesos.Count == 0)
                {
                    return Ok(pesos);
                }
                return CustomResponse("Nenhum peso encontrado.");
            }
            catch (Exception ex)
            {
                return CustomResponse(ex);
            }
        }



        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpPost("encargo/adicionar")]
        [ProducesResponseType(typeof(ProdutoEncargos), 201)]
        [ProducesResponseType(typeof(ResponseResult), 400)]
        public async Task<IActionResult> AdicionarEncargo([FromBody] ProdutoEncargos produtoEncargo)
        {
            LimparErrosProcessamento();
            try
            {
                if (produtoEncargo.IdProduto <= 0)
                {
                    return CustomResponse("O ID do produto deve ser informado.");
                }
                var produtoExistente = await _produtoRepository.CarregarPorId(produtoEncargo.IdProduto);
                if (produtoExistente.Id <= 0)
                {
                    return CustomResponse("Nenhum produto foi encontrado com o ID informado.");
                }
                var encargoAdicionado = await _produtoEncargoRepository.Inserir(produtoEncargo);
                return Ok(encargoAdicionado);
            }
            catch (Exception ex)
            {
                return CustomResponse(ex);
            }
        }
        [HttpPut("encargo/alterar")]
        [ProducesResponseType(typeof(ProdutoEncargos), 200)]
        [ProducesResponseType(typeof(ResponseResult), 400)]
        public async Task<IActionResult> AlterarEncargo([FromBody] ProdutoEncargos produtoEncargo)
        {
            LimparErrosProcessamento();
            try
            {
                if (produtoEncargo.Id <= 0)
                {
                    return CustomResponse("O ID do encargo deve ser informado.");
                }
                if (produtoEncargo.IdProduto <= 0)
                {
                    return CustomResponse("O ID do produto deve ser informado.");
                }
                var encargoExistente = await _produtoEncargoRepository.CarregarPorId(produtoEncargo.Id);
                if (encargoExistente.Id <= 0)
                {
                    return CustomResponse("Nenhum encargo foi encontrado com o ID informado.");
                }
                var produtoExistente = await _produtoRepository.CarregarPorId(produtoEncargo.IdProduto);
                if (produtoExistente.Id <= 0)
                {
                    return CustomResponse("Nenhum produto foi encontrado com o ID informado.");
                }
                var encargoAlterado = await _produtoEncargoRepository.Alterar(produtoEncargo);
                return Ok(encargoAlterado);
            }
            catch (Exception ex)
            {
                return CustomResponse(ex);
            }
        }
        [HttpGet("encargo/produto/{idProduto:int}")]
        [ProducesResponseType(typeof(ProdutoEncargos), 200)]
        [ProducesResponseType(typeof(ResponseResult), 400)]
        public async Task<IActionResult> CarregarEncargoPorIdProduto(int idProduto)
        {
            LimparErrosProcessamento();
            try
            {
                if (idProduto <= 0)
                {
                    return CustomResponse("O ID do produto deve ser deve ser informado");
                }
                var encargo = await _produtoEncargoRepository.CarregarPorIdProduto(idProduto);
                if (encargo.Id > 0)
                {
                    return Ok(encargo);
                }
                return CustomResponse("Nenhum encargo foi encontrado");
            }
            catch (Exception ex)
            {
                return CustomResponse(ex);
            }
        }
        [HttpGet("encargo/id/{idEncargo:int}")]
        [ProducesResponseType(typeof(ProdutoEncargos), 200)]
        [ProducesResponseType(typeof(ResponseResult), 400)]
        public async Task<IActionResult> CarregarEncargoPorId(int idEncargo)
        {
            LimparErrosProcessamento();
            try
            {
                if (idEncargo <= 0)
                {
                    return CustomResponse("O ID do encargo deve ser deve ser informado");
                }
                var encargo = await _produtoEncargoRepository.CarregarPorId(idEncargo);
                if (encargo.Id > 0)
                {
                    return Ok(encargo);
                }
                return CustomResponse("Nenhum encargo foi encontrado");
            }
            catch (Exception ex)
            {
                return CustomResponse(ex);
            }
        }
        [HttpGet("encargo/listar")]
        [ProducesResponseType(typeof(IEnumerable<ProdutoEncargos>), 200)]
        [ProducesResponseType(typeof(ResponseResult), 400)]
        public async Task<IActionResult> ListarEncargos([FromQuery] int idProduto = 0)
        {
            LimparErrosProcessamento();
            try
            {
                var encargos = await _produtoEncargoRepository.ListarPorIdProduto(idProduto);
                if (encargos.Count == 0)
                {
                    return CustomResponse("Nenhum encargo encontrado.");
                }
                return Ok(encargos);
            }
            catch (Exception ex)
            {
                return CustomResponse(ex);
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private async Task<ProdutoModel> MapearProduto(ProdutoInserirRequest produtoRequest)
        {
            return new ProdutoModel
            {
                Id = 0,
                Nome = produtoRequest.Nome,
                Observacao = produtoRequest.Observacao,
                CodigoBarras = produtoRequest.CodigoBarras,
                Codigo = produtoRequest.Codigo,
                UnidadeMedida = produtoRequest.UnidadeMedida,
                PrecoVenda = produtoRequest.PrecoVenda,
                PrecoCusto = produtoRequest.PrecoCusto,
                EstoqueAtual = produtoRequest.EstoqueAtual,
                EstoqueMinimo = produtoRequest.EstoqueMinimo,
                EstoqueMaximo = produtoRequest.EstoqueMaximo,
                EstoqueReservado = produtoRequest.EstoqueReservado,
                Ativo = produtoRequest.Ativo,
                ProdutoCategoria = await _produtoCategoriaRepository.CarregarPorId(produtoRequest.IdCategoria),
                ProdutoMarca = await _produtoMarcaRepository.CarregarPorId(produtoRequest.IdMarca),
                ImagemUrl = produtoRequest.ImagemUrl
            };
        }
        private async Task<ProdutoModel> MapearProduto(ProdutoAlterarRequest produtoRequest)
        {
            return new ProdutoModel
            {
                Id = produtoRequest.IdProduto,
                Nome = produtoRequest.Nome,
                Observacao = produtoRequest.Observacao,
                CodigoBarras = produtoRequest.CodigoBarras,
                Codigo = produtoRequest.Codigo,
                UnidadeMedida = produtoRequest.UnidadeMedida,
                PrecoVenda = produtoRequest.PrecoVenda,
                PrecoCusto = produtoRequest.PrecoCusto,
                EstoqueAtual = produtoRequest.EstoqueAtual,
                EstoqueMinimo = produtoRequest.EstoqueMinimo,
                EstoqueMaximo = produtoRequest.EstoqueMaximo,
                EstoqueReservado = produtoRequest.EstoqueReservado,
                Ativo = produtoRequest.Ativo,
                ProdutoCategoria = await _produtoCategoriaRepository.CarregarPorNome(produtoRequest.Categoria.Nome),
                ProdutoMarca = await _produtoMarcaRepository.CarregarPorNome(produtoRequest.Marca.Nome),
                ImagemUrl = produtoRequest.ImagemUrl
            };
        }
    }
}

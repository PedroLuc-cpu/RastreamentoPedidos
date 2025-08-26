using Dapper;
using RastreamentoPedido.Core.Data;
using RastreamentoPedido.Core.Model.ProdutoModel;
using RastreamentoPedido.Core.Repositories.Produtos;

namespace RastreamentoPedidos.API.Repositories.Produto
{
    public class ProdutoRepository(IDapperContext dapperContext, IProdutoCategoriaRepository produtoCategoria, IProdutoMarcaRepository produtoMarca) : IProdutoRepository
    {
        private readonly IDapperContext _dapperContext = dapperContext;
        private readonly IProdutoCategoriaRepository _produtoCategoriaRepository = produtoCategoria;
        private readonly IProdutoMarcaRepository _produtoMarcaRepository = produtoMarca;
        public async Task<ProdutoModel> Alterar(ProdutoModel produto)
        {
            var sql = """
                UPDATE produtos
                SET nome= @Nome, observacao= @Observacao, "codigoBarras"= @CodigoBarras, codigo= @Codigo, 
                    "unidadeMedida"= @UnidadeMedida, "precoVenda"= @PrecoVenda, "precoCusto"= @PrecoCusto,
                    "estoqueAtual"= @EstoqueAtual, "estoqueMinimo"= @EstoqueMinimo, "estoqueMaximo"= @EstoqueMaximo, 
                    "estoqueReservado"= @EstoqueReservado, ativo= @Ativo, "idCategoria"= @IdCategoria, "idMarca"= @IdMarca, "urlImagem"= @UrlImagem
                WHERE id_produto = @IdProduto;
                """;
            var parametros = new
            {
                IdProduto = produto.Id, 
                produto.Nome,
                produto.Observacao,
                produto.CodigoBarras,
                produto.Codigo,
                produto.UnidadeMedida,
                produto.PrecoVenda,
                produto.PrecoCusto,
                produto.EstoqueAtual,
                produto.EstoqueMinimo,
                produto.EstoqueMaximo,
                produto.EstoqueReservado,
                produto.Ativo,
                IdCategoria = produto.ProdutoCategoria.Id,
                idMarca = produto.ProdutoMarca.Id,
                UrlImagem = produto.ImagemUrl,
            };

            using var conexao = _dapperContext.ConnectionCreate();
            await conexao.ExecuteAsync(sql, parametros);
            return produto;
        }

        public async Task<ProdutoModel> CarregarPorCodigo(string codigo)
        {
            var sql = "SELECT * FROM produtos WHERE codigo = @Codigo";
            var parametros = new {Codigo = codigo};
            using var conexao = _dapperContext.ConnectionCreate();
            var registro = await conexao.QueryFirstOrDefaultAsync(sql, parametros);
            if (registro != null)
            {
                return await PreencherObj(registro);
            }
            return new ProdutoModel();
        }
        public async Task<ProdutoModel> CarregarPorCodigoBarra(string codigoBarra)
        {
            var sql = "SELECT * FROM produtos WHERE \"codigoBarras\" = @CodigoBarra";
            var parametros = new { CodigoBarra = codigoBarra };
            using var conexao = _dapperContext.ConnectionCreate();
            var registro = await conexao.QueryFirstOrDefaultAsync(sql, parametros);
            if (registro != null)
            {
                return await PreencherObj(registro);
            }
            return new ProdutoModel();
        }

        public async Task<ProdutoModel> CarregarPorId(int id)
        {
            var sql = "SELECT * FROM produtos WHERE id_produto = @IdProduto";
            var parametros = new { IdProduto = id };
            using var conexao = _dapperContext.ConnectionCreate();
            var registro = await conexao.QueryFirstOrDefaultAsync(sql, parametros);
            if (registro != null)
            {
                return await PreencherObj(registro);
            }
            return new ProdutoModel();
        }

        public async Task<ProdutoModel> CarregarPorNome(string nome)
        {
            var sql = "SELECT * FROM produtos WHERE nome = @Nome";
            var parametros = new { Nome = nome };
            using var conexao = _dapperContext.ConnectionCreate();
            var registro = await conexao.QueryFirstOrDefaultAsync(sql, parametros);
            if (registro != null)
            {
                return await PreencherObj(registro);
            }
            return new ProdutoModel();
        }

        public async Task<ProdutoModel> Inserir(ProdutoModel produto)
        {
            var sql = """
                INSERT INTO produtos(
                    nome, 
                    observacao, 
                    "codigoBarras", 
                    codigo, 
                    "unidadeMedida", 
                    "precoVenda", 
                    "precoCusto", 
                    "estoqueAtual", 
                    "estoqueMinimo", 
                    "estoqueMaximo", 
                    "estoqueReservado", 
                    ativo,
                    "idCategoria",
                    "idMarca",
                    "urlImagem"
                    )	
                    VALUES (
                    @Nome,
                    @Obs,
                    @CodigoBarras,
                    @Codigo,
                    @UnidadeMedida,
                    @PrecoVenda,
                    @PrecoCusto,
                    @EstoqueAtual, 
                    @EstoqueMinimo,
                    @EstoqueMaximo,
                    @EstoqueReservado, 
                    @Ativo,
                    @IdCategoria,
                    @IdMarca,
                    @UrlImagem);
                """;
            var parametros = new
            {
                produto.Nome,
                Obs = produto.Observacao,
                produto.CodigoBarras,
                produto.Codigo,
                produto.UnidadeMedida,
                produto.PrecoVenda,
                produto.PrecoCusto,
                produto.EstoqueAtual,
                produto.EstoqueMinimo,
                produto.EstoqueMaximo,
                produto.EstoqueReservado,
                produto.Ativo,
                IdCategoria = produto.ProdutoCategoria.Id <= 0 ? null : produto.ProdutoCategoria.Id,
                IdMarca = produto.ProdutoMarca.Id <= 0 ? null : produto.ProdutoMarca.Id,
                UrlImagem = produto.ImagemUrl,
            };
            using var conexao = _dapperContext.ConnectionCreate();
            var id = await conexao.ExecuteScalarAsync<int>(sql, parametros);
            produto.Id = id;
            return produto;
        }

        public async Task<IList<ProdutoModel>> ListarTodos(int pagina, int tamanhoPagina, string nome, bool ativo)
        {
            IList<ProdutoModel> lista = [];
            var sql = """
                SELECT id_produto, nome, observacao, "codigoBarras",
                       codigo, "unidadeMedida", "precoVenda", "precoCusto", 
                       "estoqueAtual", "estoqueMinimo", "estoqueMaximo", 
                       "estoqueReservado", ativo, "dataCadastro", 
                       "idCategoria", "idMarca", "urlImagem"
                FROM produtos WHERE nome ILIKE @Nome AND ativo = @Ativo Limit @Limit OFFSET @OFFSET;
                """;
            var parametros = new
            {
                Nome = $"%{nome}%",
                Ativo = ativo,
                Limit = tamanhoPagina,
                OFFSET = (pagina - 1) * tamanhoPagina
            };
            using var conexao = _dapperContext.ConnectionCreate();
            var registros = await conexao.QueryAsync(sql, parametros);
            if (registros != null)
            {
                
                foreach (var item in registros)
                {
                    lista.Add(await PreencherObj(item));
                }
            }
            return lista;
        }

        private async Task<ProdutoModel> PreencherObj(dynamic item)
        {
            ProdutoModel produto = new();
            try
            {
                produto.Id = item.id_produto;
                produto.Nome = item.nome;
                produto.Observacao = item.observacao;
                produto.CodigoBarras = item.codigoBarras;
                produto.Codigo = item.codigo;
                produto.UnidadeMedida = item.unidadeMedida;
                produto.PrecoVenda = item.precoVenda;
                produto.PrecoCusto = item.precoCusto;
                produto.EstoqueAtual = item.estoqueAtual;
                produto.EstoqueMinimo = item.estoqueMinimo;
                produto.EstoqueMaximo = item.estoqueMaximo;
                produto.EstoqueReservado = item.estoqueReservado;
                produto.Ativo = item.ativo;
                produto.DataCadastro = item.dataCadastro;
                if (item.idCategoria > 0)
                {
                    produto.ProdutoCategoria = await _produtoCategoriaRepository.CarregarPorId(item.idCategoria);
                }
                if (item.idMarca > 0)
                {
                    produto.ProdutoMarca = await _produtoMarcaRepository.CarregarPorId(item.idMarca);
                }
                produto.ImagemUrl = item.urlImagem;
                return produto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "idProduto: " + produto.Id);
            }
        }
    }
}

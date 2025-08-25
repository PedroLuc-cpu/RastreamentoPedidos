using Dapper;
using RastreamentoPedido.Core.Data;
using RastreamentoPedido.Core.Model.ProdutoModel;
using RastreamentoPedido.Core.Repositories.Produtos;

namespace RastreamentoPedidos.API.Repositories.Produto
{
    public class ProdutoMarcaRepository(IDapperContext dapperContext) : IProdutoMarcaRepository
    {
        private readonly IDapperContext _dapperContext = dapperContext;
        public async Task<ProdutoMarca> Alterar(ProdutoMarca produtoMarca)
        {
            var sql = """UPDATE "produtoMarca" SET nome = @nome WHERE id_marca = @id;""";
            var parameters = new { id = produtoMarca.Id, nome = produtoMarca.Nome };
            using var conexao = _dapperContext.ConnectionCreate();
            await conexao.ExecuteAsync(sql, parameters);
            return produtoMarca;
        }

        public async Task<ProdutoMarca> CarregarPorId(int id)
        {
            var sql = """SELECT id_marca AS Id, nome AS Nome FROM "produtoMarca" WHERE id_marca = @id;""";
            var parameters = new { id };
            using var conexao = _dapperContext.ConnectionCreate();
            var registro = await conexao.QueryFirstOrDefaultAsync<ProdutoMarca>(sql, parameters);
            if (registro != null)
            {
                return registro;
            }
            return new ProdutoMarca();
        }

        public async Task<ProdutoMarca> CarregarPorNome(string nome)
        {
            var sql = """SELECT id_marca AS Id, nome AS Nome FROM "produtoMarca" WHERE nome = @nome;""";
            var parameters = new { nome };
            using var conexao = _dapperContext.ConnectionCreate();
            var registro = await conexao.QueryFirstOrDefaultAsync<ProdutoMarca>(sql, parameters);
            if (registro != null)
            {
                return registro;
            }
            return new ProdutoMarca();
        }

        public async Task<ProdutoMarca> Inserir(ProdutoMarca produtoMarca)
        {
            var sql = """INSERT INTO "produtoMarca"(nome) VALUES (@nome);""";
            var parameters = new { nome = produtoMarca.Nome };
            using var conexao = _dapperContext.ConnectionCreate();
            var id = await conexao.ExecuteScalarAsync<int>(sql, parameters);
            produtoMarca.Id = id;
            return produtoMarca;
        }

        public async Task<IList<ProdutoMarca>> ListarTodos()
        {
            IList<ProdutoMarca> lista = [];
            var sql = """SELECT id_marca AS Id, nome AS Nome FROM "produtoMarca";""";
            using var conexao = _dapperContext.ConnectionCreate();
            var registros = await conexao.QueryAsync<ProdutoMarca>(sql);

            if (registros != null)
            {
                foreach (var item in registros)
                {
                    lista.Add(item);
                }
            }
            return lista;
        }
    }
}

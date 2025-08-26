using Dapper;
using RastreamentoPedido.Core.Data;
using RastreamentoPedido.Core.Model.ProdutoModel;
using RastreamentoPedido.Core.Repositories.Produtos;

namespace RastreamentoPedidos.API.Repositories.Produto
{
    public class ProdutoCategoriaRepository(IDapperContext dapperContext) : IProdutoCategoriaRepository
    {
        private readonly IDapperContext _dapperContext = dapperContext;
    
        public async Task<ProdutoCategoria> Alterar(ProdutoCategoria produtoCategoria)
        {
            var sql = """UPDATE "produtoCategoria" SET "nome" = @NomeCategoria WHERE "id" = @Id;""";
            var parameters = new { produtoCategoria.Id, NomeCategoria = produtoCategoria.Nome };
            using var conexao = _dapperContext.ConnectionCreate();
            await conexao.ExecuteAsync(sql, parameters);
            return produtoCategoria;
        }

        public async Task<ProdutoCategoria> CarregarPorId(int id)
        {
            ProdutoCategoria produtoCategoria = new ();
            var sql = """SELECT * FROM "produtoCategoria" WHERE id_categoria = @Id;""";
            var parameters = new { Id = id };
            using var conexao = _dapperContext.ConnectionCreate();
            var registro = await conexao.QueryFirstOrDefaultAsync(sql, parameters);
            if (registro != null)
            {
                produtoCategoria.Id = registro.id_categoria;
                produtoCategoria.Nome = registro.nome;
            }
            return produtoCategoria;
        }

        public async Task<ProdutoCategoria> CarregarPorNome(string nome)
        {
            ProdutoCategoria produtoCategoria = new();
            var sql = """SELECT * FROM "produtoCategoria" WHERE "nome" = @Nome;""";
            var parameters = new { Nome = nome };
            using var conexao = _dapperContext.ConnectionCreate();
            var registro =  await conexao.QueryFirstOrDefaultAsync(sql, parameters);
            if (registro != null)
            {
                produtoCategoria.Id = registro.id_categoria;
                produtoCategoria.Nome = registro.nome;
            }
            return produtoCategoria;
        }

        public async Task<ProdutoCategoria> Inserir(ProdutoCategoria produtoCategoria)
        {
            var sql = """INSERT INTO "produtoCategoria"(nome) VALUES (@NomeCategoria);""";
            var parameters = new { NomeCategoria = produtoCategoria.Nome };
            using var conexao = _dapperContext.ConnectionCreate();
            var id = await conexao.ExecuteScalarAsync<int>(sql, parameters);
            produtoCategoria.Id = id;

            return produtoCategoria;
        }

        public async Task<IList<ProdutoCategoria>> ListarTodos()
        {
            IList<ProdutoCategoria> lista = [];
            var sql = """SELECT * FROM "produtoCategoria";""";
            using var conexao = _dapperContext.ConnectionCreate();
            var registros = await conexao.QueryAsync(sql);
            if (registros != null)
            {
                foreach (var registro in registros)
                {
                    
                    lista.Add(new ProdutoCategoria { 
                        Id = registro.id_categoria, 
                        Nome = registro.nome
                    });
                }
            }
            return lista;
        }
    }
}

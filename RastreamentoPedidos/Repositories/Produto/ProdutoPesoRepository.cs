using Dapper;
using RastreamentoPedido.Core.Data;
using RastreamentoPedido.Core.Model.ProdutoModel;
using RastreamentoPedido.Core.Repositories.Produtos;

namespace RastreamentoPedidos.API.Repositories.Produto
{
    public class ProdutoPesoRepository(IDapperContext dapperContext) : IProdutoPesoRepository
    {
        private readonly IDapperContext _dapperContext = dapperContext;
        public async Task<ProdutoPeso> Alterar(ProdutoPeso produtoPeso)
        {
            var sql = """UPDATE "produtoPeso" SET "pesoBruto" = @PesoBruto, "pesoLiquido" = @PesoLiquido, WHERE "IdProduto" = @IdProduto; """;
            var parametros = new { produtoPeso.PesoBruto, produtoPeso.PesoLiquido };
            using var conexao = _dapperContext.ConnectionCreate();
            await conexao.ExecuteAsync(sql, parametros);
            return produtoPeso;            
        }

        public async Task<ProdutoPeso> CarregarPorId(int id)
        {
            ProdutoPeso produtoPeso = new();
            var sql = """SELECT "id_produtoPeso", "IdProduto", "pesoBruto", "pesoLiquido", "dtPesoAtualizado" FROM "produtoPeso WHERE id_produtoPeso = @Id";""";
            var parametros = new { Id = id };
            using var conexao = _dapperContext.ConnectionCreate();
            var registro = await conexao.QueryFirstOrDefault(sql, parametros);
            if (registro != null)
            {
                produtoPeso.Id = registro.id_produtoPeso;
                produtoPeso.IdProduto = registro.IdProduto;
                produtoPeso.PesoBruto = registro.pesoBruto;
                produtoPeso.PesoLiquido = registro.pesoLiquido;
                produtoPeso.DtPesoAtualizado = registro.dtPesoAtualizado;
            }
            return produtoPeso;

        }

        public async Task<ProdutoPeso> CarregarPorIdProduto(int idProduto)
        {
            ProdutoPeso produtoPeso = new();
            var sql = """SELECT "id_produtoPeso", "IdProduto", "pesoBruto", "pesoLiquido", "dtPesoAtualizado" FROM "produtoPeso WHERE "IdProduto" = @Id";""";
            var parametros = new { Id = idProduto };
            using var conexao = _dapperContext.ConnectionCreate();
            var registro = await conexao.QueryFirstOrDefaultAsync(sql, parametros);
            if (registro != null)
            {
                produtoPeso.Id = registro.id_produtoPeso;
                produtoPeso.IdProduto = registro.IdProduto;
                produtoPeso.PesoBruto = registro.pesoBruto;
                produtoPeso.PesoLiquido = registro.pesoLiquido;
                produtoPeso.DtPesoAtualizado = registro.dtPesoAtualizado;
            }
            return produtoPeso;
        }

        public async Task<ProdutoPeso> Inserir(ProdutoPeso produtoPeso)
        {
            var sql = """INSERT INTO "produtoPeso"("IdProduto", "pesoBruto", "pesoLiquido") VALUES (@IdProduto, @PesoBruto, @PesoLiquido);""";
            var parametros = new { produtoPeso.IdProduto, produtoPeso.PesoBruto, produtoPeso.PesoLiquido };
            using var conexao = _dapperContext.ConnectionCreate();
            var id = await conexao.ExecuteScalarAsync<int>(sql, parametros);
            produtoPeso.Id = id;
            return produtoPeso;
            
        }

        public async Task<IList<ProdutoPeso>> ListarTodos(int idProduto)
        {
            IList<ProdutoPeso> lista = [];
            var sql = """SELECT "id_produtoPeso", "IdProduto", "pesoBruto", "pesoLiquido", "dtPesoAtualizado" FROM "produtoPeso WHERE "IdProduto" = @Id";""";
            var parametros = new { Id = idProduto };
            using var conexao = _dapperContext.ConnectionCreate();
            var registros = await conexao.QueryFirstOrDefaultAsync(sql, parametros);
            if (registros != null)
            {
                foreach (var registro in registros)
                {
                    lista.Add(new ProdutoPeso
                    {
                        Id = registro.id_produtoPeso,
                        IdProduto = registro.IdProduto,
                        PesoBruto = registro.pesoBruto,
                        PesoLiquido = registro.pesoLiquido,
                        DtPesoAtualizado = registro.dtPesoAtualizado,
                    });
                }
            }
            return lista;
        }
    }
}

using Dapper;
using Microsoft.Win32;
using RastreamentoPedido.Core.Data;
using RastreamentoPedido.Core.Model.ProdutoModel;
using RastreamentoPedido.Core.Repositories.Produtos;

namespace RastreamentoPedidos.API.Repositories.Produto
{
    public class ProdutoEncargoRepository(IDapperContext dapperContext) : IProdutoEncargoRepository
    {
        private readonly IDapperContext _dapperContext = dapperContext;
        public async Task<ProdutoEncargos> Alterar(ProdutoEncargos produtoEncargo)
        {
            var sql = """
                UPDATE "produtoEncargos" SET 
                    "ProdutoId" = @IdProduto, 
                    "valorFrete" = @ValorFrete, 
                    "valorSeguro" = @ValorSeguro, 
                    "valorDespesas" = @ValorDespesas, 
                    "valorOutros" = @ValorOutros
                    WHERE "ProdutoId" = @ProdutoId;
                """;
            var paramentro = new
            {
                produtoEncargo.ProdutoId,
                IdProduto = produtoEncargo.ProdutoId,
                produtoEncargo.ValorFrete,
                produtoEncargo.ValorSeguro,
                produtoEncargo.ValorDespesas,
                produtoEncargo.ValorOutros
            };
            using var conexao = _dapperContext.ConnectionCreate();
            await conexao.ExecuteAsync(sql, paramentro);
            return produtoEncargo;
        }

        public async Task<ProdutoEncargos> CarregarPorIdProduto(int id)
        {
            ProdutoEncargos produtoEncargos = new();
            var sql = """SELECT * FROM "produtoEncargos" WHERE "ProdutoId" = @ProdutoId;""";
            var paramentro = new { ProdutoId = id };
            using var conexao = _dapperContext.ConnectionCreate();
            var registro = await conexao.QueryFirstOrDefaultAsync(sql, paramentro);
            if (registro != null)
            {
                produtoEncargos.Id = registro.id_encargos;
                produtoEncargos.ProdutoId = registro.ProdutoId;
                produtoEncargos.ValorFrete = registro.valorFrete;
                produtoEncargos.ValorSeguro = registro.valorSeguro;
                produtoEncargos.ValorDespesas = registro.valorDespesas;
                produtoEncargos.ValorOutros = registro.valorOutros;
            }
            return produtoEncargos;
        }

        public async Task<ProdutoEncargos> CarregarPorId(int id)
        {
            ProdutoEncargos produtoEncargos = new();
            var sql = """SELECT * FROM "produtoEncargos" WHERE "id_encargos" = @Id;""";
            var paramentro = new { Id = id };
            using var conexao = _dapperContext.ConnectionCreate();
            var registro = await conexao.QueryFirstOrDefaultAsync(sql, paramentro);
            if (registro != null)
            {
                produtoEncargos.Id = registro.id_encargos;
                produtoEncargos.ProdutoId = registro.ProdutoId;
                produtoEncargos.ValorFrete = registro.valorFrete;
                produtoEncargos.ValorSeguro = registro.valorSeguro;
                produtoEncargos.ValorDespesas = registro.valorDespesas;
                produtoEncargos.ValorOutros = registro.valorOutros;
            }
            return produtoEncargos;
        }

        public async Task<ProdutoEncargos> Inserir(ProdutoEncargos produtoEncargo)
        {
            var sql = """INSERT INTO "produtoEncargos"("ProdutoId", "valorFrete", "valorSeguro", "valorDespesas", "valorOutros")	VALUES (?, ?, ?, ?, ?);""";
            var paramentro = new { produtoEncargo.ProdutoId, produtoEncargo.ValorFrete, produtoEncargo.ValorSeguro, produtoEncargo.ValorDespesas, produtoEncargo.ValorOutros };
            using var conexao = _dapperContext.ConnectionCreate();
            var id = await conexao.ExecuteScalarAsync<int>(sql, paramentro);
            produtoEncargo.Id = id;
            return produtoEncargo;
        }

        public async Task<IList<ProdutoEncargos>> ListarPorIdProduto(int id)
        {
            IList<ProdutoEncargos> listar = [];
            var sql = """SELECT * FROM "produtoEncargos" WHERE "ProdutoId" = @ProdutoId;""";
            var paramentro = new
            {
                ProdutoId = id,
            };
            using var conexao = _dapperContext.ConnectionCreate();
            var registros = await conexao.QueryFirstOrDefaultAsync(sql, paramentro);
            if (registros != null)
            {
                foreach (var registro in registros)
                {
                    listar.Add(new ProdutoEncargos
                    {
                        Id = registro.id_encargos,
                        ProdutoId = registro.ProdutoId,
                        ValorFrete = registro.valorFrete,
                        ValorSeguro = registro.valorSeguro,
                        ValorDespesas = registro.valorDespesas,
                        ValorOutros = registro.valorOutros,
                    });
                }
            }
            return listar;
        }

    }
}

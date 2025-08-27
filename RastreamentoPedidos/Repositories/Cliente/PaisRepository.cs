using Dapper;
using RastreamentoPedido.Core.Data;
using RastreamentoPedido.Core.Model.Endereco;
using RastreamentoPedido.Core.Repositories.Cliente;

namespace RastreamentoPedidos.API.Repositories.Cliente
{
    public class PaisRepository(IDapperContext dapperContext) : IPaisRepository
    {
        private readonly IDapperContext _dapperContext = dapperContext;
        public async Task<Pais> CarregarPorId(int id)
        {
            Pais pais = new();
            var sql = """SELECT "idPais", nome, sigla, cod_bcb FROM pais WHERE "idPais" = @IdPais;""";
            var parametros = new { IdPais = id };
            using var conexao = _dapperContext.ConnectionCreate();
            var retorno = await conexao.QueryFirstOrDefaultAsync(sql, parametros);
            if (retorno != null)
            {
                pais.IdPais = retorno.idPais;
                pais.Nome = retorno.nome;
                pais.Sigla = retorno.sigla;
                pais.Cod_bcb = retorno.cod_bcb;
            }
            return pais;
        }

        public async Task<IList<Pais>> CarregarTodos()
        {
            IList<Pais> paises = [];
            paises.Clear();
            var sql = """SELECT "idPais", nome, sigla, cod_bcb FROM public.pais;""";
            using var conexao = _dapperContext.ConnectionCreate();
            var registros = await conexao.QueryAsync(sql);
            if (registros != null)
            {
                foreach (var item in registros)
                {
                    paises.Add(new Pais
                    {
                        IdPais = item.idPais,
                        Nome = item.nome,
                        Sigla = item.sigla,
                        Cod_bcb = item.cod_bcb
                    });
                }
            }
            return paises;
        }
    }
}

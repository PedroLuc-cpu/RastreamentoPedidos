using Dapper;
using RastreamentoPedido.Core.Data;
using RastreamentoPedido.Core.Model.Endereco;
using RastreamentoPedido.Core.Repositories.Clientes;

namespace RastreamentoPedidos.Repositories.ClienteRepository
{
    public class CidadeRepository(IUFRepository ufRepository, IDapperContext context) : ICidadeRepository
    {
        private readonly IDapperContext _context = context;
        private readonly IUFRepository _ufRepository = ufRepository;

        public async Task<Cidade> CarregarPorId(int id)
        {
            Cidade cidade = new ();
            var sql = """SELECT * FROM cidade WHERE "idCidade" = @IdCidade """;
            var parametros = new { IdCidade = id }; 
            using var conexao = _context.ConnectionCreate();
            var retorno = await conexao.QueryFirstOrDefaultAsync(sql, parametros);
            if (retorno != null)
            {
                cidade.IdCidade = retorno.idCidade;
                cidade.Nome = retorno.nome;
                cidade.CodIbge = retorno.codIBJE;
                cidade.UF = await _ufRepository.CarregarPorId(retorno.idUF);
            }
            return cidade;
        }

        public async Task<IList<Cidade>> CarregarPorIdUf(int idUf)
        {
            IList<Cidade> cidades = [];
            var sql = """SELECT "idCidade", nome, cep, "idUF", "codIBJE", "integrarSuframa" FROM cidade WHERE "idUF" = @IdUf;""";
            var parametros = new { IdUf = idUf };
            using var conexao = _context.ConnectionCreate();
            var registros = await conexao.QueryAsync(sql, parametros);
            if (registros != null)
            {
                foreach (var item in registros)
                {
                   cidades.Add(new Cidade
                    {
                        IdCidade = item.idCidade,
                        Nome = item.nome,
                        Cep = item.cep,
                        UF = await _ufRepository.CarregarPorId(item.idUF),
                        CodIbge = item.codIBJE,
                        IntegrarSuframa = item.integrarSuframa
                    });

                }
            }
            return cidades;

        }

        public async Task<IList<Cidade>> CarregarPorUF(string uf)
        {
            IList<Cidade> cidades = [];
            var sql = """
                SELECT c."idCidade", c.nome, c.cep, c."idUF", c."codIBJE", c."integrarSuframa" 
                
                         FROM cidade c
                         INNER JOIN uf u ON c."idUF" = u."idUF"
                         WHERE u.sigla = @Sigla;
                """;
            var parametros = new { Sigla = uf };
            using var conexao = _context.ConnectionCreate();
            var registros = await conexao.QueryAsync(sql, parametros);
            if (registros != null)
            {
                foreach (var item in registros)
                {
                    cidades.Add(new Cidade
                    {
                        IdCidade = item.idCidade,
                        Nome = item.nome,
                        Cep = item.cep,
                        UF = await _ufRepository.CarregarPorId(item.idUF),
                        CodIbge = item.codIBJE,
                        IntegrarSuframa = item.integrarSuframa
                    });
                }
            }
            return cidades;
        }

        public async Task<IList<Cidade>> CarregarTodos()
        {
            var cidades = new List<Cidade>();
            cidades.Clear();
            var sql = """SELECT "idCidade", nome, cep, "idUF", "codIBJE", "integrarSuframa" FROM cidade;""";
            using var conexao = _context.ConnectionCreate();
            var registros = await conexao.QueryAsync(sql);
            if (registros != null)
            {
                foreach (var item in registros)
                {
                    cidades.Add(new Cidade
                    {
                        IdCidade = item.idCidade,
                        Nome = item.nome,
                        Cep = item.cep,
                        UF = await _ufRepository.CarregarPorId(item.idUF),
                        CodIbge = item.codIBJE,
                        IntegrarSuframa = item.integrarSuframa
                    });
                }
            }
            return cidades;
        }
    }
}

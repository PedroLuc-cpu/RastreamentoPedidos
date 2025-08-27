using Dapper;
using RastreamentoPedido.Core.Data;
using RastreamentoPedido.Core.Model.Endereco;
using RastreamentoPedido.Core.Repositories.Clientes;
using RastreamentoPedido.Core.Repositories.Encomenda;

namespace RastreamentoPedidos.API.Repositories.Cliente
{
    public class EnderecoRepository(IDapperContext context, ITpLogradouroRepository tpLogradouroRepository, ICidadeRepository cidadeRepository, IEncomendaRepository encomendaRepository) : IEnderecoRepository
    {
        private readonly IDapperContext _context = context;
        private readonly ITpLogradouroRepository _tpLogradouroRepository = tpLogradouroRepository;
        private readonly ICidadeRepository _cidadeRepository = cidadeRepository;
        private readonly IEncomendaRepository _encomendaRepository = encomendaRepository;

        public async Task<Enderecos> Alterar(Enderecos endereco)
        {
            var sql = """
                    UPDATE endereco
                    SET "idCliente"= @IdCidade, "idLogradouro"= @IdLogradouro, 
                        "idCidade"= @IdCidade, "idEncomenda"= @IdEncomenda,
                        complemento= @Complemento, bairro= @Bairro, numero= @Numero, rua= @Rua, cep= @Cep
                    WHERE "idCliente" = @IdCidade;
                """;
            var parametros = new {
                idCliente = endereco.IdPessoa,
                idLogradouro = endereco.IdTpLogradouro,
                idCidade = endereco.IdCidade,
                idEncomenda = endereco.IdEncomenda,
                endereco.Complemento,
                endereco.Bairro,
                endereco.Numero,
                endereco.Rua,
                endereco.CEP
            };
            using var conexao = _context.ConnectionCreate();
            await conexao.ExecuteAsync(sql, parametros);
            return endereco;
        }

        public async Task<IList<Enderecos>> CarregarPorIdCliente(int idCliente)
        {
            IList<Enderecos> enderecos = [];
            enderecos.Clear();
            var sql = """SELECT * FROM enderecos WHERE "idCliente" = @IdCliente """;
            var parametros = new { IdCliente = idCliente };
            using var conexao = _context.ConnectionCreate();
            var result = await conexao.QueryAsync(sql, parametros);

            foreach (var item in result)
            {
                enderecos.Add(PreencherObj(item));
            }
            return enderecos;
        }

        public async Task<Enderecos> Inserir(Enderecos endereco)
        {
            var sql = """
                INSERT INTO endereco(
                    "idCliente", 
                    "idLogradouro", 
                    "idCidade", 
                    "idEncomenda", 
                    complemento, bairro, numero, rua, cep) VALUES(@idCliente, @idLogradouro, @idCidade, @idEncomenda, @Complemento, @Bairro, @Numero, @Rua, @Cep); 
            """;
            var parametros = new
            {
                idCliente = endereco.IdPessoa,
                idLogradouro = endereco.IdTpLogradouro,
                idCidade = endereco.IdCidade,
                idEncomenda = endereco.IdEncomenda,
                endereco.Complemento,
                endereco.Bairro,
                endereco.Numero,
                endereco.Rua,
                endereco.CEP
            };
            using var conexao = _context.ConnectionCreate();
            var idEndereco = await conexao.ExecuteScalarAsync<int>(sql, parametros);
            endereco.Id = idEndereco;
            return endereco;


        }

        private async Task<Enderecos> PreencherObj(dynamic item)
        {
            Enderecos enderecos = new();
            try
            {
                enderecos.Id = item.idEndereco;
                enderecos.IdPessoa = item.idCliente;
                enderecos.Complemento = item.complemento;
                enderecos.Bairro = item.bairro;
                enderecos.Numero = item.numero;
                enderecos.Rua = item.rua;
                enderecos.CEP = item.cep;
                if (item.idEncomenda > 0)
                {
                    enderecos.IdEncomenda = await _encomendaRepository.CarregarEncomendaPorId(item.idEncomenda);
                }
                if (item.idCidade > 0)
                {
                    enderecos.Cidade = await _cidadeRepository.CarregarPorId(item.idCidades);
                }
                
                if (item.idLogradouro > 0)
                {
                    enderecos.TpLogradouro = await _tpLogradouroRepository.CarregarPorId(item.IdTpLogradouro);
                }
                
                return enderecos;
            }
            catch (Exception ex)
            {
                throw new Exception("O ocorreu um erro ao carregar o endereço Id: " + enderecos.Id + ex.Message);
            }
        }
    }

}

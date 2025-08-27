using Dapper;
using Microsoft.EntityFrameworkCore;
using RastreamentoPedido.Core.Data;
using RastreamentoPedido.Core.Model.Clientes;
using RastreamentoPedido.Core.Queries.Clientes;
using RastreamentoPedido.Core.Repositories.Cliente;
using RastreamentoPedido.Core.Repositories.Clientes;
using RastreamentoPedido.Core.Repositories.IEstadoCivilRepository;


namespace RastreamentoPedidos.Repositories.ClienteRepository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly IDapperContext _dapperContext;
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly ITelefoneRepository _telefoneRepository;
        private readonly IEstadoCivilRepository _estadoCivilRepository;


        public ClienteRepository(IDapperContext dapperContext, IEnderecoRepository enderecoRepository, ITelefoneRepository telefoneRepository, IEstadoCivilRepository estadoCivilRepository)
        {
            _dapperContext = dapperContext;
            _enderecoRepository = enderecoRepository;
            _telefoneRepository = telefoneRepository;
            _estadoCivilRepository = estadoCivilRepository;
        }

        public async Task<ClienteModel> Inserir(ClienteModel cliente)
        {
            using (var connection = _dapperContext.ConnectionCreate())
            {
                var paramSQL = ClienteQueries.AdicionarCliente(cliente);
                var id = await connection.ExecuteScalarAsync<int>(paramSQL.Sql, paramSQL.Parametros);
                cliente.IdCliente = id;
            }
            return cliente;
        }

        public async Task<ClienteModel> CarregarPorDocumento(string documento)
        {
            ClienteModel cliente = new();
            using (var connection = _dapperContext.ConnectionCreate())
            {
                var paramSQL = ClienteQueries.CarregarClientePorDocumento(documento);
                var resultado = await connection.QueryFirstOrDefaultAsync(paramSQL.Sql, paramSQL.Parametros);
                if (resultado != null)
                {
                    cliente = await PreencherObjeto(resultado);
                }
            }
            return cliente;

        }

        public async Task<ClienteModel> CarregarPorEmail(string email)
        {
            ClienteModel cliente = new();
            using (var connection = _dapperContext.ConnectionCreate())
            {
                var paramSQL = ClienteQueries.CarregarClientePorEmail(email);
                var resultado = await connection.QueryFirstOrDefaultAsync(paramSQL.Sql, paramSQL.Parametros);
                if (resultado != null)
                {
                    cliente = await PreencherObjeto(resultado);
                }
            }
           return cliente;
        }

        public async Task<ClienteModel> CarregarPorId(int id)
        {
            ClienteModel cliente = new();
            using (var connection = _dapperContext.ConnectionCreate())
            {
                var paramSQL = ClienteQueries.CarregarClientePorId(id);
                var resultado = await connection.QueryFirstOrDefaultAsync(paramSQL.Sql, paramSQL.Parametros);
                if (resultado != null)
                {
                    cliente = await PreencherObjeto(resultado);
                }
            }
            return cliente;
        }

        public async Task<IList<ClienteModel>> CarregarTodos()
        {
            IList<ClienteModel> clientes = [];
            clientes.Clear();
            using (var connection = _dapperContext.ConnectionCreate())
            {
                var paramSQL = ClienteQueries.CarregarTodosClientes();
                var resultado = await connection.QueryAsync(paramSQL.Sql, paramSQL.Parametros);
                if (resultado != null)
                {
                    foreach (var item in resultado)
                    {
                        clientes.Add(await PreencherObjeto(item));
                    }
                }
            }
            return clientes;
        }
        public async Task<ClienteModel> Alterar(ClienteModel cliente)
        {
            var sql = """
                
                UPDATE public.cliente
                SET "idEncomenda" = @IdEncomenda, "idEstadoCivil" = @IdEstadoCivil, "idEmail" = @idEmail,
                    "idEndereco" = @IdEndereco, nome = @Nome, ativo = @Ativo, "dataNascimento" = @DataNascimento,
                    documento = @Documento, email = @Email, sexo = @Sexo
                WHERE "idCliente" = @IdCliente; 
                """;
            var parametros = new
            {
                cliente.IdCliente,
                cliente.IdEncomenda,
                IdEstadoCivil = cliente.EstadoCivil.Id,
                idEmail = cliente.IdEmail,
                idEndereco = cliente.IdEndereco,
                cliente.Nome,
                cliente.Ativo,
                cliente.DataNascimento,
                cliente.Documento,
                cliente.Email,
                cliente.Sexo
            };
            using var conexao = _dapperContext.ConnectionCreate();
            await conexao.ExecuteAsync(sql, parametros);
            return cliente;
        }
        private async Task<ClienteModel> PreencherObjeto(dynamic item)
        {
            ClienteModel cliente = new();
            try
            {
                cliente.IdCliente = item.idCliente;
                cliente.Nome = item.nome;
                cliente.Email = item.email;
                cliente.Ativo = item.ativo;
                cliente.Documento = item.documento;
                cliente.DataNascimento = item.dataNascimento;
                cliente.Sexo = item.sexo;
                if (item.idEstadoCivil != null) {

                    if (item.idEstadoCivil > 0)
                    {
                        cliente.EstadoCivil = await _estadoCivilRepository.CarregarEstadoCivilPorId(item.idEstadoCivil);
                    }
                }
                if (item.idEndereco != null) 
                { 
                    if (item.idEndereco > 0)
                    {
                        cliente.Enderecos = await _enderecoRepository.CarregarPorIdCliente(item.idCliente);
                    }
                }
                ;
                if (item.idTelefone != null) 
                { 
                    if (item.idTelefone > 0)
                    {
                        cliente.Telefones = await _telefoneRepository.CarregarPorIdCliente(item.idCliente);
                    }
                }
                return cliente;                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " IdCliente = " + cliente.IdCliente);
            }
        }
    }
}

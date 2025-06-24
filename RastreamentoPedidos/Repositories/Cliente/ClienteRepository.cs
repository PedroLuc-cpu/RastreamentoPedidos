using Dapper;
using Microsoft.EntityFrameworkCore;
using RastreamentoPedido.Core.Data;
using RastreamentoPedido.Core.Model.Clientes;
using RastreamentoPedido.Core.Queries.Clientes;
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

        public async Task<Cliente> Adicionar(Cliente cliente)
        {
            using (var connection = _dapperContext.ConnectionCreate())
            {
                var paramSQL = ClienteQueries.AdicionarCliente(cliente);
                var id = await connection.ExecuteScalarAsync<int>(paramSQL.Sql, paramSQL.Parametros);
                cliente.IdCliente = id;
            }
            return cliente;
        }

        public async Task<Cliente> CarregarPorDocumento(string documento)
        {
            Cliente cliente = new Cliente();
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

        public async Task<Cliente> CarregarPorEmail(string email)
        {
            Cliente cliente = new Cliente();
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

        public async Task<Cliente> CarregarPorId(int id)
        {
            Cliente cliente = new Cliente();
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

        public async Task<IList<Cliente>> CarregarTodos()
        {
            IList<Cliente> clientes = new List<Cliente>();
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

        private async Task<Cliente> PreencherObjeto(dynamic item)
        {
            Cliente cliente = new Cliente();
            try
            {
                cliente.IdCliente = item.IdCliente;
                cliente.Nome = item.Nome;
                cliente.Email = item.Email;
                cliente.Ativo = item.Ativo;
                cliente.Documento = item.Documento;
                cliente.DataNascimento = item.DataNascimento;
                cliente.Sexo = item.Sexo;
                cliente.EstadoCivilId = item.EstadoCivilId;
                if (item.EstadoCivilId != null) {

                    if (item.EstadoCivilId > 0)
                    {
                        cliente.EstadoCivil = await _estadoCivilRepository.CarregarEstadoCivilPorId(item.EstadoCivilId);
                    }
                }
                cliente.Enderecos = await _enderecoRepository.CarregarPorIdCliente(item.IdCliente);
                cliente.Telefones = await _telefoneRepository.CarregarPorIdCliente(item.IdCliente);

                return cliente;                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " IdCliente = " + cliente.IdCliente);
            }
        }
    }
}

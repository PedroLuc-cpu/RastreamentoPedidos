using Dapper;
using Microsoft.EntityFrameworkCore;
using RastreamentoPedido.Core.Data;
using RastreamentoPedido.Core.Model.Clientes;
using RastreamentoPedido.Core.Queries.Clientes;
using RastreamentoPedido.Core.Repositories.Clientes;


namespace RastreamentoPedidos.Repositories.ClienteRepository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly IDapperContext _dapperContext;
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly ITelefoneRepository _telefoneRepository;


        public ClienteRepository(IDapperContext dapperContext, IEnderecoRepository enderecoRepository, ITelefoneRepository telefoneRepository)
        {
            
            _dapperContext = dapperContext;
            _enderecoRepository = enderecoRepository;
            _telefoneRepository = telefoneRepository;
        }

        public async Task<Cliente> Adicionar(Cliente cliente)
        {
            Cliente clienteCadastrado = new Cliente();

            using (var connection = _dapperContext.ConnectionCreate())
            {
                var paramSQL = ClienteQueries.AdicionarCliente(cliente);
                var id = await connection.ExecuteScalarAsync<int>(paramSQL.Sql, paramSQL.Parametros);
                if (id != 0)
                {
                    clienteCadastrado.IdCliente = id;
                    clienteCadastrado.Documento = cliente.Documento;
                    clienteCadastrado.Email = cliente.Email;
                    clienteCadastrado.Nome = cliente.Nome;
                    clienteCadastrado.Ativo = cliente.Ativo;
                    clienteCadastrado.Sexo = cliente.Sexo;
                    clienteCadastrado.DataNascimento = cliente.DataNascimento;
                    clienteCadastrado.EstadoCivil = cliente.EstadoCivil;
                    //clienteCadastrado.Enderecos = await _enderecoRepository.CarregarPorIdCliente(clienteCadastrado.IdCliente);
                    //clienteCadastrado.Telefones = await _telefoneRepository.CarregarPorIdCliente(clienteCadastrado.IdCliente);

                }
            }
            return clienteCadastrado;

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
            Cliente cliente = new Cliente
            {
                IdCliente = item.idCliente,
                Documento = item.Documento,
                Email = item.Email,
                Nome = item.Nome,
                Ativo = item.Ativo,
                Sexo = item.Sexo,
                DataNascimento = item.DataNascimento,
            };
            cliente.Enderecos = await _enderecoRepository.CarregarPorIdCliente(cliente.IdCliente);
            cliente.Telefones = await _telefoneRepository.CarregarPorIdCliente(cliente.IdCliente);
            return cliente;
        }
    }
}

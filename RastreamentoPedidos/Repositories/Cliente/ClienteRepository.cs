using Microsoft.EntityFrameworkCore;
using RastreamentoPedido.Core.Model.Clientes;
using RastreamentoPedido.Core.Repositories.Clientes;
using RastreamentoPedidos.Data;


namespace RastreamentoPedidos.Repositories.ClienteRepository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly RastreamentoPedidosContext _context;
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly ITelefoneRepository _telefoneRepository;


        public ClienteRepository(RastreamentoPedidosContext context, IEnderecoRepository enderecoRepository, ITelefoneRepository telefoneRepository)
        {
            _context = context;
            _enderecoRepository = enderecoRepository;
            _telefoneRepository = telefoneRepository;
        }

        public async Task<Cliente> Adicionar(Cliente cliente)
        {
            var clientes = new Cliente
            {
                Email = cliente.Email,
                Nome = cliente.Nome,
                Documento = cliente.Documento,
                Enderecos = cliente.Enderecos,
                Telefones = cliente.Telefones
            };

            if (clientes == null) throw new ArgumentNullException(nameof(cliente));

            var ClienteExistente = await _context.Clientes
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Email == cliente.Email);

            if (ClienteExistente != null)
            {
                throw new InvalidOperationException("Já existe um cliente com este e-mail.");
            }

            await _context.Clientes.AddAsync(clientes);
            await _context.SaveChangesAsync();
            return cliente;
        }

        public async Task<Cliente> CarregarPorDocumento(string documento)
        {
            Cliente cliente = new Cliente();
            var resultado = await _context.Clientes.Where(x => x.Documento == documento).FirstOrDefaultAsync();
            if (resultado != null)
            {
                cliente = await PreencherObjeto(resultado);
            }
            return cliente;
        }

        public async Task<Cliente> CarregarPorEmail(string email)
        {
            Cliente cliente = new Cliente();
            var resultado = await _context.Clientes.Where(x => x.Email == email).FirstOrDefaultAsync();
            if (resultado != null)
            {
                cliente = await PreencherObjeto(resultado);
            }
            return cliente;
        }

        public async Task<Cliente> CarregarPorId(int id)
        {
            Cliente cliente = new Cliente();
            var resultado = await _context.Clientes.Where(x => x.IdCliente == id).FirstOrDefaultAsync();
            if (resultado != null)
            {
                cliente = await PreencherObjeto(resultado);
            }
            return cliente;
        }

        public async Task<IList<Cliente>> CarregarTodos()
        {
            IList<Cliente> clientes = new List<Cliente>();
            clientes.Clear();
            var resultado = _context.Clientes.ToListAsync();
            foreach (var item in resultado.Result)
            {
                clientes.Add(await PreencherObjeto(item));
            }
            return clientes;
        }

        private async Task<Cliente> PreencherObjeto(dynamic item)
        {
            Cliente cliente = new Cliente
            {
                IdCliente = item.idCliente,
                Documento = item.documento,
                Email = item.email,
                Nome = item.nome,
            };
            cliente.Enderecos = await _enderecoRepository.CarregarPorIdCliente(cliente.IdCliente);
            cliente.Telefones = await _telefoneRepository.CarregarPorIdCliente(cliente.IdCliente);
            return cliente;
        }
    }
}

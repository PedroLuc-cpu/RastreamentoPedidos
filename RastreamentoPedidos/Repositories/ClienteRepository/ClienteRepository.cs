using Microsoft.EntityFrameworkCore;
using RastreamentoPedidos.Data;
using RastreamentoPedidos.Model.Clientes;
using RastreamentoPedidos.Repositories.Interface.ICliente;

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
                email = cliente.email,
                nome = cliente.nome,
                documento = cliente.documento,
                enderecos = cliente.enderecos,
                telefones = cliente.telefones
            };

            if (clientes == null) throw new ArgumentNullException(nameof(cliente));

            var ClienteExistente = await _context.Clientes
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.email == cliente.email);

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
            var resultado = await _context.Clientes.Where(x => x.documento == documento).FirstOrDefaultAsync();
            if (resultado != null)
            {
                cliente = await PreencherObjeto(resultado);
            }
            return cliente;
        }

        public async Task<Cliente> CarregarPorEmail(string email)
        {
            Cliente cliente = new Cliente();
            var resultado = await _context.Clientes.Where(x => x.email == email).FirstOrDefaultAsync();
            if (resultado != null)
            {
                cliente = await PreencherObjeto(resultado);
            }
            return cliente;
        }

        public async Task<Cliente> CarregarPorId(long id)
        {
            Cliente cliente = new Cliente();
            var resultado = await _context.Clientes.Where(x => x.idCliente == id).FirstOrDefaultAsync();
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
                idCliente = item.idCliente,
                documento = item.documento,
                email = item.email,
                nome = item.nome,
            };
            cliente.enderecos = await _enderecoRepository.CarregarPorIdCliente(cliente.idCliente);
            cliente.telefones = await _telefoneRepository.CarregarPorIdCliente(cliente.idCliente);
            return cliente;
        }
    }
}

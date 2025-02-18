using Microsoft.EntityFrameworkCore;
using RastreamentoPedidos.Data;
using RastreamentoPedidos.Model;
using RastreamentoPedidos.Model.Clientes;
using RastreamentoPedidos.Model.DTO;
using RastreamentoPedidos.Repositories.Interface;

namespace RastreamentoPedidos.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly RastreamentoPedidosContext _context;

        public ClienteRepository( RastreamentoPedidosContext context ) { _context = context; }

        public async Task<ClienteDto> AdicionarClientes(ClienteDto cliente)
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

        public async Task<ClienteDto> CarregarPorEmail(string email)
        {

            var clientePorEmail = await _context.Clientes.Where(x => x.email == email).FirstOrDefaultAsync();
            if (clientePorEmail == null)
            {
                throw new KeyNotFoundException($"Cliente com Id {email} não encontrado.");
            }
            var clientesDTO = new ClienteDto()
            {   idCliente = clientePorEmail.idCliente,
                email = clientePorEmail.email,
                nome = clientePorEmail.nome, 
                telefones = clientePorEmail.telefones,
                encomendas = clientePorEmail.encomendas.Select(b => new EncomendaDTO
                {
                    id_encomenda = b.id_encomenda,
                    data_encomenda = b.data_encomenda,
                    descricao = b.descricao,
                    statusEntregas = b.statusEntregas
                }).ToList()                
            };
            return clientesDTO;
        }

        public async Task<ClienteDto> CarregarPorId(long id)
        {
            var clientePorEmail = await _context.Clientes.Where(x => x.idCliente == id).FirstOrDefaultAsync();
            if (clientePorEmail == null)
            {
                throw new KeyNotFoundException($"Cliente com Id {id} não encontrado.");
            }
            var clientesDTO = new ClienteDto()
            {
                idCliente = clientePorEmail.idCliente,
                email = clientePorEmail.email,
                nome = clientePorEmail.nome,
                telefones = clientePorEmail.telefones,
                encomendas = clientePorEmail.encomendas.Select(b => new EncomendaDTO
                {
                    id_encomenda = b.id_encomenda,
                    data_encomenda = b.data_encomenda,
                    descricao = b.descricao,
                    statusEntregas = b.statusEntregas
                }).ToList()
            };
            return clientesDTO;
        }

        public async Task<IEnumerable<ClienteDto>> CarregarTodos()
        {
            return await _context.Clientes.Select(x => new ClienteDto
            {
                idCliente = x.idCliente,
                email = x.email,
                encomendas = x.encomendas.Select(e => new EncomendaDTO
                {
                    id_encomenda = e.id_encomenda,
                    data_encomenda = e.data_encomenda,
                    descricao = e.descricao,
                    statusEntregas = e.statusEntregas
                }).ToList(),
                nome = x.nome,
                telefones = x.telefones,
            }).ToListAsync();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using RastreamentoPedido.Core.Model.Clientes;
using RastreamentoPedido.Core.Repositories.Clientes;
using RastreamentoPedidos.Data;

namespace RastreamentoPedidos.Repositories.ClienteRepository
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly RastreamentoPedidosContext _context;
        private readonly ITpLogradouroRepository _tpLogradouroRepository;
        private readonly ICidadeRepository _cidadeRepository;
        public EnderecoRepository(RastreamentoPedidosContext context, ITpLogradouroRepository tpLogradouroRepository, ICidadeRepository cidadeRepository )
        {
            _context = context;
            _tpLogradouroRepository = tpLogradouroRepository;
            _cidadeRepository = cidadeRepository;
        }
        public async Task<IList<Endereco>> CarregarPorIdCliente(int idCliente)
        {
            IList<Endereco> enderecos = new List<Endereco>();
            enderecos.Clear();
            var result = await _context.Enderecos.Where(e => e.IdEnderecoCliente == idCliente).ToListAsync();
            foreach (var item in result)
            {
                Endereco endereco = new Endereco();
                endereco.Bairro = item.Bairro;
                endereco.Complemento = item.Complemento;
                endereco.IdCliente = item.IdCliente;
                endereco.IdEnderecoCliente = item.IdEnderecoCliente;
                endereco.CEP = item.CEP;
                endereco.Numero = item.Numero;
                endereco.Rua = item.Rua;
                long? idCidade = item.IdCliente;
                if (idCidade != null)
                {
                    endereco.Cidade = await _cidadeRepository.CarregarPorId(idCidade.Value);
                }
                endereco.TpLogradouro = await _tpLogradouroRepository.CarregarPorId(item.IdTpLogradouro);
                enderecos.Add(endereco);
            }
            return enderecos;
        }
    }
}

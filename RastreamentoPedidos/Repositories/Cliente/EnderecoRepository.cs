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
            var result = await _context.enderecos.Where(e => e.idEnderecoCliente == idCliente).ToListAsync();
            foreach (var item in result)
            {
                Endereco endereco = new Endereco();
                endereco.Bairro = item.Bairro;
                endereco.Complemento = item.Complemento;
                endereco.idCliente = item.idCliente;
                endereco.idEnderecoCliente = item.idEnderecoCliente;
                endereco.CEP = item.CEP;
                endereco.Numero = item.Numero;
                endereco.Rua = item.Rua;
                int? idCidade = item.idCliente;
                if (idCidade != null)
                {
                    endereco.Cidade = await _cidadeRepository.CarregarPorId(idCidade.Value);
                }
                endereco.TpLogradouro = await _tpLogradouroRepository.CarregarPorId(item.idTpLogradouro);
                enderecos.Add(endereco);
            }
            return enderecos;
        }
        
    }
}

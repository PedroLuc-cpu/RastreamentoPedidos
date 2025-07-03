using Microsoft.EntityFrameworkCore;
using RastreamentoPedido.Core.Model.Endereco;
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
        public async Task<IList<Enderecos>> CarregarPorIdCliente(int idCliente)
        {
            IList<Enderecos> enderecos = new List<Enderecos>();
            enderecos.Clear();
            var result = await _context.Enderecos.Where(e => e.Id == idCliente).ToListAsync();
            foreach (var item in result)
            {
                Enderecos endereco = new Enderecos();
                endereco.Bairro = item.Bairro;
                endereco.Complemento = item.Complemento;
                endereco.IdPessoa = item.IdPessoa;
                endereco.Id = item.Id;
                endereco.CEP = item.CEP;
                endereco.Numero = item.Numero;
                endereco.Rua = item.Rua;
                long? idCidade = item.IdPessoa;
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

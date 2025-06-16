using RastreamentoPedido.Core.DomainObjects;
using RastreamentoPedido.Core.Model;
using RastreamentoPedido.Core.Model.Clientes;

namespace RastreamentoPedidos.Model.Encomenda
{
    public class Encomendas : IAggregateRoot
    {
        public int? IdEncomenda { get; set; }
        public int IdCliente { get; set; }
        public Cliente Cliente { get; set; } = new Cliente();
        public DateTime DataEncomenda { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public IList<StatusEntrega> StatusEntregas { get; set; } = new List<StatusEntrega>();
        public IList<Endereco> Localizacao {  get; set; } = new List<Endereco>();
    }
}

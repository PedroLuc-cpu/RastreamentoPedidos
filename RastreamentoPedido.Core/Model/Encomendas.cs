using RastreamentoPedido.Core.DomainObjects;
using RastreamentoPedido.Core.Model;
using RastreamentoPedido.Core.Model.Clientes;

namespace RastreamentoPedidos.Model.Encomenda
{
    public class Encomendas : IAggregateRoot
    {
        public int? id_encomenda { get; set; }
        public int idCliente { get; set; }
        public Cliente cliente { get; set; } = new Cliente();
        public DateTime data_encomenda { get; set; }
        public string descricao { get; set; } = string.Empty;
        public IList<StatusEntrega> statusEntregas { get; set; } = new List<StatusEntrega>();
        public IList<Endereco> localizacao {  get; set; } = new List<Endereco>();
    }
}

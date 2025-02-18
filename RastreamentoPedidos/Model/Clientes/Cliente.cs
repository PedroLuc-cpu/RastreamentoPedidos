using System.Collections.ObjectModel;
using RastreamentoPedidos.DomainObjects;
using RastreamentoPedidos.Model.Encomenda;

namespace RastreamentoPedidos.Model.Clientes
{
    public class Cliente : IAggregateRoot
    {
        public int idCliente { get; set; }
        public long? id_encomenda { get; set; }
        public string nome { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        /// <summary>
        /// CPF e CNPJ do cliente
        /// </summary>
        public string documento {  get; set; } = string.Empty;
        public IList<Endereco>? enderecos { get; set; } = new List<Endereco>();
        public IList<Telefone>? telefones { get; set; } = new List<Telefone>();
        public IList<Encomendas>? encomendas {  get; set; } = new List<Encomendas>();
    }
}

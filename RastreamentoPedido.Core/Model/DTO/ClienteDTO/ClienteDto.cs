using RastreamentoPedido.Core.DomainObjects;
using RastreamentoPedido.Core.Model.Clientes;


namespace RastreamentoPedido.Core.Model.DTO.ClienteDTO
{
    public class ClienteDto : IAggregateRoot
    {
        public long idCliente { get; set; }
        public string nome { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        /// <summary>
        /// CPF e CNPJ do cliente
        /// </summary>
        public string documento { get; set; } = string.Empty;
        public IList<Endereco>? enderecos { get; set; } = new List<Endereco>();
        public IList<Telefone>? telefones { get; set; } = new List<Telefone>();
        public IList<EncomendaDTO>? encomendas { get; set; } = new List<EncomendaDTO>();
    }
}

using RastreamentoPedido.Core.DomainObjects;

namespace RastreamentoPedido.Core.Model.Clientes
{
    public class Telefone: IAggregateRoot
    {
        public int idTelefoneCliente { get; set; }
        public string prefixo { get; set; } = string.Empty;
        public string numero {  get; set; } = string.Empty;
        public int idCliente {  get; set; }
        public bool padrao { get; set; } = false;
    }
}

using RastreamentoPedido.Core.DomainObjects;

namespace RastreamentoPedido.Core.Model.Clientes
{
    public class Telefone: IAggregateRoot
    {
        public int IdTelefoneCliente { get; set; }
        public string Prefixo { get; set; } = string.Empty;
        public string Numero {  get; set; } = string.Empty;
        public int IdCliente {  get; set; }
        public bool Padrao { get; set; } = false;
    }
}

using RastreamentoPedido.Core.DomainObjects;
using System.ComponentModel.DataAnnotations;

namespace RastreamentoPedido.Core.Model.Clientes
{
    public class Telefone: IAggregateRoot
    {
        [Key]
        public int IdTelefoneCliente { get; set; }
        public string Prefixo { get; set; } = string.Empty;
        public string Numero {  get; set; } = string.Empty;
        public int IdCliente {  get; set; }
        public bool Padrao { get; set; } = false;
    }
}

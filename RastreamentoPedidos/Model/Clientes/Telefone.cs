using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RastreamentoPedidos.DomainObjects;

namespace RastreamentoPedidos.Model.Clientes
{
    //[Table("table_cliente")]
    public class Telefone: IAggregateRoot
    {
        //[Key]
        public int idTelefoneCliente { get; set; }
        public string prefixo { get; set; } = string.Empty;
        public string numero {  get; set; } = string.Empty;
        public int idCliente {  get; set; }
        public bool padrao { get; set; } = false;
    }
}

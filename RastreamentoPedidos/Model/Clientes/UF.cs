using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RastreamentoPedidos.DomainObjects;

namespace RastreamentoPedidos.Model.Clientes
{
    //[Table("uf")]
    public class UF : IAggregateRoot
    {
        //[Key]
        public int idUF { get; set; }
        public string sigla { get; set; } = string.Empty;
    }
}

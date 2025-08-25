using RastreamentoPedido.Core.DomainObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RastreamentoPedido.Core.Model.Endereco
{
    [NotMapped]
    public class Cidade : IAggregateRoot
    {
        [Key]
        public int IdCidade { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Cep { get; set; } = string.Empty;
        public int IdUF { get; set; }
        public UF UF { get; set; } = new UF();
        public string CodIbge { get; set; } = string.Empty;
        
    }
}

using RastreamentoPedido.Core.DomainObjects;
using System.ComponentModel.DataAnnotations;

namespace RastreamentoPedido.Core.Model.Endereco
{
    public class UF : IAggregateRoot
    {
        [Key]
        public int IdUF { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Sigla { get; set; } = string.Empty;
        public int IdPais { get; set; }
        public Pais Pais { get; set; } = new Pais();
        public int CodUf { get; set; }
    }
}

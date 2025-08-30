using RastreamentoPedido.Core.DomainObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RastreamentoPedido.Core.Model.Endereco
{
    [NotMapped]
    public class UF : IAggregateRoot
    {
        [Key]
        public int IdUF { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Sigla { get; set; } = string.Empty;
        [JsonIgnore]
        public int IdPais { get; set; }
        public Pais Pais { get; set; } = new Pais();
        public string CodUf { get; set; } = string.Empty;
    }
}

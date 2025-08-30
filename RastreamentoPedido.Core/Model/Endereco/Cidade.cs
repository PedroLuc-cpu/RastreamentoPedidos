using RastreamentoPedido.Core.DomainObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RastreamentoPedido.Core.Model.Endereco
{
    [NotMapped]
    public class Cidade : IAggregateRoot
    {
        [Key]
        public int IdCidade { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Cep { get; set; } = string.Empty;
        [JsonIgnore]
        public int IdUF { get; set; }
        public UF UF { get; set; } = new UF();
        public string CodIbge { get; set; } = string.Empty;
        public bool IntegrarSuframa { get; set; }
    }
}

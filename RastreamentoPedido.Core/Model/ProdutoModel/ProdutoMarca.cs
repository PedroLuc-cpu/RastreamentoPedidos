using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RastreamentoPedido.Core.Model.ProdutoModel
{
    [Table("produtoMarca")]
    public class ProdutoMarca
    {
        [Key]
        [Column("id_marca")]
        public int? Id { get; set; }
        [Column("nome")]
        public string Nome { get; set; } = string.Empty;
        [NotMapped]
        [JsonIgnore]
        public ProdutoModel Produto { get; set; } = new ProdutoModel();
    }
}

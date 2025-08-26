using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RastreamentoPedido.Core.Model.ProdutoModel
{
    [Table("produtoCategoria")]
    public class ProdutoCategoria
    {
        [Key]
        [Column("id_categoria")]
        public int Id { get; set; }
        [ForeignKey("id_produto")]
        [Column("produtoId")]
        public int ProdutoId { get; set; }
        [Column("nome")]
        public string Nome { get; set; } = string.Empty;
        [NotMapped]
        [JsonIgnore]
        public ProdutoModel Produto { get; set; } = new ProdutoModel();

    }
}

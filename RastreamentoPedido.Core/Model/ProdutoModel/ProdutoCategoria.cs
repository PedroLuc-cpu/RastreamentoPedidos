using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    }
}

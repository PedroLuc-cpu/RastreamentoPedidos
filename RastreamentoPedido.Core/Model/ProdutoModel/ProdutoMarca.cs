using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RastreamentoPedido.Core.Model.ProdutoModel
{
    [Table("produtoMarca")]
    public class ProdutoMarca
    {
        [Key]
        [Column("id_marca")]
        public int Id { get; set; }
        [Column("nome")]
        public string Nome { get; set; } = string.Empty;
    }
}

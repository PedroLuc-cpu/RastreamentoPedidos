using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RastreamentoPedido.Core.Model.ProdutoModel
{
    [Table("produtoEncargos")]
    public class ProdutoEncargos
    {
        [Key]
        [Column("id_encargos")]
        public int Id { get; set; }
        [ForeignKey("id_produto")]
        public int ProdutoId { get; set; }
        [Column("dataUltimoEncarco")]
        public DateTime UltimoEncarco;
        [Column("valorFrete")]
        public double ValorFrete { get; set; } = 0.00;
        [Column("valorSeguro")]
        public double ValorSeguro { get; set; } = 0.00;
        [Column("valorDespesas")]
        public double ValorDespesas { get; set; } = 0.00;
        [Column("valorOutros")]
        public double ValorOutros { get; set; } = 0.00;
        [NotMapped]
        public double ValorTotal => ValorFrete + ValorSeguro + ValorDespesas + ValorOutros;
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RastreamentoPedido.Core.Model.ProdutoModel
{
    [Table("produtoPeso")]
    public class ProdutoPeso
    {

        [Key]
        [Column("id_produtoPeso")]
        public int Id { get; set; }
        [ForeignKey("id_produto")]
        public int IdProduto { get; set; }
        [Column("pesoBruto")]
        public double PesoBruto { get; set; } = 0.00;
        [Column("pesoLiquido")]
        public double PesoLiquido { get; set; } = 0.00;
        [Column("dtPesoAtualizado")]
        public DateTime DtPesoAtualizado { get; set; } = DateTime.Now;
        [NotMapped]
        [JsonIgnore]
        public ProdutoModel Produto { get; set; } = new ProdutoModel();
    }
}

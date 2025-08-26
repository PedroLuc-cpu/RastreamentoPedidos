using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RastreamentoPedido.Core.Model.ProdutoModel
{
    [Table("produtos")]
    public class ProdutoModel
    {
        [Key]
        [Column("id_produto")]
        public int Id { get; set; }
        [Column("nome")]
        public string Nome { get; set; } = string.Empty;
        [Column("observacao")]
        public string? Observacao { get; set; }
        [Column("codigoBarras")]
        public string CodigoBarras { get; set; } = string.Empty;
        [Column("codigo")]
        public string Codigo { get; set; } = string.Empty;
        [Column("unidadeMedida")]
        public string? UnidadeMedida { get; set; }
        [Column("precoVenda")]
        public decimal PrecoVenda { get; set; }
        [Column("precoCusto")]
        public decimal? PrecoCusto { get; set; }
        [Column("estoqueAtual")]
        public int? EstoqueAtual { get; set; }
        [Column("estoqueMinimo")]
        public int? EstoqueMinimo { get; set; }
        [Column("estoqueMaximo")]
        public int? EstoqueMaximo { get; set; }
        [Column("estoqueReservado")]
        public int? EstoqueReservado { get; set; }
        [NotMapped]
        public int? EstoqueDisponivel => EstoqueAtual - EstoqueReservado;
        [NotMapped]
        public int? EstoqueTotal => EstoqueAtual + EstoqueReservado;
        [Column("ativo")]
        public bool Ativo { get; set; }
        [Column("dataCadastro")]
        public DateTime DataCadastro { get; set; }
        [Column("idCategoria")]
        [ForeignKey("id_categoria")]
        public int? IdCategoria { get; set; } 
        [Column("idMarca")]
        [ForeignKey("id_marca")]
        public int? IdMarca { get; set; }
        [Column("urlImagem")]
        public string ImagemUrl { get; set; } = string.Empty;
        public ProdutoCategoria ProdutoCategoria { get; set; }
        public ProdutoMarca ProdutoMarca { get; set; }
        public ProdutoPeso ProdutoPeso { get; set; }
        public ProdutoEncargos ProdutoEncargos { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RastreamentoPedido.Core.Model.ProdutoModel
{
    [Table("produtos")]
    public class Produto
    {
        [Key]
        [Column("id_produto")]
        public int Id { get; set; }
        [Column("nome")]
        public string Nome { get; set; } = string.Empty;
        [Column("observacao")]
        public string Observacao { get; set; } = string.Empty;
        [Column("codigoBarras")]
        public string CodigoBarras { get; set; } = string.Empty;
        [Column("codigo")]
        public string Codigo { get; set; } = string.Empty;
        [Column("unidadeMedida")]
        public string UnidadeMedida { get; set; } = string.Empty;
        [Column("precoVenda")]
        public double PrecoVenda { get; set; } = 0.00;
        [Column("precoCusto")]
        public double PrecoCusto { get; set; } = 0.00;
        [Column("estoqueAtual")]
        public double EstoqueAtual { get; set; } = 0.00;
        [Column("estoqueMinimo")]
        public double EstoqueMinimo { get; set; } = 0.00;
        [Column("estoqueMaximo")]
        public double EstoqueMaximo { get; set; } = 0.00;
        [Column("estoqueReservado")]
        public double EstoqueReservado { get; set; } = 0.00;
        [NotMapped]
        public double EstoqueDisponivel => EstoqueAtual - EstoqueReservado;
        [NotMapped]
        public double EstoqueTotal => EstoqueAtual + EstoqueReservado;
        [Column("ativo")]
        public bool Ativo { get; set; } = true;
        [Column("dataCadastro")]
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        [Column("idCategoria")]
        [ForeignKey("id_categoria")]
        public int IdCategoria { get; set; } 
        [Column("idMarca")]
        [ForeignKey("id_marca")]
        public int IdMarca { get; set; }
        [Column("urlImagem")]
        public string ImagemUrl { get; set; } = string.Empty;
        public ProdutoCategoria ProdutoCategoria { get; set; } = new ProdutoCategoria();
        public ProdutoMarca ProdutoMarca { get; set; } = new ProdutoMarca();
        public ProdutoPeso ProdutoPeso { get; set; } = new ProdutoPeso();
        public ProdutoEncargos ProdutoEncargos { get; set; } = new ProdutoEncargos();
    }
}

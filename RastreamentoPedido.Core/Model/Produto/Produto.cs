namespace RastreamentoPedido.Core.Model.Produto
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Observacao { get; set; } = string.Empty;
        public string CodigoBarras { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public string UnidadeMedida { get; set; } = string.Empty;
        public double PrecoVenda { get; set; } = 0.00;
        public double PrecoCusto { get; set; } = 0.00;
        public double EstoqueAtual { get; set; } = 0.00;
        public double EstoqueMinimo { get; set; } = 0.00;
        public double EstoqueMaximo { get; set; } = 0.00;
        public double EstoqueReservado { get; set; } = 0.00;
        public double EstoqueDisponivel => EstoqueAtual - EstoqueReservado;
        public double EstoqueTotal => EstoqueAtual + EstoqueReservado;
        public bool Ativo { get; set; } = true;
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public int IdCategoria { get; set; } 
        public int IdMarca { get; set; }
        public string ImagemUrl { get; set; } = string.Empty;
        public ProdutoCategoria ProdutoCategoria { get; set; } = new ProdutoCategoria();
        public ProdutoMarca ProdutoMarca { get; set; } = new ProdutoMarca();
        public ProdutoPeso ProdutoPeso { get; set; } = new ProdutoPeso();
        public ProdutoEncargos ProdutoEncargos { get; set; } = new ProdutoEncargos();
    }
}

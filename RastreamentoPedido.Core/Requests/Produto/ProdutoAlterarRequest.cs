using FluentValidation;
using FluentValidation.Results;
using System.Text.Json.Serialization;

namespace RastreamentoPedido.Core.Requests.Produto
{
    public class ProdutoAlterarRequest
    {
        public int IdProduto { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Observacao { get; set; } = string.Empty;
        public string CodigoBarras { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public string UnidadeMedida { get; set; } = string.Empty;
        public decimal PrecoVenda { get; set; }
        public decimal PrecoCusto { get; set; }
        public int EstoqueAtual { get; set; }
        public int EstoqueMinimo { get; set; }
        public int EstoqueMaximo { get; set; }
        public int EstoqueReservado { get; set; }
        public bool Ativo { get; set; } = true;
        public ProdutoCategoriaAlterarRequest Categoria { get; set; } = new();
        public ProdutoMarcaRequest Marca { get; set; } = new();
        public string ImagemUrl { get; set; } = string.Empty;

        private readonly ValidationResult _validationResult = new();

        [JsonIgnore]
        public ValidationResult ValidationResult
        {
            get
            {
                var erros = new ProdutoAlterarRequestValidor().Validate(this).Errors;
                _validationResult.Errors.Clear();
                _validationResult.Errors.AddRange(erros);
                return _validationResult;
            }
        }
    }
    public class ProdutoAlterarRequestValidor : AbstractValidator<ProdutoAlterarRequest>
    {
        public ProdutoAlterarRequestValidor()
        {
            RuleFor(p => p.Nome)
                .NotEmpty().WithMessage("O nome do produto é obrigatório.");
            RuleFor(p => p.CodigoBarras)
                .NotEmpty().WithMessage("O código de barras é obrigatório.");
            RuleFor(p => p.Codigo)
                .NotEmpty().WithMessage("O código do produto é obrigatório.");
            RuleFor(p => p.PrecoVenda)
                .GreaterThanOrEqualTo(0).WithMessage("O preço de venda deve ser maior ou igual a zero.");
            RuleFor(p => p.PrecoCusto)
                .GreaterThanOrEqualTo(0).WithMessage("O preço de custo deve ser maior ou igual a zero.");
            RuleFor(p => p.EstoqueAtual)
                .GreaterThanOrEqualTo(0).WithMessage("O estoque atual deve ser maior ou igual a zero.");
            RuleFor(p => p.EstoqueMinimo)
                .GreaterThanOrEqualTo(0).WithMessage("O estoque mínimo deve ser maior ou igual a zero.");
            RuleFor(p => p.EstoqueMaximo)
                .GreaterThanOrEqualTo(0).WithMessage("O estoque máximo deve ser maior ou igual a zero.");
            RuleFor(p => p.EstoqueReservado)
                .GreaterThanOrEqualTo(0).WithMessage("O estoque reservado deve ser maior ou igual a zero.");
        }
    }
}

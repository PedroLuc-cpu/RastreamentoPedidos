using FluentValidation;
using FluentValidation.Results;
using System.Text.Json.Serialization;

namespace RastreamentoPedido.Core.Requests.Cliente
{
    public class EnderecoRequest
    {
        public int IdCliente { get; set; }
        public int IdTpLogradouro { get; set; }
        public int IdCidade { get; set; } 
        public string Numero { get; set; } = string.Empty;
        public string Complemento { get; set; } = string.Empty;
        public string Bairro { get; set; } = string.Empty;
        public string Cep { get; set; } = string.Empty;
        private readonly ValidationResult _validationResult = new();

        [JsonIgnore]
        public ValidationResult ValidationResult
        {
            get
            {
                var erros = new EnderecoRequestValidor().Validate(this).Errors;
                _validationResult.Errors.Clear();
                _validationResult.Errors.AddRange(erros);
                return _validationResult;
            }
        }
    }
    public class EnderecoRequestValidor : AbstractValidator<EnderecoRequest>
    {
        public EnderecoRequestValidor()
        {
            RuleFor(e => e.IdCliente)
                .GreaterThan(0).WithMessage("O ID do cliente deve ser maior que zero.");
            RuleFor(e => e.IdTpLogradouro)
                .GreaterThan(0).WithMessage("O ID do tipo de logradouro deve ser maior que zero.");
            RuleFor(e => e.Numero)
                .NotEmpty().WithMessage("O número é obrigatório.")
                .MaximumLength(20).WithMessage("O número deve ter no máximo 20 caracteres.");
            RuleFor(e => e.Bairro)
                .NotEmpty().WithMessage("O bairro é obrigatório.")
                .MaximumLength(100).WithMessage("O bairro deve ter no máximo 100 caracteres.");
            RuleFor(e => e.IdCidade)
                .GreaterThan(0).WithMessage("O ID da cidade deve ser maior que zero.");
            RuleFor(e => e.Cep)
                .NotEmpty().WithMessage("O CEP é obrigatório.")
                .Matches(@"^\d{5}-\d{3}$").WithMessage("O CEP deve estar no formato 00000-000.");
        }
    }
}

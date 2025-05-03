using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.Results;

namespace RastreamentoPedidos.Model.Usuario
{
    public class UsuarioLogin
    {
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        private ValidationResult validationResult = new ValidationResult();
        [JsonIgnore]
        public ValidationResult ValidationResult
        {
            get
            {
                var erros = new UsuarioLoginValidator().Validate(this).Errors;
                validationResult.Errors.AddRange(erros);
                return validationResult;
            }
        }

        public class UsuarioLoginValidator : AbstractValidator<UsuarioLogin>
        {
            public UsuarioLoginValidator()
            {
                RuleFor(ul => ul.Email).NotNull().NotEmpty().WithMessage("O e-mail é obrigatório.");
                RuleFor(ul => ul.Email).EmailAddress().WithMessage("O e-mail informado está em formato invalido.");
                RuleFor(ul => ul.Senha).NotNull().NotEmpty().WithMessage("A senha é obrigatória.");
            }
        }
    }
}
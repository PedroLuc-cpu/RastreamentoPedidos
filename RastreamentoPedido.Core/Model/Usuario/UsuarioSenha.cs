using FluentValidation;
using FluentValidation.Results;
using System.Text.Json.Serialization;

namespace RastreamentoPedido.Core.Model.Usuario
{
    public class UsuarioSenha
    {
        public string Email { get; set; } = string.Empty;

        public string SenhaAntiga { get; set; } = string.Empty;

        public string SenhaNova { get; set; } = string.Empty;

        private ValidationResult validationResult = new ValidationResult();

        [JsonIgnore]
        public ValidationResult ValidationResult
        {
            get
            {
                var erros = new UsuarioSenhaValidator().Validate(this).Errors;
                validationResult.Errors.AddRange(erros);
                return validationResult;
            }
        }

        public class UsuarioSenhaValidator : AbstractValidator<UsuarioSenha>
        {
            public UsuarioSenhaValidator()
            {
                RuleFor(us => us.Email).NotNull().NotEmpty().WithMessage("O e-mail é obrigatório.");
                RuleFor(us => us.Email).EmailAddress().WithMessage("O e-mail informado está em formato invalido.");
                RuleFor(us => us.SenhaAntiga).NotNull().NotEmpty().WithMessage("A senha antiga é obrigatória.");
                RuleFor(us => us.SenhaNova).NotNull().NotEmpty().WithMessage("A senha nova é obrigatória.");
                RuleFor(us => us.SenhaNova).NotEqual(us => us.SenhaAntiga).WithMessage("A senha nova deve ser diferente da senha antiga.");
                RuleFor(us => us.SenhaNova).MinimumLength(6).MaximumLength(100).WithMessage("A senha nova deve possuir enre 6 e 100 caracteres.");
            }
        }
    }
}

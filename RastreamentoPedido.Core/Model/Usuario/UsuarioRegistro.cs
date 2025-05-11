using FluentValidation;
using System.Text.Json.Serialization;
using FluentValidation.Results;

namespace RastreamentoPedido.Core.Model.Usuario
{
    public class UsuarioRegistro
    {
        public string NomeUsuario { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string NomeCompleto { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public string SenhaConfirmacao { get; set; } = string.Empty;
        public string Funcao { get; set; } = string.Empty;

        private ValidationResult validationResult = new ValidationResult();

        [JsonIgnore]
        public ValidationResult ValidationResult
        {
            get
            {
                var erros = new UsuarioRegistroValidator().Validate(this).Errors;
                validationResult = new ValidationResult(erros);
                return validationResult;
            }
        }

        public class UsuarioRegistroValidator : AbstractValidator<UsuarioRegistro>
        {
            public UsuarioRegistroValidator()
            {
                RuleFor(ur => ur.NomeUsuario).NotNull().NotEmpty().WithMessage("O campo Usuario Bemasoft é obrigatório.");
                RuleFor(ur => ur.NomeUsuario).NotNull().NotEmpty().WithMessage("O campo Senha Bemasoft é obrigatório.");
                RuleFor(ur => ur.Email).NotNull().NotEmpty().WithMessage("O e-mail é obrigatório.");
                RuleFor(ur => ur.Email).EmailAddress().WithMessage("O e-mail informado está em formato invalido.");
                RuleFor(ur => ur.Senha).NotNull().WithMessage("A senha é obrigatória.");
                RuleFor(ur => ur.Senha).NotEmpty().WithMessage("A senha é obrigatória.");
                RuleFor(ur => ur.Senha).MinimumLength(6).WithMessage("A senha deve possuir enre 6 e 100 caracteres.");
                RuleFor(ur => ur.Senha).MaximumLength(100).WithMessage("A senha deve possuir enre 6 e 100 caracteres.");
                RuleFor(ur => ur.SenhaConfirmacao).NotNull().NotEmpty().WithMessage("A confirmação de senha é obrigatória.");
                RuleFor(ur => ur.SenhaConfirmacao).Equal(ur => ur.Senha).WithMessage("As senhas não conferem.");
                RuleFor(ur => ur.Funcao).NotNull().WithMessage("A função é obrigatória.");
                RuleFor(ur => ur.Funcao).NotEmpty().WithMessage("A função é obrigatória.");
            }
        }

    }
}
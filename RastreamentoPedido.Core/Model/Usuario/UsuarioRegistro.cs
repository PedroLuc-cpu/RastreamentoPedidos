using FluentValidation;
using System.Text.Json.Serialization;
using FluentValidation.Results;

namespace RastreamentoPedido.Core.Model.Usuario
{
    public class UsuarioRegistro
    {
        public string nomeUsuario { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public bool ativo { get; set; }
        public string nome { get; set; } = string.Empty;
        public string senha { get; set; } = string.Empty;
        public string senhaConfirmacao { get; set; } = string.Empty;
        public string funcao { get; set; } = string.Empty;
        public string bio { get; set; } = string.Empty;

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
                RuleFor(ur => ur.nomeUsuario).NotNull().NotEmpty().WithMessage("O campo Usuario Bemasoft é obrigatório.");
                RuleFor(ur => ur.nomeUsuario).NotNull().NotEmpty().WithMessage("O campo Senha Bemasoft é obrigatório.");
                RuleFor(ur => ur.email).NotNull().NotEmpty().WithMessage("O e-mail é obrigatório.");
                RuleFor(ur => ur.email).EmailAddress().WithMessage("O e-mail informado está em formato invalido.");
                RuleFor(ur => ur.senha).NotNull().WithMessage("A senha é obrigatória.");
                RuleFor(ur => ur.senha).NotEmpty().WithMessage("A senha é obrigatória.");
                RuleFor(ur => ur.senha).MinimumLength(6).WithMessage("A senha deve possuir enre 6 e 100 caracteres.");
                RuleFor(ur => ur.senha).MaximumLength(100).WithMessage("A senha deve possuir enre 6 e 100 caracteres.");
                RuleFor(ur => ur.senhaConfirmacao).NotNull().NotEmpty().WithMessage("A confirmação de senha é obrigatória.");
                RuleFor(ur => ur.senhaConfirmacao).Equal(ur => ur.senha).WithMessage("As senhas não conferem.");
                RuleFor(ur => ur.funcao).NotNull().WithMessage("A função é obrigatória.");
                RuleFor(ur => ur.funcao).NotEmpty().WithMessage("A função é obrigatória.");
            }
        }

    }
}
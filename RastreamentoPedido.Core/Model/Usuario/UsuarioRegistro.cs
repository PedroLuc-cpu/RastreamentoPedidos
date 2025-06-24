using FluentValidation;
using FluentValidation.Results;
using System.Text.Json.Serialization;

namespace RastreamentoPedido.Core.Model.Usuario
{
    public class UsuarioRegistro
    {
        public string NomeUsuario { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string NomeCompleto { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public string SenhaConfirmacao { get; set; } = string.Empty;
        //public string Funcao { get; set; } = Roles.Usuario;

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
                RuleFor(ur => ur.NomeUsuario).NotNull().NotEmpty().WithMessage("O Usuario é obrigatório.");
                RuleFor(ur => ur.NomeUsuario).NotNull().NotEmpty().WithMessage("O Senha é obrigatório.");
                RuleFor(ur => ur.NomeCompleto).NotNull().NotEmpty().WithMessage("O nome completo é obrigatório.");
                RuleFor(ur => ur.Email).NotNull().NotEmpty().WithMessage("O e-mail é obrigatório.");
                RuleFor(ur => ur.Email).EmailAddress().WithMessage("O e-mail informado está em formato invalido.");
                RuleFor(ur => ur.Senha).NotNull().NotEmpty().WithMessage("A senha é obrigatória.");
                RuleFor(ur => ur.Senha).MinimumLength(6).WithMessage("A senha deve possuir no mínimo 6 caracteres.");
                RuleFor(ur => ur.Senha).MaximumLength(100).WithMessage("A senha deve possuir no máxímo 6 e 100 caracteres.");
                RuleFor(ur => ur.SenhaConfirmacao).NotNull().NotEmpty().WithMessage("A confirmação de senha é obrigatória.");
                RuleFor(ur => ur.SenhaConfirmacao).Equal(ur => ur.Senha).WithMessage("As senhas não conferem.");
            }
        }

    }
}
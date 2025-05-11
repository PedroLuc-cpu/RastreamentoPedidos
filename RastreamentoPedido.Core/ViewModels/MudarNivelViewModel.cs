
using FluentValidation;
using FluentValidation.Results;
using System.Text.Json.Serialization;

namespace RastreamentoPedido.Core.ViewModels
{
    public class MudarNivelViewModel
    {
        public Guid Id { get; set; } = Guid.Empty;
        public string Nivel { get; set; } = string.Empty;

        private readonly ValidationResult validationResult = new ValidationResult();

        [JsonIgnore]
        public ValidationResult ValidationResult
        {
            get 
            {
                var erros = new MudarNivelViewModelValidator().Validate(this).Errors;
                validationResult.Errors.Clear();
                validationResult.Errors.AddRange(erros);
                return validationResult;

            }
        }
        public class MudarNivelViewModelValidator : AbstractValidator<MudarNivelViewModel>
        {
            public MudarNivelViewModelValidator()
            {
                RuleFor(x => x.Id.ToString()).NotNull().WithMessage("Id do usuário não informado!");
                RuleFor(x => x.Id.ToString()).NotEqual(Guid.Empty.ToString()).WithMessage("Id do usuário inválido!");
                RuleFor(x => x.Nivel).NotNull().WithMessage("novo nivel usuário não informado!");
                RuleFor(x => x.Nivel).NotEqual(string.Empty).WithMessage("Novo nivel do usuário inválido!");
            }
        }




    }
}

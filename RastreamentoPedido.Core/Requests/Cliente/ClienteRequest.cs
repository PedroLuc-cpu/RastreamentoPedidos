using FluentValidation;
using FluentValidation.Results;
using RastreamentoPedido.Core.Utils;
using RastreamentoPedido.Core.Utils.ValidacaoStrings;
using System.Text.Json.Serialization;

namespace RastreamentoPedido.Core.Requests.Cliente
{
    public class ClienteRequest
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Documento { get; set; } = string.Empty;
        public EstadoCivilRequest EstadoCivil { get; set; } = new EstadoCivilRequest();
        public bool Ativo { get; set; } = true;
        /// <summary>
        /// SeXo: true = Masculino, false = Feminino
        /// </summary>
        public bool Sexo { get; set; } = true;
        public DateTime DataNascimento { get; set; } = DateTime.MinValue;

        private readonly ValidationResult _validationResult = new ValidationResult();

        [JsonIgnore]
        public ValidationResult ValidationResult { get 
            {
                var erros = new ClienteRequestValidor().Validate(this).Errors;
                _validationResult.Errors.Clear();
                _validationResult.Errors.AddRange(erros);
                return _validationResult;
            } 
        }

        public void Limpar()
        {
            Nome = string.Empty;
            Email = string.Empty;
            Documento = string.Empty;
            EstadoCivil = new EstadoCivilRequest();
            Ativo = true;
            Sexo = true;
            DataNascimento = DateTime.MinValue;
        }

    }

    public class ClienteRequestValidor : AbstractValidator<ClienteRequest>
    {
        public ClienteRequestValidor()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O nome é obrigatório.");
            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("O email é obrigatório.")
                .EmailAddress().WithMessage("O email deve ser válido.");
            RuleFor(c => c.Documento).Must(DocumentoValido).WithMessage("O CPF/CNPJ é inválido");
            RuleFor(c => c.DataNascimento)
                .LessThan(DateTime.Now).WithMessage("A data de nascimento deve ser anterior a hoje.");
        }

        private bool DocumentoValido(string documento)
        {
            bool doc = true;

            if (string.IsNullOrEmpty(documento))
            {
                return false;
            }
            if (documento.RemoveCaracteresEspeciais(documento).Length == 11)
            {
                if (!ValidaCPF.IsCpf(documento))
                {
                    return doc = false;
                }
            }
            else if (documento.RemoveCaracteresEspeciais(documento).Length == 14)
            {
              if (!ValidaCNPJ.IsCnpj(documento))
              {
                    return doc = false;
              }
            }
            return doc;
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<ClienteRequest>.CreateWithOptions((ClienteRequest)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
            {
                return [];
            }
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}

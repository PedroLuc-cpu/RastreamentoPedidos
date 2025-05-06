using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RastreamentoPedido.Core.Communication;

namespace RastreamentoPedido.WebApi.Core.Controllers
{
    [ApiController]
    public abstract class MainController : Controller
    {
        protected ICollection<string> Erros = new List<string>();
        public MainController() { }
        protected ActionResult CustomResponse(object? result = null)
        {
            if (OperacaoValida())
            {
                if (result == null)
                {
                    return Ok();
                }
                return Ok(result);
            }
            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                {"Mensagens", Erros.ToArray() }
            }));
        }

        protected ActionResult CustomResponse(string mensagem)
        {
            if (mensagem != "")
            {
                AdicionarErroProcessamento(mensagem);
            }
            return CustomResponse();
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);

            foreach (var erro in erros)
            {
                AdicionarErroProcessamento(erro.ErrorMessage);
            }
            return CustomResponse();
        }

        protected ActionResult CustomResponse(FluentValidation.Results.ValidationResult validationResult)
        {
            foreach (var erro in validationResult.Errors)
            {
                AdicionarErroProcessamento(erro.ErrorMessage);
            }
            return CustomResponse();
        }

        protected ActionResult CustomResponse(ResponseResult resposta)
        {
            ResponsePossuiErros(resposta);
            return CustomResponse();
        }

        protected bool ResponsePossuiErros(ResponseResult resposta)
        {
            if (resposta == null || !resposta.Errors.Mensagens.Any())
            {
                return false;
            }
            foreach (var mensagem in resposta.Errors.Mensagens)
            {
                AdicionarErroProcessamento(mensagem);
            }
            return true;
        }

        protected void AdicionarErroProcessamento(string erroMessage)
        {
            Erros.Add(erroMessage);
        }

        protected bool OperacaoValida()
        {
            return !Erros.Any();
        }

        protected void LimparErrosProcessamento()
        {
            Erros.Clear();
        }
    }
}

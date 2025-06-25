﻿using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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

        protected IActionResult CustomResponse(ValidationResult validationResult)
        {
            foreach (var erro in validationResult.Errors)
            {
                AdicionarErroProcessamento(erro.ErrorMessage);
            }

            return CustomResponse();
        }
        protected IActionResult CustomResponse(Exception ex)
        {
            AdicionarErroProcessamento(ex.Message);

            return CustomResponse();
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

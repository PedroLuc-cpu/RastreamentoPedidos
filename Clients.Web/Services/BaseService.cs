using Clients.Web.Exceptions;
using RastreamentoPedido.Core.Converters;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Clients.Web.Services
{
    public class BaseService
    {
        protected readonly HttpClient _httpClient;
        protected readonly JsonSerializerOptions _options;

        public BaseService(HttpClient httpClient)
        {
            _options = new JsonSerializerOptions();
            _options.PropertyNameCaseInsensitive = true;
            _options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            _options.Converters.Add(new DateOnlyConverter());
            _options.Converters.Add(new TimeOnlyConverter());
            _options.Converters.Add(new DateTimeConverter());
            _httpClient = httpClient;
        }

        protected bool ValidarResponse(HttpResponseMessage response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                case HttpStatusCode.Forbidden:
                case HttpStatusCode.NotFound:
                case HttpStatusCode.InternalServerError:
                    throw new CustomHttpRequestException(response.StatusCode);
                case HttpStatusCode.BadRequest:
                    return false;
            }
            response.EnsureSuccessStatusCode();
            return true;
        }
        protected StringContent ObterConteudo(object objeto)
        {
            var conteudo = JsonSerializer.Serialize(objeto);
            return new StringContent(conteudo, Encoding.UTF8, "application/json");
        }
        protected async Task<T> DeserializarObjetoResponse<T>(HttpResponseMessage response)
        {
            return JsonSerializer.Deserialize<T>(await response.Content.ReadAsStringAsync(), _options);
        }
    }
}   

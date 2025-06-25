using Blazored.LocalStorage;
using Clients.Web.Contracts.Services.Authentication;
using Clients.Web.Providers;
using Microsoft.AspNetCore.Components.Authorization;
using RastreamentoPedido.Core.Model.Usuario;
using RastreamentoPedido.Core.Requests.Authentication;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Clients.Web.Services.Authentication
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorageService;
        public AuthService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider, ILocalStorageService localStorageService) : base(httpClient)
        {
            _authenticationStateProvider = authenticationStateProvider;
            _localStorageService = localStorageService;

        }

        public async Task<UsuarioRespostaLogin> Login(UsuarioLoginReq loginReq)
        {
            var loginAsJson = JsonSerializer.Serialize(loginReq);
            var reponse = await _httpClient.PostAsync("identidade", new StringContent(loginAsJson, Encoding.UTF8, "application/json"));
            var loginResult = JsonSerializer.Deserialize<UsuarioRespostaLogin>(await reponse.Content.ReadAsStringAsync(), new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                
            });

            if (!reponse.IsSuccessStatusCode)
            {
                return loginResult;
            }

            await _localStorageService.SetItemAsync("accesstoken", loginResult.AccessToken);
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(loginResult);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResult.AccessToken);

            return loginResult;
        }

        public async Task Logout()
        {
            await _localStorageService.RemoveItemAsync("accesstoken");
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}

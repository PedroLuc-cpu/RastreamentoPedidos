using Bogus;
using Microsoft.AspNetCore.Mvc.Testing;
using RastreamentoPedido.Core.Data;
using RastreamentoPedido.Core.Model.Clientes;
using RastreamentoPedido.Core.Model.Usuario;
using RastreamentoPedido.Core.Requests.Cliente;
using RastreamentoPedido.Test.Configuration;
using RastreamentoPedido.Test.Extensions;
using RastreamentoPedidos.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using System.Text.Json;

namespace RastreamentoPedido.Test.TestesApi.Utils
{
    public class MetodosAuxiliares
    {
        private readonly RastreamentoPedidoWebApplicationFactory<Program> _factory;
        private readonly Faker _faker;
        private readonly HttpClient _client;
        private readonly IDapperContext _dapperContext;

        public MetodosAuxiliares(RastreamentoPedidoWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _faker = new Faker("pt_BR");
            _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false,
                BaseAddress = new Uri("http://localhost")

            });
            _dapperContext = new DapperContext(_factory.Configuration);
            
        }

        public HttpClient Client => _client;
        public Faker Faker => _faker;

        public async Task RealizarCadastroDoUsuarioNovo()
        {
            var userData = new UsuarioRegistro
            {
                NomeUsuario = "pedrolucas.dev",
                Email = "pedrolucas@bemasoft.com.br",
                NomeCompleto = "Pedro Lucas Santos dos Santos",
                Senha = "123456",
                SenhaConfirmacao = "123456",
            };
            var response = await _client.PostAsJsonAsync("/api/identidade/nova-conta", userData);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            Sessions.Instance.UserTokenEntregador = "";
            if (result != null)
            {
                UsuarioRespostaLogin? resposta = JsonSerializer.Deserialize<UsuarioRespostaLogin>(result);
                
                if (resposta != null)
                    Sessions.Instance.UserTokenEntregador = resposta.AccessToken;
            }
            _client.AtribuirToken(Sessions.Instance.UserTokenEntregador);
        }

        public async Task RealizarLoginApiEntregador()
        {
            if (!TokenValido(Sessions.Instance.UserTokenEntregador))
            {
                var userData = new UsuarioLogin
                {
                    Email = "pedrolucas@bemasoft.com.br",
                    Senha = "123456"
                };
                var response = await _client.PostAsJsonAsync("/api/identidade/autenticar", userData);
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadAsStringAsync();
                Sessions.Instance.UserTokenEntregador = "";
                if (result != null)
                {
                    UsuarioRespostaLogin? reposta = JsonSerializer.Deserialize<UsuarioRespostaLogin?>(result);

                    if (reposta != null)
                        Sessions.Instance.UserTokenEntregador = reposta.AccessToken;
                }
            }
            _client.AtribuirToken(Sessions.Instance.UserTokenEntregador);
        }

        public async Task<HttpResponseMessage> CriarCliente(ClienteInserirRequest objeto)
        {
            var response = await _client.PostAsJsonAsync("/api/cliente/adicionar", objeto);
            return response;
        }

        public async Task<HttpResponseMessage> AlterarClientes(ClienteAlterarResquest objeto)
        {
            var response = await _client.PutAsJsonAsync("/api/cliente/alterar", objeto);
            return response;
        }

        public ClienteInserirRequest GerarInserirClienteValido()
        {
            var cliente = new ClienteInserirRequest
            {
                Nome = _faker.Person.FullName,
                Documento = GeradorCpfValido.GerarCPF(),
                Email = _faker.Internet.Email(),
                DataNascimento = _faker.Person.DateOfBirth,
                Ativo = true,
                EstadoCivil = new EstadoCivilRequest { EstadoCivil = "solteiro(a)"},
                Sexo = true,
            };
            return cliente;
        }

        public ClienteAlterarResquest GerarAlteraClienteValido(ClienteModel cliente)
        {
            return new ClienteAlterarResquest
            {
                IdCliente = cliente.IdCliente,
                Nome = cliente.Nome,
                Documento = cliente.Documento,
                Email = cliente.Email,
                DataNascimento = cliente.DataNascimento,
                Ativo = cliente.Ativo,
                EstadoCivil = new EstadoCivilRequest { EstadoCivil = cliente.EstadoCivil.EstadoCivilDescricao },
                Sexo = cliente.Sexo,
            };
        }


        private static bool TokenValido(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return false;
            }
            var handler = new JwtSecurityTokenHandler();
            if (handler.ReadToken(token) is not JwtSecurityToken jwtToken)
            {
                return false;
            }
            var dataExpiracao = jwtToken.ValidTo;
            if (dataExpiracao < DateTime.UtcNow)
            {
                return false;
            }
            return true;
        }

    }
}

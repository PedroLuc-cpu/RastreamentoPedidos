using RastreamentoPedido.Core.Converters;
using RastreamentoPedido.Core.Model.Clientes;
using RastreamentoPedido.Core.Requests.Cliente;
using RastreamentoPedido.Test.Configuration;
using RastreamentoPedido.Test.TestesApi.Utils;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace RastreamentoPedido.Test.TestesApi.Clientes
{
    public class ClientesControllerTestes : IClassFixture<RastreamentoPedidoWebApplicationFactory<Program>>
    {
        private readonly JsonSerializerOptions _options;
        private readonly MetodosAuxiliares _aux;
        private const string BASE_PATH = "/api/cliente";

        public ClientesControllerTestes(RastreamentoPedidoWebApplicationFactory<Program> factory)
        {
            _options = new JsonSerializerOptions();
            _options.Converters.Add(new DateTimeConverter());
            _options.PropertyNameCaseInsensitive = true;
            _aux = new MetodosAuxiliares(factory);
        }

        #region Testes da rota para cadastrar um novo cliente
        [Fact]
        [Trait("Clientes", "Cadastrar um cliente novo")]
        public async Task ClientesController_ApiClientes_NovoCliente_DeveRetornarSucesso()
        {
            await _aux.RealizarLoginApiEntregador();
            var response = await _aux.CriarCliente(new ClienteInserirRequest
            {
                Nome = _aux.GerarInserirClienteValido().Nome,
                Ativo = true,
                DataNascimento = _aux.GerarInserirClienteValido().DataNascimento,
                Documento = _aux.GerarInserirClienteValido().Documento,
                Email = _aux.GerarInserirClienteValido().Email,
                EstadoCivil = new EstadoCivilRequest { EstadoCivil = "solteiro(a)" },
                Sexo = true,
            });
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        [Trait("Clientes", "Cadastrar um cliente novo com documento já cadastrado")]
        public async Task ClientesController_ApiClientes_NovoCliente_DocumentoJaExiste_DeveFalhar()
        {
            await _aux.RealizarLoginApiEntregador();
            var response = await _aux.CriarCliente(new ClienteInserirRequest
            {
                Nome = "Pedro Lucas Santos dos Santos",
                Ativo = true,
                DataNascimento = _aux.GerarInserirClienteValido().DataNascimento,
                Documento = "06238390085",
                Email = _aux.GerarInserirClienteValido().Email,
                EstadoCivil = new EstadoCivilRequest { EstadoCivil = "solteiro(a)" },
                Sexo = true,
            });
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            var menssagem = await response.Content.ReadAsStringAsync();
            Assert.Contains("Já existe um cliente cadastrado com este documento.", menssagem);
        }

        [Fact]
        [Trait("Clientes", "Cadastrar um cliente novo com email já cadastrado")]
        public async Task ClientesController_ApiClientes_NovoCliente_EmailJaExiste_DeveFalhar()
        {
            await _aux.RealizarLoginApiEntregador();
            var response = await _aux.CriarCliente(new ClienteInserirRequest
            {
                Nome = "Pedro Lucas Santos dos Santos",
                Ativo = true,
                DataNascimento = new DateTime().Date,
                Documento = "06238390085",
                Email = "pedrolucassantos@gmail.com.br",
                EstadoCivil = new EstadoCivilRequest { EstadoCivil = "solteiro(a)" },
                Sexo = true,
            });
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            var menssagem = await response.Content.ReadAsStringAsync();
            Assert.Contains("Já existe um cliente cadastrado com este e-mail.", menssagem);

        }

        [Fact]
        [Trait("Clientes", "Cadastrar um cliente novo com documento inválido")]
        public async Task ClientesController_ApiClientes_NovoCliente_DocumentoInvalido_DeveFalhar()
        {
            await _aux.RealizarLoginApiEntregador();
            var response = await _aux.CriarCliente(new ClienteInserirRequest
            {
                Nome = "Pedro Lucas Santos dos Santos",
                Ativo = true,
                DataNascimento = new DateTime().Date,
                Documento = "11231190115",
                Email = "pedrolucassantos@gmail.com.br",
                EstadoCivil = new EstadoCivilRequest { EstadoCivil = "solteiro(a)" },
                Sexo = true,
            });
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            var menssagem = await response.Content.ReadAsStringAsync();
            Assert.Contains("O CPF/CNPJ é inválido", menssagem);

        }
        #endregion

        #region Teste da rota para listar todos clientes
        [Fact]
        [Trait("Clientes", "Carregar todos os clientes")]
        public async Task ClientesController_ApiClientes_DeveRetornarSucessoEListarTodosOsClientes()
        {
            await _aux.RealizarLoginApiEntregador();
            var response = await _aux.Client.GetAsync(BASE_PATH);
            string strResponse = await response.Content.ReadAsStringAsync();
            Assert.True(response.IsSuccessStatusCode, $"Erro ao carregar a lista de clientes: {response.StatusCode} - {strResponse}");
            IEnumerable<ClienteModel>? clientes = JsonSerializer.Deserialize<IEnumerable<ClienteModel>>(strResponse, _options);
            Assert.NotNull(clientes);
            Assert.True(clientes.Count() > 0);
        }
        #endregion
    }
}
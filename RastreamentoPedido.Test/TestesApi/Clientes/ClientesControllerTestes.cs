using RastreamentoPedido.Core.Converters;
using RastreamentoPedido.Core.Model.Clientes;
using RastreamentoPedido.Test.Configuration;
using RastreamentoPedido.Test.TestesApi.Utils;
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

        [Fact]
        [Trait("Clientes", "Carregar todos os clientes")]
        public async Task ClientesController_ApiClientes_DeveRetornarSucessoEListarTodosOsClientes()
        {
            await _aux.RealizarLoginApiAdministrador();
            var response = await _aux.Client.GetAsync(BASE_PATH);
            string strResponse = await response.Content.ReadAsStringAsync();
            Assert.True(response.IsSuccessStatusCode, $"Erro ao carregar a lista de clientes: {response.StatusCode} - {strResponse}");
            IEnumerable<ClienteModel>? clientes = JsonSerializer.Deserialize<IEnumerable<ClienteModel>>(strResponse, _options);
            Assert.NotNull( clientes );
            Assert.True(clientes.Count() > 0);
        }
    }
}

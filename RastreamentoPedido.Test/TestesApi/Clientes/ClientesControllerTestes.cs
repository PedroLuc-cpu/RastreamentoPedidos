using RastreamentoPedido.Test.Configuration;
using RastreamentoPedido.Test.TestesApi.Utils;
using System.Text.Json;

namespace RastreamentoPedido.Test.TestesApi.Clientes
{
    public class ClientesControllerTestes(JsonSerializerOptions jsonSerializerOptions, MetodosAuxiliares) : IClassFixture<RastreamentoPedidoWebApplicationFactory<Program>>
    {
    }
}

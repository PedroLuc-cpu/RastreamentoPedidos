using System.Net.Http.Headers;

namespace RastreamentoPedido.Test.Extensions
{
    public static class TestsExtensions
    {
        public static decimal ApenasNumeros(this string valor)
        {
            return Convert.ToDecimal(new string(valor.Where(char.IsDigit).ToArray()));
        }

        public static void AtribuirJsonMediaType(this HttpClient client)
        {
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static void AtribuirToken(this HttpClient client, string token)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}

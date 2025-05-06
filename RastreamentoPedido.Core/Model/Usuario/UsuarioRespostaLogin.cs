using System.Text.Json.Serialization;

namespace RastreamentoPedido.Core.Model.Usuario
{
    public class UsuarioRespostaLogin
    {
        [JsonPropertyName("accessToken")]
        public string AccessToken { get; set; } = string.Empty;
        public double ExpiresIn { get; set; }
        public UsuarioToken UsuarioToken { get; set; } = new UsuarioToken();
    }
}
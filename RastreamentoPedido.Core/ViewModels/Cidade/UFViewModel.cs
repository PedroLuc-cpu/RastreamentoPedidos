using System.Text.Json.Serialization;

namespace RastreamentoPedido.Core.ViewModels.Cidade
{
    public class UFViewModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("sigla")]
        public string Sigla { get; set; } = string.Empty;
        [JsonPropertyName("nome")]
        public string Nome { get; set; } = string.Empty;
        [JsonPropertyName("regiao")]
        public RegiaoViewModel Regiao { get; set; } = new RegiaoViewModel();

    }
}

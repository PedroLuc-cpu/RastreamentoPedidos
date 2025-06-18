using System.Text.Json.Serialization;

namespace RastreamentoPedido.Core.ViewModels.Cidade
{
    public class RegiaoImediataViewModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("nome")]
        public string Nome { get; set; } = string.Empty;
        [JsonPropertyName("regiao-intermediaria")]
        public RegiaoIntermediariaViewModel RegiaoIntermediaria { get; set; } = new RegiaoIntermediariaViewModel();
    }
}
    
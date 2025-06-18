using System.Text.Json.Serialization;

namespace RastreamentoPedido.Core.ViewModels.Cidade
{
    public class RegiaoIntermediariaViewModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("nome")]
        public string Nome { get; set; } = string.Empty;
        [JsonPropertyName("UF")]
        public UFViewModel UF { get; set; } = new UFViewModel();
    }
}

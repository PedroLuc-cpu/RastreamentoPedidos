using System.Text.Json.Serialization;

namespace RastreamentoPedido.Core.ViewModels.Cidade
{
    public class CidadeViewModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("nome")]
        public string Nome { get; set; } = string.Empty;
        [JsonPropertyName("microrregiao")]
        public MicrorRegiaoViewModel MicrorRegiao { get; set; } = new MicrorRegiaoViewModel();
        [JsonPropertyName("regiao-imediata")]
        public RegiaoImediataViewModel RegiaoImediata { get; set; } = new RegiaoImediataViewModel();

    }
}

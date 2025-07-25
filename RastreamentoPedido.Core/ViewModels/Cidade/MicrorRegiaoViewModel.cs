﻿using System.Text.Json.Serialization;

namespace RastreamentoPedido.Core.ViewModels.Cidade
{
    public class MicrorRegiaoViewModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("nome")]
        public string Nome { get; set; } = string.Empty;
        [JsonPropertyName("mesorregiao")]
        public MesorRegiaoViewModel MesorRegiao { get; set; } = new MesorRegiaoViewModel();

    }
}

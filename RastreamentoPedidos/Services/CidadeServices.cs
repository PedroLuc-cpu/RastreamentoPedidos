using RastreamentoPedido.Core.Service;
using RastreamentoPedido.Core.ViewModels.Cidade;
using System.Text.Json;

namespace RastreamentoPedidos.API.Services
{
    public class CidadeServices : ICidadeService
    {
        private readonly HttpClient _httpClient;

        public CidadeServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IList<CidadeViewModel>> BuscarCidadePorEstado(string sigla)
        {
            IList<CidadeViewModel> cidades = new List<CidadeViewModel>();

            var url = $"https://servicodados.ibge.gov.br/api/v1/localidades/estados/{sigla}/municipios";
            
            var resposta = await _httpClient.GetAsync(url);

            if (resposta.IsSuccessStatusCode)
            {
                var JSON = await resposta.Content.ReadAsStringAsync();
                var cidade = JsonSerializer.Deserialize<List<CidadeViewModel>>(JSON, new JsonSerializerOptions 
                { 
                    PropertyNameCaseInsensitive = true
                });

                foreach (var item in cidade)
                {
                    cidades.Add(new CidadeViewModel
                    {
                        Id= item.Id,
                        Nome = item.Nome,
                        MicrorRegiao = new MicrorRegiaoViewModel
                        {
                            Id = item.MicrorRegiao.Id,
                            Nome = item.MicrorRegiao.Nome,
                            MesorRegiao = new MesorRegiaoViewModel
                            {
                                Id = item.MicrorRegiao.MesorRegiao.Id,
                                Nome = item.MicrorRegiao.MesorRegiao.Nome,
                                UF = new UFViewModel
                                {
                                    Id = item.MicrorRegiao.MesorRegiao.UF.Id,
                                    Sigla = item.MicrorRegiao.MesorRegiao.UF.Sigla,
                                    Nome = item.MicrorRegiao.MesorRegiao.UF.Nome,
                                    Regiao = new RegiaoViewModel
                                    {
                                        Id = item.MicrorRegiao.MesorRegiao.UF.Regiao.Id,
                                        Nome = item.MicrorRegiao.MesorRegiao.UF.Regiao.Nome,
                                        Sigla = item.MicrorRegiao.MesorRegiao.UF.Regiao.Sigla,
                                    }
                                }
                            }
                        },
                        RegiaoImediata = new RegiaoImediataViewModel
                        {
                            Id = item.RegiaoImediata.Id,
                            Nome = item.RegiaoImediata.Nome,
                            RegiaoIntermediaria = new RegiaoIntermediariaViewModel
                            {
                                Id = item.RegiaoImediata.RegiaoIntermediaria.Id,
                                Nome = item.RegiaoImediata.RegiaoIntermediaria.Nome,
                                UF = new UFViewModel
                                {
                                    Id = item.RegiaoImediata.RegiaoIntermediaria.UF.Id,
                                    Sigla = item.RegiaoImediata.RegiaoIntermediaria.UF.Sigla,
                                    Nome = item.RegiaoImediata.RegiaoIntermediaria.UF.Nome,
                                    Regiao = new RegiaoViewModel
                                    {
                                        Id = item.RegiaoImediata.RegiaoIntermediaria.UF.Regiao.Id,
                                        Nome = item.RegiaoImediata.RegiaoIntermediaria.UF.Regiao.Nome,
                                        Sigla = item.RegiaoImediata.RegiaoIntermediaria.UF.Regiao.Sigla,
                                    }
                                }
                            }
                        }                        
                    });
                }
            }
                return cidades;                    
        }
    }
}

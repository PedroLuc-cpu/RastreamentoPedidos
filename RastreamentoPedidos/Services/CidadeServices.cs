using RastreamentoPedido.Core.Model.Clientes;
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
                var cidade = JsonSerializer.Deserialize<List<CidadeViewModel>>(JSON);

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
                            UF = new UFViewModel
                            {
                                Id = item.MicrorRegiao.UF.Id,
                                Sigla = item.MicrorRegiao.UF.Sigla,
                                Nome = item.MicrorRegiao.UF.Nome
                            }

                        },
                        RegiaoImediata = new RegiaoImediataViewModel
                        {
                            Id = item.RegiaoImediata.Id,
                            Nome = item.RegiaoImediata.Nome,
                            UF = new UFViewModel
                            {
                                Id = item.RegiaoImediata.UF.Id,
                                Sigla = item.RegiaoImediata.UF.Sigla,
                                Nome = item.RegiaoImediata.UF.Nome
                            }

                        }
                        
                    });
                }

            }
                return cidades;                    
        }
    }
}

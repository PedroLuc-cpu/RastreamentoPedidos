using RastreamentoPedido.Core.ViewModels.Cidade;
using RastreamentoPedido.Core.ViewModels.Cidade.ViaCep;

namespace RastreamentoPedido.Core.Service
{
    public interface ICidadeService
    {
        Task<IList<CidadeViewModel>> BuscarCidadePorEstado(string sigla);
        Task<IList<UFViewModel>> BuscarTodosEstados();
        Task<ViaCepViewModel> BuscarEnderecoViaCep(string cep);

    }
}

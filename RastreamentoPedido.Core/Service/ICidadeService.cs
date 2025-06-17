using RastreamentoPedido.Core.ViewModels.Cidade;

namespace RastreamentoPedido.Core.Service
{
    public interface ICidadeService
    {
        Task<IList<CidadeViewModel>> BuscarCidadePorEstado(string sigla);
    }
}


namespace RastreamentoPedido.Core.Model.Lista
{
    /// <summary>
    /// Representa uma lista paginada.
    /// </summary>
    class ListaPaginada<T>
    {
        public int PaginaAtual { get; set; }
        public int TotalPaginas { get; set; }

        public List<T> Itens { get; set; }


        public ListaPaginada(List<T> itens, int count, int pageIndex, int pageSize)
        {
            PaginaAtual = pageIndex;
            TotalPaginas = (int)Math.Ceiling(count / (double)pageSize);
            Itens = new List<T>();
            Itens.Clear();
            Itens.AddRange(itens);
        }

        public ListaPaginada()
        {
            PaginaAtual = 1;
            TotalPaginas = 15;
            Itens = new List<T>();
        }

    }
}


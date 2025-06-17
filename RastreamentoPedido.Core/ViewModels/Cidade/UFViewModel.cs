namespace RastreamentoPedido.Core.ViewModels.Cidade
{
    public class UFViewModel
    {
        public int Id { get; set; }
        public string Sigla { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public RegiaoViewModel Regiao { get; set; } = new RegiaoViewModel();

    }
}

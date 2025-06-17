namespace RastreamentoPedido.Core.ViewModels.Cidade
{
    public class MicrorRegiaoViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public UFViewModel UF { get; set; } = new UFViewModel();
    }
}

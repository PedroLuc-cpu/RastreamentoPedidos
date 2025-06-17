namespace RastreamentoPedido.Core.ViewModels.Cidade
{
    public class CidadeViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public MicrorRegiaoViewModel MicrorRegiao { get; set; } = new MicrorRegiaoViewModel();
        public RegiaoImediataViewModel RegiaoImediata { get; set; } = new RegiaoImediataViewModel();

    }
}

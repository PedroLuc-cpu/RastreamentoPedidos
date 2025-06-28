namespace RastreamentoPedido.Core.Requests.Cliente
{
    public class TelefoneRequest
    {
        public string Numero { get; set; } = string.Empty;
        public bool Padrao { get; set; } = false;
        public string Prefixo { get; set; } = string.Empty;
        public int idCliente { get; set; }
    }
}

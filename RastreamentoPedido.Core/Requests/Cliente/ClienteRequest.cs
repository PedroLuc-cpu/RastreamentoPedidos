namespace RastreamentoPedido.Core.Requests.Cliente
{
    public class ClienteRequest
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Documento { get; set; } = string.Empty;
        public EstadoCivilRequest EstadoCivil { get; set; } = new EstadoCivilRequest();
        public bool Ativo { get; set; } = true;
        public bool Sexo { get; set; } = true; // true = Masculino, false = Feminino
        public DateTime DataNascimento { get; set; } = DateTime.MinValue;
    }
}

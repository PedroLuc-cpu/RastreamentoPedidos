
using RastreamentoPedido.Core.Model.Clientes;

namespace RastreamentoPedido.Core.Requests
{
    public class ClienteRequest
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public IList<Telefone> Telefone { get; set; } = new List<Telefone>();
        public IList<Endereco> Endereco { get; set; } = new List<Endereco>();
        
        public ClienteRequest(string nome, string email, IList<Telefone> telefone, IList<Endereco> endereco)
        {
            Nome = nome;
            Email = email;
            Telefone = telefone;
            Endereco = endereco;
        }
    }
}

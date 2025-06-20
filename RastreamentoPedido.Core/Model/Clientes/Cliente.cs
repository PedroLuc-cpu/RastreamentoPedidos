using RastreamentoPedido.Core.DomainObjects;
using RastreamentoPedido.Core.Model.Encomenda;

namespace RastreamentoPedido.Core.Model.Clientes
{
    public class Cliente : IAggregateRoot
    {
        public int IdCliente { get; set; }
        public int? IdEncomenda { get; set; }
        public int EstadoCivilId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public EstadoCivil EstadoCivil { get; set; } = new EstadoCivil();
        public bool Ativo { get; set; } = true;
        public bool Sexo { get; set; } = true; // true = Masculino, false = Feminino
        public DateTime DataNascimento { get; set; }
        /// <summary>
        /// CPF e CNPJ do cliente
        /// </summary>
        public string Documento {  get; set; } = string.Empty;
        public IList<Endereco>? Enderecos { get; set; } = new List<Endereco>();
        public IList<Telefone>? Telefones { get; set; } = new List<Telefone>();
        public IList<Encomendas>? Encomendas {  get; set; } = new List<Encomendas>();
    }
}

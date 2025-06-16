
using RastreamentoPedido.Core.DomainObjects;

namespace RastreamentoPedido.Core.Model.Clientes
{
    public class EstadoCivil : IAggregateRoot
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public string EstadoCivilDescricao { get; set; } = string.Empty;
        public EstadoCivil(int id, string estadoCivilDescricao)
        {
            Id = id;
            EstadoCivilDescricao = estadoCivilDescricao;
        }
        public EstadoCivil() { } // Construtor padrão para serialização e deserialização
    }
}

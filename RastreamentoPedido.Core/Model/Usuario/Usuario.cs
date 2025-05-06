using System.ComponentModel.DataAnnotations;
using RastreamentoPedido.Core.DomainObjects;

namespace RastreamentoPedido.Core.Model.Usuario
{
    public class Usuario : IAggregateRoot
    {
        [Key]
        public int idUsuario { get; set; }
        public string login { get; set; } = string.Empty;
        public bool ativo { get; set; }
        public string nome { get; set; } = string.Empty;
        public string senha { get; set; } = string.Empty;
        public string bio { get; set; } = string.Empty;
        
    }
}
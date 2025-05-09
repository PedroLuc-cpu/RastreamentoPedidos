using System.ComponentModel.DataAnnotations;
using RastreamentoPedido.Core.DomainObjects;

namespace RastreamentoPedido.Core.Model.Usuario
{
    public class Usuario : IAggregateRoot
    {
        [Key]
        public int idUsuario { get; set; }
        public string nomeUsuario { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public bool ativo { get; set; }
        public string senhaConfirmacao { get; set; } = string.Empty;
        public string senha { get; set; } = string.Empty;
        public string funcao { get; set; } = string.Empty;
        public string bio { get; set; } = string.Empty;
        
    }
}
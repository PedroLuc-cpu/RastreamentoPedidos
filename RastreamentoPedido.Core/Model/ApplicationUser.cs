using Microsoft.AspNetCore.Identity;

namespace RastreamentoPedidos.Model
{
    public class ApplicationUser : IdentityUser
    {
        public enum StatusUsuario { OffLine = 0, OnLine = 1, Desativado = 2 };

        private StatusUsuario statusUsuario;
        public string DescricaoStatus { get; private set; } = "";
        public string NomeCompleto { get; set; } = string.Empty;
        public StatusUsuario StatusUser
        {
            get { return statusUsuario; }
            set
            {
                statusUsuario = value;
                switch (value)
                {
                    case StatusUsuario.OnLine:
                        DescricaoStatus = "On-Line";
                        break;
                    case StatusUsuario.OffLine:
                        DescricaoStatus = "Off-line";
                        break;
                    case StatusUsuario.Desativado:
                        DescricaoStatus = "Desativado";
                        break;
                    default:
                        DescricaoStatus = "Off-line";
                        break;
                }
            }
        }
    }
}
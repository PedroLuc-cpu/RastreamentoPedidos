
namespace RastreamentoPedido.Core.Model.Notifications
{
    public class Notification
    {
        public Guid Id { get; set; } = Guid.Empty;
        public string Menssage { get; set; } = string.Empty;
        public string Operation { get; set; } = string.Empty;
    }
}

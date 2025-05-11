using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using RastreamentoPedido.Core.Model.Notifications;
using RastreamentoPedidos.Model;
using SignalRSwaggerGen.Attributes;
using static RastreamentoPedidos.Model.ApplicationUser;

namespace RastreamentoPedidos.API.Hubs
{
    public class NotificationHub : Hub
    {
        public readonly UserManager<ApplicationUser> _userManager;

        public NotificationHub(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task SendNotification(Notification notification)
        {
            await Clients.Others.SendAsync("ReceiveNotification", notification).ConfigureAwait(false);
        }

        [SignalRHidden]
        public override async Task OnConnectedAsync()
        {
            string id = Context.User!.Claims.FirstOrDefault(x => x.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"))!.Value;

            var usuario = _userManager.FindByEmailAsync(id).Result;
            if (usuario != null)
            {
                usuario.StatusUser = StatusUsuario.OnLine;

                var result = _userManager.UpdateAsync(usuario).Result;

                var roles = await _userManager.GetRolesAsync(usuario);
                if (roles != null)
                {
                    var role = roles.FirstOrDefault();
                    if (role != null)
                    {
                        await Groups.AddToGroupAsync(Context.ConnectionId, role);
                    }
                }
                UserHandler.ConnectedUsers.Add(Context.ConnectionId, id);
                IList<string>? connecionIds;
                if (UserHandler.UserSections.TryGetValue(usuario.Email!, out connecionIds))
                {
                    connecionIds.Add(Context.ConnectionId);
                    UserHandler.UserSections[usuario.Email!] = connecionIds;
                }
                else
                {
                    connecionIds = new List<string>();
                    connecionIds.Add(Context.ConnectionId);
                    UserHandler.UserSections.Add(usuario.Email!, connecionIds);
                }
            }
            await base.OnConnectedAsync();
        }

        [SignalRHidden]
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            string? id;
            UserHandler.ConnectedUsers.TryGetValue(Context.ConnectionId, out id);

            UserHandler.ConnectedUsers.Remove(Context.ConnectionId);

            int qnt = 0;
            foreach (var item in UserHandler.UserSections)
            {
                if (item.Value.Equals(id))
                {
                    qnt += 1;
                }
            }
            if (qnt == 0)
            {
                if (id == null)
                {
                    var usuario = await _userManager.FindByIdAsync(id!);
                    if (usuario != null)
                    {
                        usuario.StatusUser = StatusUsuario.OffLine;
                        var result = await _userManager.UpdateAsync(usuario);

                        var roles = await _userManager.GetRolesAsync(usuario);
                        if (roles != null)
                        {
                            var role = roles.FirstOrDefault();
                            if (role != null)
                            {
                                await Groups.RemoveFromGroupAsync(Context.ConnectionId, role);
                            }
                        }
                    }
                }
            }

            await base.OnDisconnectedAsync(exception);
        }

    }
    public static class UserHandler
    {
        public static IDictionary<string, string> ConnectedUsers = new Dictionary<string, string>();
        public static IDictionary<string, IList<string>> UserSections = new Dictionary<string, IList<string>>();
    }
}


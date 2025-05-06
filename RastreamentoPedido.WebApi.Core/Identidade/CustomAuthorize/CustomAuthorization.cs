
using Microsoft.AspNetCore.Http;

namespace RastreamentoPedido.WebApi.Core.Identidade.CustomAuthorize
{
    class CustomAuthorization
    {
        public static bool ValidarClaimsUsuario(HttpContext context, string claimName, string claimValue)
        {
            var claims = context.User.Claims;
            if (claims == null)
            {
                return false;
            }
            if (context.User.Identity != null)
            {
                return context.User.Identity.IsAuthenticated &&
                   context.User.Claims.Any(c => c.Type == claimName && c.Value.Contains(claimValue));
            }
            return false;
        }
    }
}

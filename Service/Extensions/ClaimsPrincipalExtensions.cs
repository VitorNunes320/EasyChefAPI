
using System.Security.Claims;

namespace Service.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid? GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                return null;
            }
            var claim = principal.FindFirst(ClaimTypes.NameIdentifier);
            return claim != null ? Guid.Parse(claim.Value) : null;
        }
    }
}

using System.Security.Claims;

namespace CityInfo.API.Helpers
{
    public static class PrincipalExtensions
    {
        public static string GetCurrentUserId(this ClaimsPrincipal claimsPrincipal)
        {
            string? userId = claimsPrincipal.FindFirstValue("user_id");
            return userId ?? throw new ArgumentException("Unable to retrieve user id from auth information");
        }
    }
}

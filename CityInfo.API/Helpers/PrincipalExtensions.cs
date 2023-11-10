using System.Security.Claims;

namespace CityInfo.API.Helpers
{
    public static class PrincipalExtensions
    {
        public static int GetCurrentUserId(this ClaimsPrincipal claimsPrincipal)
        {
            string? userId = claimsPrincipal.FindFirstValue("user_id");
            if (userId == null)
            {
                throw new ArgumentException("Unable to retrieve user id from auth information");
            }
            return Int32.Parse(userId);
        }

        public static int GetCurrentOrganizationId(this ClaimsPrincipal claimsPrincipal)
        {
            string? userId = claimsPrincipal.FindFirstValue("organization_id");
            if (userId == null)
            {
                throw new ArgumentException("Unable to retrieve organization id from auth information");
            }
            return Int32.Parse(userId);
        }
    }
}

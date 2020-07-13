using System.Linq;
using System.Security.Claims;

namespace Btc.Etherscan.Common.Core.Extensions
{
    /// <summary>
    /// Various user (<see cref="ClaimsPrincipal"/>) extensions
    /// </summary>
    public static class UserExtensions
    {
        /// <summary>
        /// Get user email from claims
        /// </summary>
        /// <param name="user"><see cref="ClaimsPrincipal"/> representing the logged in user</param>
        /// <returns>User email or null if not present in the claims</returns>
        public static string GetEmail(this ClaimsPrincipal user)
        {
            if (user == null || user.Claims == null)
            {
                return null;
            }

            return user.Claims.Where(e => e.Type == ClaimTypes.Email)?.Select(x => x.Value)?.FirstOrDefault();
        }

        /// <summary>
        /// Get user name from claims
        /// </summary>
        /// <param name="user"><see cref="ClaimsPrincipal"/> representing the logged in user</param>
        /// <returns>User name or null if not present in the claims</returns>
        public static string GetFirstName(this ClaimsPrincipal user)
        {
            if (user == null || user.Claims == null)
            {
                return null;
            }

            return user.Claims.Where(e => e.Type == ClaimTypes.GivenName)?.Select(x => x.Value)?.FirstOrDefault();
        }

        /// <summary>
        /// Get user surname from claims
        /// </summary>
        /// <param name="user"><see cref="ClaimsPrincipal"/> representing the logged in user</param>
        /// <returns>User surname or null if not present in the claims</returns>
        public static string GetSurname(this ClaimsPrincipal user)
        {
            if (user == null || user.Claims == null)
            {
                return null;
            }

            return user.Claims.Where(e => e.Type == ClaimTypes.Surname)?.Select(x => x.Value)?.FirstOrDefault();
        }
    }
}

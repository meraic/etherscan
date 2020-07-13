using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Btc.Etherscan.Common.Core.Extensions
{
    /// <summary>
    /// Various user (<see cref="IHostingEnvironment"/>) extensions
    /// </summary>
    public static class EnvironmentExtensions
    {
        private const string Dev = "DEV";
        private const string Uat = "UAT";
        private const string Prd = "PRD";

        /// <summary>
        /// Gets an environment-specific URL part for the current environment (e.g. 'UAT').
        /// Will return 'DEV' if environment has not been correctly specified
        /// </summary>
        /// <param name="environment"></param>
        /// <returns></returns>
        public static string GetEnvironmentUrlPart(this IWebHostEnvironment environment)
        {
            if (environment.IsUat())
            {
                return Uat;
            }

            // Production has no extension part
            if (environment.IsPrd())
            {
                return "";
            }

            // Return dev by default
            return Dev;
        }

        /// <summary>
        /// Returns <see cref="true"/> if the current environment is DEV (deployed on DEV server)
        /// </summary>
        /// <param name="environment"></param>
        /// <returns></returns>
        public static bool IsDev(this IWebHostEnvironment environment)
        {
            return environment.IsEnvironment(Dev);
        }

        /// <summary>
        /// Returns <see cref="true"/> if the current environment is UAT
        /// </summary>
        /// <param name="environment"></param>
        /// <returns></returns>
        public static bool IsUat(this IWebHostEnvironment environment)
        {
            return environment.IsEnvironment(Uat);
        }

        /// <summary>
        /// Returns <see cref="true"/> if the current environment is production
        /// </summary>
        /// <param name="environment"></param>
        /// <returns></returns>
        public static bool IsPrd(this IWebHostEnvironment environment)
        {
            return environment.IsEnvironment(Prd);
        }
    }
}

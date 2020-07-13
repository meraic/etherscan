using System.Security.Principal;

namespace Btc.Etherscan.Common.Core.Models
{
    /// <summary>
    /// User Identity Information
    /// </summary>
    public class UserIdentity : IIdentity
    {
        /// <summary>
        /// Authentication type used
        /// </summary>
        public string AuthenticationType { get; }

        /// <summary>
        /// Whether the user is currently authenticated
        /// </summary>
        public bool IsAuthenticated => true;

        /// <summary>
        /// User's full name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// User's email address
        /// </summary>
        public string Email { get; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="name">User's full name</param>
        public UserIdentity(string name)
            : this(name, name, "Basic")
        {
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="name">User's full name</param>
        /// <param name="email">User's email address</param>
        public UserIdentity(string name, string email)
            : this(name, email, "Basic")
        {
        }

        /// <summary>
        /// Ctor
        /// </summary>
        public UserIdentity(string name, string email, string authenticationType)
        {
            Name = name;
            Email = email;
            AuthenticationType = authenticationType;
        }
    }
}

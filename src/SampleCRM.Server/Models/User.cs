using OpenRiaServices.Server;
using OpenRiaServices.Server.Authentication;
#nullable enable

namespace SampleCRM.Web
{
    /// <summary>
    /// Class containing information about the authenticated user.
    /// </summary>
    public partial class User : IUser
    {
        [Key]
        [Required]
        public /*required */string Name { get; set; } = string.Empty;
        
        public /*required */IEnumerable<string> Roles { get; set; } = Enumerable.Empty<string>();

        /// <summary>
        /// Gets and sets the friendly name of the user.
        /// </summary>
        public string? FriendlyName { get; set; }

    }
}

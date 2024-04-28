using blazor_with_auth.Shared.Models;
using Microsoft.AspNetCore.Identity;

namespace blazor_with_auth.Data
{
    // Add profile data for application users by adding properties to the AppUser class
    //  TODO: copy structure and apply from :
    // Add properties to this class and update the server and client AuthenticationStateProviders
    // to expose more information about the authenticated user to the client.
    public class AppUser : IdentityUser
    {
        public ICollection<Beer> Beers { get; set; }
        public DateTime? PasswordChangedDate { get; set; }
        public string? NickName { get; set; }

    }

}

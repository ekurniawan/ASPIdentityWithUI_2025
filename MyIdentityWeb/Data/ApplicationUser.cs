using Microsoft.AspNetCore.Identity;

namespace MyIdentityWeb.Data
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public string? FirstName { get; set; }

        [PersonalData]
        public string? LastName { get; set; }

        [PersonalData]
        public DateTime CareerStarted { get; set; }
    }
}

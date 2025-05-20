using Microsoft.AspNetCore.Identity;

namespace MyIdentityWeb.Data
{
    public class ApplicationUser : IdentityUser
    {

        public string? FirstName { get; set; }


        public string? LastName { get; set; }


        public DateTime CareerStarted { get; set; }
    }
}

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace MyIdentityWeb.Areas.Identity
{
    public class ClaimsTransformer : IClaimsTransformation
    {
        private readonly IUserStore<IdentityUser> _UserStore;
        public ClaimsTransformer(IUserStore<IdentityUser> userStore)
        {
            _UserStore = userStore;
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            var clonePrincipal = principal.Clone();
            if (clonePrincipal.Identity == null)
            {
                return clonePrincipal;
            }
            var identity = (ClaimsIdentity)clonePrincipal.Identity;

            var existingClaim = identity.Claims.FirstOrDefault(c => c.Type == "careerstarted");
            if (existingClaim != null)
                return clonePrincipal;

            var nameIdClaim = identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (nameIdClaim == null)
            {
                return clonePrincipal;
            }

            var user = await _UserStore.FindByIdAsync(nameIdClaim.Value, CancellationToken.None);
            if (user != null)
            {
                identity.AddClaim(new Claim("careerstarted", user.NormalizedUserName.ToString()));
            }

            return clonePrincipal;
        }
    }
}

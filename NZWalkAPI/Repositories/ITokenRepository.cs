using Microsoft.AspNetCore.Identity;

namespace NZWalkAPI.Repositories
{
    public interface ITokenRepository
    {
       string  createJWTToken(IdentityUser user, List<string> roles);
    }
}

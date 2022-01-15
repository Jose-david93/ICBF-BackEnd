using ICBF.Domain.Models;

namespace ICBF.Application.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}

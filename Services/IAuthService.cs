using AuthorizationModule.Models;

namespace AuthorizationModule.Services
{
    public interface IAuthService
    {
        public AuthResponse Login(User user);
    }
}

using AuthorizationModule.Models;

namespace AuthorizationModule.Repository
{
    public interface IDatabaseRepository
    {
        public User GetUser(User user);
    }
}

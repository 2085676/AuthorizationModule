using AuthorizationModule.Models;
using System;
using System.Linq;

namespace AuthorizationModule.Repository
{
    public class DatabaseRepository : IDatabaseRepository
    {
        private readonly DBContext _context;

        public DatabaseRepository() { }

        public DatabaseRepository(DBContext context)
        {
            _context = context;
        }

        public User GetUser(User user)
        {
            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
                throw new Exception("Please enter email or password");
            User u = _context.Users.SingleOrDefault(i => i.Email == user.Email && i.Password == user.Password);
            return u;
        }
    }
}

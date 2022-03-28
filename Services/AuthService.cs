using AuthorizationModule.Models;
using AuthorizationModule.Repository;
using System;

namespace AuthorizationModule.Services
{
    public class AuthService : IAuthService
    {
        private readonly IDatabaseRepository _databaseRepository;
        private readonly ITokenService _tokenService;

        public AuthService(ITokenService tokenService, IDatabaseRepository databaseRepository)
        {
            _databaseRepository = databaseRepository;
            _tokenService = tokenService;
        }

        public AuthResponse Login(User user)
        {
            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
                throw new Exception("Please enter email or password");
            User u = _databaseRepository.GetUser(user);
            if (u == null)
                throw new Exception("Can not find user with given credentials");
            string token = _tokenService.GenerateToken(u.Email);
            if (string.IsNullOrEmpty(token))
                throw new Exception("Failed to generate token");
            return new AuthResponse { 
                Token = token,
                Message = "User logged in"
            };
        }
    }
}

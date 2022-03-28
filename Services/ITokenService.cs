namespace AuthorizationModule.Services
{
    public interface ITokenService
    {
        public string GenerateToken(string userEmail);
    }
}

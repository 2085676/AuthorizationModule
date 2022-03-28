using System.Text.Json;

namespace AuthorizationModule.Models
{
    public class AuthResponse
    {
        public string Token { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}

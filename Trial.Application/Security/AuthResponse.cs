using Trial.Application.DTO;

namespace Trial.Application.Security
{
    public class AuthResponse
    {
        public string Token { get; set; }
        public UserDTO User { get; set; }
    }
}

using backend.Models;

namespace backend.Dtos.Login
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }


        public AuthenticateResponse(UserLogin user, string token)
        {
            Id = user.Id;
            Username = user.Username;
            Token = token;
        }
    }
}
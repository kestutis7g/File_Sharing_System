using Forum.API.Models;

namespace Forum.API.Auth.Model;

[Serializable]
public class JWTResponse
{
    public string Token { get; set; }
    public Guid Id { get; set; }
    public string Role { get; set; }
}

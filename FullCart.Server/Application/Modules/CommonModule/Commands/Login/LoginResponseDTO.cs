namespace FullCart.Server.Application.Modules.CommonModule.Commands.Login;

public class LoginResponseDTO
{
    public string Username { get; set; }
    public int ExpiresIn { get; set; }
    public string AccessToken { get; set; }
    public string Role { get; set; }
}

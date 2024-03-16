namespace FullCart.Server.Shared.BaseModels;

public class JWTSettings
{
    public string SecretHashKey { get; set; }
    public int DurationInMillisecond { get; set; }
}

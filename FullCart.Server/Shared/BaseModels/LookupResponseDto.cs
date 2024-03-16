namespace FullCart.Server.Shared.BaseModels;

public class LookupResponseDto<T>
{
    public T Id { get; set; }
    public string Name { get; set; }
}

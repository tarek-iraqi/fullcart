namespace FullCart.Server.Shared.BaseModels;

public abstract class Entity<T> : IBaseEntity
{
    public T Id { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
}

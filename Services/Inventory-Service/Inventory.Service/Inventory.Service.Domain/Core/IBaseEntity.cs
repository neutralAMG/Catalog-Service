namespace Inventory.Service.Domain.Core;

public interface IBaseEntity<TKey>
{
    public TKey Id { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime  UpdatedAt { get; set; }
}


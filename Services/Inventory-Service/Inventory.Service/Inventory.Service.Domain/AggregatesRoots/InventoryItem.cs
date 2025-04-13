using Inventory.Service.Domain.Core;

namespace Inventory.Service.Domain.AgregatesRoots;

public class InventoryItem  : IBaseEntity<int>
{
    public int Id { get; set; }
    
    public int InventoryId { get; set; }
    
    public int ItemId { get; set; }
    
    public int CurrentQuantity { get; set; }
    
    public int MaxQuantity { get; set; } = 999;
    
    public Inventory Inventory { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime  UpdatedAt { get; set; }
}
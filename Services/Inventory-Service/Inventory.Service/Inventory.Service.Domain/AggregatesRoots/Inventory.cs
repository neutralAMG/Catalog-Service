using System.Collections.ObjectModel;
using Inventory.Service.Domain.Core;

namespace Inventory.Service.Domain.AgregatesRoots;

public class Inventory : IBaseEntity<int>
{
    public int Id { get; set; }
    
    public Guid UserId { get; set; }
    public int MaxSpace { get; set; }
    public int CurrentSpace { get; set; }
    
    public ReadOnlyCollection<InventoryItem> Items { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime  UpdatedAt { get; set; }
}
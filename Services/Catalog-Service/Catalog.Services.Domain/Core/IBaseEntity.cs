
namespace Catalog.Service.Domain.Core
{
    public interface IBaseEntity<TId>
    {
        public TId  Id { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset? DateUpdated { get; set; }
    }
}

using Inventofree.Shared.Core.Entities;

namespace Inventofree.Module.Item.Core.Entities
{
    public class Item : BaseEntity
    {
        public string Name { get; set; }
        public string Detail { get; set; }
        public long CreatedBy { get; set; }
        public long UpdatedBy { get; set; }
        public long? CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
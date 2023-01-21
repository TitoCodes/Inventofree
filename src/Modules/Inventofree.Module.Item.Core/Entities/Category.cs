using Inventofree.Shared.Core.Entities;

namespace Inventofree.Module.Item.Core.Entities
{
    public class Category: BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
    }
}
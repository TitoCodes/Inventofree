using Inventofree.Shared.Core.Entities;

namespace Inventofree.Module.Item.Core.Entities
{
    public class Item : BaseEntity
    {
        public string Name { get; set; }
        public string Detail { get; set; }
        public int CreatedBy { get; set; }
    }
}
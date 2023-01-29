using System;

namespace Inventofree.Module.Item.Core.Dto.Item
{
    public class ItemDto
    {        
        public long Id { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public long CreatedBy { get; set; }
        public long UpdatedBy { get; set; }
        public long? CategoryId { get; set; }
        public ItemCategoryDto Category { get; set; }
    }
}
using System;

namespace Inventofree.Module.Item.Core.Dto.Category
{
    public class CategoryDto
    {
        public long Id { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long CreatedBy { get; set; }
        public long UpdatedBy { get; set; }
    }
}
using System;

namespace Inventofree.Shared.Core.Entities
{
    public abstract class BaseEntity
    {
        public long Id { get; set; }
        
        public DateTimeOffset CreatedDate { get; set; }
        
        public DateTimeOffset? ModifiedDate { get; set; }
    }
}
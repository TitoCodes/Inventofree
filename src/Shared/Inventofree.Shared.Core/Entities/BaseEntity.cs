using System;

namespace Inventofree.Shared.Core.Entities
{
    public abstract class BaseEntity
    {
        public long Id { get; set; }
        
        public DateTime CreatedDate { get; set; }
        
        public DateTime? ModifiedDate { get; set; }
    }
}
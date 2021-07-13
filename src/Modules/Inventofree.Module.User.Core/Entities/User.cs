using Inventofree.Shared.Core.Entities;

namespace Inventofree.Module.User.Core.Entities
{
    public class User : BaseEntity
    {
        public string Email { get; set; }

        public string Firstname { get; set; }

        public string Middlename { get; set; }

        public string Lastname { get; set; }

        public long? ModifiedId { get; set; }
        
        public virtual User Modified { get; set; }
    }
}
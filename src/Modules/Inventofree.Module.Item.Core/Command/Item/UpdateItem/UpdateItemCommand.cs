using MediatR;

namespace Inventofree.Module.Item.Core.Command.Item.UpdateItem
{
    public class UpdateItemCommand : IRequest<bool>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public long UpdatedBy { get; set; }
    }
}
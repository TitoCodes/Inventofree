using MediatR;

namespace Inventofree.Module.Item.Core.Command.Item.UpdateItem
{
    public class UpdateItemCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public int UpdatedBy { get; set; }
    }
}
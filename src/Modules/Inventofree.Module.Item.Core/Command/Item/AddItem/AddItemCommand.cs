using MediatR;

namespace Inventofree.Module.Item.Core.Command.Item.AddItem
{
    public class AddItemCommand : IRequest<long>
    {
        public string Name { get; set; }
        public string Detail { get; set; }
        public long CreatedBy { get; set; }
        public long? CategoryId { get; set; }
    }
}
using MediatR;

namespace Inventofree.Module.Item.Core.Command.Item.DeleteItem
{
    public class DeleteItemCommand: IRequest<Unit>
    {
        public long Id { get; init; }
        public int UserId { get; set; }
    }
}
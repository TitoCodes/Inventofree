using MediatR;

namespace Inventofree.Module.Item.Core.Command.Item.SetItemCategory
{
    public class SetItemCategoryCommand: IRequest<Unit>
    {
        public long ItemId { get; set; }
        public long CategoryId { get; set; }
        public long UserId { get; set; }
    }
}
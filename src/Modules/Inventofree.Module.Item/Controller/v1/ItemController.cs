using System.Threading;
using System.Threading.Tasks;
using Inventofree.Module.Item.Core.Command.Item.AddItem;
using Inventofree.Module.Item.Core.Command.Item.DeductItem;
using Inventofree.Module.Item.Core.Command.Item.DeleteItem;
using Inventofree.Module.Item.Core.Command.Item.SetItemCategory;
using Inventofree.Module.Item.Core.Command.Item.UpdateItem;
using Inventofree.Module.Item.Core.Queries.Item.GetAllItems;
using Inventofree.Module.Item.Core.Queries.Item.GetItemById;
using Inventofree.Module.Item.Core.Queries.Item.GetItemsByName;
using Inventofree.Module.Item.Core.Queries.Item.GetItemsCount;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inventofree.Module.Item.Controller.v1
{
    [ApiController]
    [Route("/api/items")]
    public class ItemController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ItemController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllItemsAsync(CancellationToken cancellationToken)
        {
            var item = await _mediator.Send(new GetAllItemsQuery(), cancellationToken);
            return Ok(item);
        }
        
        [HttpGet("count")]
        public async Task<IActionResult> GetTotalItemsCountAsync(CancellationToken cancellationToken)
        {
            var itemCount = await _mediator.Send(new GetItemsCountQuery(), cancellationToken);
            return Ok(itemCount);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetItemByIdAsync(int id, CancellationToken cancellationToken)
        {
            var item = await _mediator.Send(new GetItemByIdQuery() { Id = id }, cancellationToken);
            return Ok(item);
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetItemsByNameAsync(string name, CancellationToken cancellationToken)
        {
            var item = await _mediator.Send(new GetItemsByNameQuery() { Name = name }, cancellationToken);
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> AddItemAsync(AddItemCommand command, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(command, cancellationToken));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateItemAsync(UpdateItemCommand command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }

        [HttpPut("set-category")]
        public async Task<IActionResult> SetItemCategoryAsync(SetItemCategoryCommand command,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteItemAsync(int id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteItemCommand() { Id = id }, cancellationToken);
            return NoContent();
        }
        
        [HttpPut("deduct")]
        public async Task<IActionResult> DeductItemAsync(DeductItemCommand command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }
    }
}
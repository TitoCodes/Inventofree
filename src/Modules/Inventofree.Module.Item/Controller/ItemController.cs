using System.Threading.Tasks;
using Inventofree.Module.Item.Core.Command.Item;
using Inventofree.Module.Item.Core.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inventofree.Module.Item.Controller
{
    [ApiController]
    [Route("/api/item/[controller]")]
    internal class ItemController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ItemController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var item = await _mediator.Send(new GetAllItemsQuery());
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterItemAsync(RegisterItemCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
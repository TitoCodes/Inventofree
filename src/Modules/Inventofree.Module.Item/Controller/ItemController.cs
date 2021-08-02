using System;
using System.Threading;
using System.Threading.Tasks;
using Inventofree.Module.Item.Core.Command.Item;
using Inventofree.Module.Item.Core.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inventofree.Module.Item.Controller
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ItemController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            try
            {
                var item = await _mediator.Send(new GetAllItemsQuery(), cancellationToken);

                return Ok(item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> RegisterItemAsync(RegisterItemCommand command,
            CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _mediator.Send(command, cancellationToken));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
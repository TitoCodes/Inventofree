using System;
using System.Threading;
using System.Threading.Tasks;
using Inventofree.Module.Item.Core.Command.Item.AddItem;
using Inventofree.Module.Item.Core.Command.Item.DeleteItem;
using Inventofree.Module.Item.Core.Command.Item.SetItemCategory;
using Inventofree.Module.Item.Core.Command.Item.UpdateItem;
using Inventofree.Module.Item.Core.Queries.Item.GetAllItems;
using Inventofree.Module.Item.Core.Queries.Item.GetItemById;
using Inventofree.Module.Item.Core.Queries.Item.GetItemsByName;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Inventofree.Module.Item.Controller.v1
{
    [ApiController]
    [Route("/api/items")]
    public class ItemController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ItemController> _logger;

        public ItemController(IMediator mediator, ILogger<ItemController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllItemsAsync(CancellationToken cancellationToken)
        {
            try
            {
                var item = await _mediator.Send(new GetAllItemsQuery(), cancellationToken);
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.InnerException, ex);
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetItemByIdAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                var item = await _mediator.Send(new GetItemByIdQuery(){ Id = id}, cancellationToken);
                return Ok(item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetItemsByNameAsync(string name, CancellationToken cancellationToken)
        {
            try
            {
                var item = await _mediator.Send(new GetItemsByNameQuery(){ Name = name}, cancellationToken);
                return Ok(item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> AddItemAsync(AddItemCommand command, CancellationToken cancellationToken)
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
        
        [HttpPut]
        public async Task<IActionResult> UpdateItemAsync(UpdateItemCommand command, CancellationToken cancellationToken)
        {
            try
            {
                await _mediator.Send(command, cancellationToken);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPut("set-category")]
        public async Task<IActionResult> SetItemCategoryAsync(SetItemCategoryCommand command, CancellationToken cancellationToken)
        {
            try
            {
                await _mediator.Send(command, cancellationToken);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteItemAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                await _mediator.Send(new DeleteItemCommand() { Id = id }, cancellationToken);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
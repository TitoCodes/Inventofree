using System.Threading;
using System.Threading.Tasks;
using Inventofree.Module.Item.Core.Command.Category.AddCategory;
using Inventofree.Module.Item.Core.Command.Category.DeleteCategory;
using Inventofree.Module.Item.Core.Command.Category.UpdateCategory;
using Inventofree.Module.Item.Core.Queries.Category.GetAllCategories;
using Inventofree.Module.Item.Core.Queries.Category.GetCategoryById;
using Inventofree.Module.Item.Core.Queries.Category.GetCategoryByName;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inventofree.Module.Item.Controller.v1
{
    [ApiController]
    [Route("/api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategoriesAsync(CancellationToken cancellationToken)
        {
            var item = await _mediator.Send(new GetAllCategoriesQuery(), cancellationToken);
            return Ok(item);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetCategoryByName(string name, CancellationToken cancellationToken)
        {
            var item = await _mediator.Send(new GetCategoryByNameQuery() { Name = name }, cancellationToken);
            return Ok(item);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id, CancellationToken cancellationToken)
        {
            var item = await _mediator.Send(new GetCategoryByIdQuery() { Id = id }, cancellationToken);
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategoryAsync(AddCategoryCommand command,
            CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(command, cancellationToken));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategoryAsync(UpdateCategoryCommand command,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCategoryAsync(int id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteCategoryCommand() { Id = id }, cancellationToken);
            return NoContent();
        }
    }
}
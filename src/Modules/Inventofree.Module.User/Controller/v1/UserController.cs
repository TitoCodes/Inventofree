using System.Threading;
using System.Threading.Tasks;
using Inventofree.Module.User.Core.Command.User.CreateUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inventofree.Module.User.Controller.v1
{
    [ApiController]
    [Route("/api/users")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> InsertUser(CreateUserCommand command, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(command, cancellationToken));
        }
    }
}
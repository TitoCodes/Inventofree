using System;
using System.Threading;
using System.Threading.Tasks;
using Inventofree.Module.User.Core.Command.User.InsertUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inventofree.Module.User.Controller
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost]
        public async Task<IActionResult> InsertUser(InsertUserCommand command, CancellationToken cancellationToken)
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
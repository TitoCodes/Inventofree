using Inventofree.Module.Transaction.Core.Command.Transaction.AddTransaction;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inventofree.Module.Transaction.Controller.v1;
[ApiController]
[Route("/api/transactions")]
public class TransactionController : ControllerBase
{
    private readonly IMediator _mediator;

    public TransactionController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<IActionResult> AddTransactionAsync(AddTransactionCommand command,
        CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(command, cancellationToken));
    }
}
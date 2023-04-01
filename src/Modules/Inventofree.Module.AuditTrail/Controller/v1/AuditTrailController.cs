using Inventofree.Module.AuditTrail.Core.Command.AuditTrail.AddAuditTrail;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inventofree.Module.AuditTrail.Controller.v1;

[ApiController]
[Route("/api/audit-trails")]
public class AuditTrailController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuditTrailController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<IActionResult> AddAuditTrailAsync(AddAuditTrailCommand command, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(command, cancellationToken));
    }
}
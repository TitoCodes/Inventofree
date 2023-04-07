using Inventofree.Module.AuditTrail.Core.Command.AuditTrail.AddAuditTrail;
using Inventofree.Module.AuditTrail.Core.Command.AuditTrail.DeleteAuditTrail;
using Inventofree.Module.AuditTrail.Core.Command.AuditTrail.UpdateAuditTrail;
using Inventofree.Module.AuditTrail.Core.Queries.AuditTrail.GetAllAuditTrail;
using Inventofree.Module.AuditTrail.Core.Queries.AuditTrail.GetAuditTrailById;
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
    
    [HttpGet]
    public async Task<IActionResult> GetAllAuditTrailListAsync(CancellationToken cancellationToken)
    {
        var item = await _mediator.Send(new GetAllAuditTrailQuery(), cancellationToken);
        return Ok(item);
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetAuditTrailByIdAsync(int id, CancellationToken cancellationToken)
    {
        var item = await _mediator.Send(new GetAuditTrailByIdQuery() { Id = id }, cancellationToken);
        return Ok(item);
    }
    
    [HttpPost]
    public async Task<IActionResult> AddAuditTrailAsync(AddAuditTrailCommand command, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(command, cancellationToken));
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateAuditTrailAsync(UpdateAuditTrailCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAuditTrailAsync(int id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteAuditTrailCommand() { Id = id }, cancellationToken);
        return NoContent();
    }
}
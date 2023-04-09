namespace Inventofree.Module.AuditTrail.Core.Dto.AuditTrail;

public class AuditTrailDto
{
    public long Id { get; init; }
    public string? Action { get; set; }
    public string? Details { get; set; }
    public long CreatedBy { get; set; }
}
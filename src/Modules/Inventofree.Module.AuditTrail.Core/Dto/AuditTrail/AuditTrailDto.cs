namespace Inventofree.Module.AuditTrail.Core.Dto.AuditTrail;

public class AuditTrailDto
{
    public int Id { get; set; }
    public string? Action { get; set; }
    public string? Details { get; set; }
    public long CreatedBy { get; set; }
}
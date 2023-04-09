using Inventofree.Module.AuditTrail.Core.Command.AuditTrail.AddAuditTrail;
using Inventofree.Module.AuditTrail.Core.Dto.AuditTrail;

namespace Inventofree.Module.AuditTrail.Core.Profile;

public class MappingProfile : AutoMapper.Profile
{
    public MappingProfile()
    {
        AuditTrailMappingProfile();
    }
    
    private void AuditTrailMappingProfile()
    {
        CreateMap<AddAuditTrailCommand, Entities.AuditTrail>()
            .ForMember(
                dest => dest.CreatedDate,
                opt => opt.MapFrom(src => DateTimeOffset.UtcNow)
            );
        CreateMap<Entities.AuditTrail, AuditTrailDto>();
    }
}
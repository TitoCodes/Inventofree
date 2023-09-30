using Inventofree.Module.Transaction.Core.Command.Transaction.AddTransaction;
using Inventofree.Module.Transaction.Core.Dto;

namespace Inventofree.Module.Transaction.Core.Profile;

public class MappingProfile: AutoMapper.Profile
{
    public MappingProfile()
    {
        TransactionMappingProfile();
    }

    private void TransactionMappingProfile()
    {
        CreateMap<AddTransactionCommand, Entities.Transaction>()
            .ForMember(
                dest => dest.Type,
                opt => opt.Ignore()
            )
            .ForMember(
                dest => dest.ModifiedDate,
                opt => opt.Ignore()
            )
            .ForMember(
                dest => dest.CreatedDate,
                opt => opt.MapFrom(src => DateTimeOffset.UtcNow)
            );
        CreateMap<Entities.Transaction, TransactionDto>();
    }
}
using System;
using Inventofree.Module.Item.Core.Command.Item;

namespace Inventofree.Module.Item.Core.Profile
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile() {
            CreateMap<AddItemCommand, Entities.Item>()
                .ForMember(
                    dest => dest.CreatedDate,
                    opt => opt.MapFrom(src => DateTimeOffset.UtcNow)
                    );
        }
    }
}
using System;
using Inventofree.Module.Item.Core.Command.Category.AddCategory;
using Inventofree.Module.Item.Core.Command.Item.AddItem;

namespace Inventofree.Module.Item.Core.Profile
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            ItemMappingProfile();
            CategoryMappingProfile();
        }

        private void ItemMappingProfile()
        {
            CreateMap<AddItemCommand, Entities.Item>()
                .ForMember(
                    dest => dest.CreatedDate,
                    opt => opt.MapFrom(src => DateTimeOffset.UtcNow)
                );
        }
        
        private void CategoryMappingProfile()
        {
            CreateMap<AddCategoryCommand, Entities.Category>()
                .ForMember(
                    dest => dest.CreatedDate,
                    opt => opt.MapFrom(src => DateTimeOffset.UtcNow)
                );
        }
    }
}
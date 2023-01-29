using System;
using Inventofree.Module.Item.Core.Command.Category.AddCategory;
using Inventofree.Module.Item.Core.Command.Item.AddItem;
using Inventofree.Module.Item.Core.Dto.Category;
using Inventofree.Module.Item.Core.Dto.Item;

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
            CreateMap<Entities.Item, ItemDto>();
        }
        
        private void CategoryMappingProfile()
        {
            CreateMap<AddCategoryCommand, Entities.Category>()
                .ForMember(
                    dest => dest.CreatedDate,
                    opt => opt.MapFrom(src => DateTimeOffset.UtcNow)
                );
            CreateMap<Entities.Category, CategoryDto>();
            CreateMap<Entities.Category, ItemCategoryDto>();
        }
    }
}
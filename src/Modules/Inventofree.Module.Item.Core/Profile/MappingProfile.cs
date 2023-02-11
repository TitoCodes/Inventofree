using System;
using Inventofree.Module.Item.Core.Command.Category.AddCategory;
using Inventofree.Module.Item.Core.Command.Item.AddItem;
using Inventofree.Module.Item.Core.Dto.Category;
using Inventofree.Module.Item.Core.Dto.Item;
using Inventofree.Module.Item.Core.Dto.Price;
using Inventofree.Module.Item.Core.Entities;

namespace Inventofree.Module.Item.Core.Profile
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            ItemMappingProfile();
            PriceMappingProfile();
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

        private void PriceMappingProfile()
        {
            CreateMap<PriceDto, Price>()
                .ForMember(
                    dest => dest.CreatedDate,
                    opt => opt.MapFrom(src => DateTimeOffset.UtcNow)
                )
                .ForMember(
                    dest => dest.Currency,
                    opt => opt.MapFrom(src => src.CurrencyType)
                );
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
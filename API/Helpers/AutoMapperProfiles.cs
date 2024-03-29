using AutoMapper;
using API.Models;
using API.Dtos;
using API.Dtos.FileDtos;
using API.Dtos.UserDtos;
using API.Dtos.OrderDtos;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<ItemTemplate, ItemTemplateForGetDto>();
            CreateMap<ItemTemplate, ItemTemplateForAddDto>();
            CreateMap<ItemTemplate, ItemTemplateForTableDto>();
            CreateMap<ItemTemplate, ItemTemplatePartForGetDto>();
            CreateMap<ItemTemplate, ItemTemplatePartOfForGetDto>();
            CreateMap<ItemTemplate, ItemTemplateForItemGetDto>();
            CreateMap<ItemTemplatePart, ItemTemplatePartDto>();
            CreateMap<ItemTemplatePart, ItemTemplatePartOfDto>();
            CreateMap<ItemPropertyName, ItemPropertyNameForAddDto>();
            CreateMap<ItemPropertyName, ItemPropertyNameForGetDto>();
            CreateMap<ItemPropertyDescription, ItemPropertyDescriptionForGetDto>();
            CreateMap<TemplatePropertyRelation, ItemPropertyNameForGetDto>()
                .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Property.Name))
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.PropertyId));
            CreateMap<Item, ItemForTableGetDto>();
            CreateMap<TemplatePropertyRelation, ItemPropertyNameForGetDto>()
                .ForMember(x => x.Name, opt => opt.MapFrom(src => src.PropertyId));
            CreateMap<Item, UserForItemGetDto>();
            CreateMap<Order, OrderForTableDto>();
            CreateMap<ItemItemRelation, ItemItemRelationPartOfForGet>();
            CreateMap<ItemItemRelation, ItemItemRelationForGet>()
                .ForMember(x => x.Template, opt => opt.MapFrom(src => src.Part.Template));
            CreateMap<User, UserForGetDto>()
                .ForMember(x => x.RoleCategories, opt => opt.MapFrom(src => src.UserRoleCategoryRelations));
            CreateMap<UserRoleCategoryRelation, RoleCategory>()
                .ForMember(x => x.Name, opt => opt.MapFrom(src => src.RoleCategory.Name))
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.RoleCategory.Id));
            CreateMap<User, UserForRegisterDto>();
            CreateMap<UserForRegisterDto, User>();
            CreateMap<TemplateFileName, FileForTableGetDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember( x => x.FileName, opt => opt.MapFrom(src => src.FileName));
            CreateMap<TemplateFileName, FileNameForGetDto>()
                .ForMember(x => x.FileDataId, opt => opt.MapFrom(src => src.FileData.Id));
            CreateMap<ItemTemplateCategory, TemplateCategoryForAddDto>();
            CreateMap<ItemTemplateCategory, TemplateCategoryForGetDto>();
            CreateMap<UnitType, UnitTypeForAddDto>();
            CreateMap<UnitType, UnitTypeForGetDto>();
            CreateMap<UserRole, UserRoleDto>();
            CreateMap<UserRoleDto,UserRole>();
            CreateMap<Order, OrderForGetDto>();
            CreateMap<OrderFileName, FileForTableGetDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember( x => x.FileName, opt => opt.MapFrom(src => src.FileName));            
        }
    }
}
using Accountant.DAL.Entities;
using AutoMapper;
using System.Linq;

namespace Accountant.API.AutoMapper
{
    public class AccountantProfile : Profile
    {
        public AccountantProfile()
        {
            CreateMap<Category, DTOs.Category>().ReverseMap();
            CreateMap<Expense, DTOs.Expense>().ReverseMap();
            CreateMap<Group, DTOs.Group>()
                .ForMember(dto => dto.Users, opt => opt.Ignore())
                .AfterMap((g, dto, ctx) =>
                    dto.Users = g.UserGroups.Select(ug =>
                        ctx.Mapper.Map<DTOs.User>(ug.User)).ToList())
                .ReverseMap();
            CreateMap<Report, DTOs.Report>().ReverseMap();
            CreateMap<ShoppingList, DTOs.ShoppingList>()
                .ForMember(dto => dto.Items, opt =>
                    opt.MapFrom(sl => sl.ShoppingListItems))
                .ReverseMap();
            CreateMap<ShoppingListItem, DTOs.ShoppingListItem>().ReverseMap();
            CreateMap<User, DTOs.User>()
                .ForMember(dto => dto.Groups, opt => opt.Ignore())
                .AfterMap((u, dto, ctx) =>
                dto.Groups = u.UserGroups.Select(ug =>
                    ctx.Mapper.Map<DTOs.Group>(ug.Group)).ToList())
                .ReverseMap();

            CreateMap<User, DTOs.UserModels.UpdateModel>().ReverseMap();
        }
    }
}

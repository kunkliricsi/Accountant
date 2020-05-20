using Accountant.DAL.Entities;
using AutoMapper;
using System.Collections.Generic;
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
                    {
                        return new DTOs.User
                        {
                            Id = ug.User.Id,
                            Name = ug.User.Name,
                        };
                    }).ToList())
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
                {
                    return new DTOs.Group
                    {
                        Id = ug.Group.Id,
                        Name = ug.Group.Name,
                        Reports = ctx.Mapper.Map<ICollection<DTOs.Report>>(ug.Group.Reports),
                        ShoppingLists = ctx.Mapper.Map<ICollection<DTOs.ShoppingList>>(ug.Group.ShoppingLists),
                    };
                }).ToList())
                .ReverseMap();

            CreateMap<User, DTOs.UserModels.UpdateModel>().ReverseMap();
        }
    }
}

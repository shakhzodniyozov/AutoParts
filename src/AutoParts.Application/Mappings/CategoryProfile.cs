using AutoMapper;
using AutoParts.Application.Categories.Commands.Create;
using AutoParts.Application.Categories.Commands.Update;
using AutoParts.Application.Categories.Queries;
using AutoParts.Domain.Entities;

namespace AutoParts.Application.Mappings;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryDto>();
        CreateMap<Category, Category>().ForMember(d => d.Image, opt => opt.Ignore());
        CreateMap<CreateCategoryCommand, Category>().ForMember(dest => dest.Image, opt => opt.Ignore());
        CreateMap<UpdateCategoryCommand, Category>();
    }
}
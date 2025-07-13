using AutoMapper;
using NetPCTest.Backend.Dtos;
using NetPCTest.Backend.Models;

namespace NetPCTest.Backend.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Contact, ContactBriefDto>();
        CreateMap<Contact, ContactDto>();
        CreateMap<ContactCreationDto, Contact>();
        CreateMap<ContactBrief, ContactBriefDto>();
        CreateMap<Category, CategoryDto>();
        CreateMap<SubCategory, SubCategoryDto>();
    }
}
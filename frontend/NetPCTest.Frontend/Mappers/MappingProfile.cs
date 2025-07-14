using AutoMapper;
using NetPCTest.Frontend.Dtos;
using NetPCTest.Frontend.Models;

namespace NetPCTest.Frontend.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ContactEditFormModel, ContactCreationDto>();
        CreateMap<ContactEditFormModel, ContactUpdateDto>();
        CreateMap<ContactDto, ContactEditFormModel>();
    }
}
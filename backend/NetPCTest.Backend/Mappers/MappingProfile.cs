using AutoMapper;
using NetPCTest.Backend.Dtos;
using NetPCTest.Backend.Models;

namespace NetPCTest.Backend.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Contact, ContactBriefDto>();
        CreateMap<Contact, ContactDetailsDto>();
        CreateMap<ContactCreationDto, Contact>();
    }
}
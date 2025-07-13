using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Moq;
using NetPCTest.Backend.Dtos;
using NetPCTest.Backend.Models;
using NetPCTest.Backend.Repositories;
using NetPCTest.Backend.Services;
using NetPCTest.Backend.Validators;

namespace NetPCTest.Backend.Tests;

[TestFixture]
public class ContactsServiceTests
{
    private Mock<IRepository> _repositoryMock = null!;
    private IPasswordHasher<Contact> _passwordHasher = null!;
    private PasswordService _passwordService = null!;
    private IMapper _mapper = null!;
    private ICategoryValidator _validator = null!;
    private ContactsService _service = null!;
    
    [SetUp]
    public void Setup()
    {
        _repositoryMock = new Mock<IRepository>();
        _passwordHasher = new PasswordHasher<Contact>();
        _passwordService = new PasswordService(_passwordHasher);
        
        var mapperMock = new Mock<IMapper>();
        _mapper = mapperMock.Object;
        
        _validator = Mock.Of<ICategoryValidator>();
        _service = new ContactsService(_repositoryMock.Object, _passwordService, _mapper, _validator);
    }

    [Test]
    public async Task GetContacts_ReturnsMappedList()
    {
        // ContactsBrief list mock.
        var contacts = new List<ContactBrief>
        {
            new()
            {
                Id = 1,
                Name = "Alice",
                Surname = "Goog",
            },
            new() { 
                Id = 2,
                Name = "Bob",
                Surname = "Goog",
            },
            new() { 
                Id = 3,
                Name = "Charles",
                Surname = "Goog",
            },
            new() { 
                Id = 3,
                Name = "Daniel",
                Surname = "Goog",
            }
        };

        _repositoryMock
            .Setup(r => r.GetContacts(1, 2))
            .ReturnsAsync(contacts);

        // We expect these DTOs.
        var expectedDtos = new List<ContactBriefDto>
        {
            new()
            {
                Id = 2,
                Name = "Bob",
                Surname = "Goog",
            },
            new() { 
                Id = 3,
                Name = "Charles",
                Surname = "Goog",
            }
        };

        var mapperMock = Mock.Get(_mapper);
        mapperMock
            .Setup(m => m.Map<List<ContactBriefDto>>(contacts))
            .Returns(expectedDtos);

        // Test the method.
        var result = await _service.GetContacts(1, 2);

        // Assertions.
        Assert.That(result, Is.EqualTo(expectedDtos));
        _repositoryMock.Verify(r => r.GetContacts(1, 2), Times.Once);
        mapperMock.Verify(m => m.Map<List<ContactBriefDto>>(contacts), Times.Once);
    }
}
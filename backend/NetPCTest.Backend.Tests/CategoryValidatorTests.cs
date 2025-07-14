using Moq;
using NetPCTest.Backend.Models;
using NetPCTest.Backend.Repositories;
using NetPCTest.Backend.Validators;

namespace NetPCTest.Backend.Tests;

[TestFixture]
public class CategoryValidatorTests
{
    private ICategoryValidator _validator = null!;
    private Mock<IRepository> _repositoryMock = null!;

    public CategoryValidatorTests()
    {
        _repositoryMock = new Mock<IRepository>();
        _validator = new CategoryValidator(_repositoryMock.Object);
    }
    
    [Test]
    public async Task CategoryValidator_FollowsRules()
    {
        // Category 1 allows for no custom subcategories.
        var category1 = new Category()
        {
            Id = 1,
            Name = "test.category1",
            CustomSubcategoryRequired = false,
            SubCategories = new List<SubCategory>(),
            Members = new List<Contact>(),
        };
            
        // Category 2 requires custom subcategories.
        var category2 = new Category()
        {
            Id = 2,
            Name = "test.category2",
            CustomSubcategoryRequired = true,
            SubCategories = new List<SubCategory>(),
            Members = new List<Contact>(),
        };
            
        // Category 3 allows for no custom subcategories.
        var category3 = new Category()
        {
            Id = 3,
            Name = "test.category3",
            CustomSubcategoryRequired = false,
            SubCategories = new List<SubCategory>(),
            Members = new List<Contact>(),
        };
            
        // Category 4 allows for no custom subcategories, but it won't have any subcategories either.
        var category4 = new Category()
        {
            Id = 4,
            Name = "test.category4",
            CustomSubcategoryRequired = false,
            SubCategories = new List<SubCategory>(),
            Members = new List<Contact>(),
        };
        
        // SubCategory 1 and SubCategory 2 will belong to Category 1.
        var subCategory1 = new SubCategory()
        {
            Id = 1,
            Name = "test.subcategory1",
            CategoryId = 1,
            Category = category1,
            Members = new List<Contact>(),
        };
        
        var subCategory2 = new SubCategory()
        {
            Id = 2,
            Name = "test.subcategory2",
            CategoryId = 1,
            Category = category1,
            Members = new List<Contact>(),
        };
        
        // Whilst SubCategory 3 will belong to Category 3.
        var subCategory3 = new SubCategory()
        {
            Id = 3,
            Name = "test.subcategory3",
            CategoryId = 3,
            Category = category3,
            Members = new List<Contact>(),
        };
        
        category1.SubCategories = new List<SubCategory>(){subCategory1, subCategory2};
        category2.SubCategories = new List<SubCategory>(){};
        category3.SubCategories = new List<SubCategory>(){subCategory3};
        category4.SubCategories = new List<SubCategory>(){};
        
        // Contact 3 - Has a CustomSubCategory, but belongs to Category 1, which doesn't allow it.
        var contact3 = new Contact()
        {
            Name = "b",
            Surname = "c",
            Email = "c@d.pl",
            BirthDate = new DateTime(1990, 1, 1),
            Phone = "1234567890",
            CategoryId = 1,
            CustomSubCategory = "Lorem ipsum dolor sit amet",
            PasswordHash = "asdf",
            Category = category1,
        };
        
        // Contact 4 - Has a CustomSubCategory, and belongs to Category 2, which does allow it.
        var contact4 = new Contact()
        {
            Name = "b",
            Surname = "c",
            Email = "c@d.pl",
            BirthDate = new DateTime(1990, 1, 1),
            Phone = "1234567890",
            CategoryId = 2,
            CustomSubCategory = "Lorem ipsum dolor sit amet",
            PasswordHash = "asdf",
            Category = category2,
        };
        
        // Contact 5 - Has a SubCategoryId of 1, and belongs to Category 1, which does allow it.
        var contact5 = new Contact()
        {
            Name = "b",
            Surname = "c",
            Email = "c@d.pl",
            BirthDate = new DateTime(1990, 1, 1),
            Phone = "1234567890",
            CategoryId = 1,
            SubCategoryId = 1,
            PasswordHash = "asdf",
            Category = category1,
        };
        
        // Contact 6 - Has a SubCategoryId of 3, and belongs to Category 1, but SubCategory 3 does not
        // belong to Category 1.
        var contact6 = new Contact()
        {
            Name = "b",
            Surname = "c",
            Email = "c@d.pl",
            BirthDate = new DateTime(1990, 1, 1),
            Phone = "1234567890",
            CategoryId = 1,
            SubCategoryId = 3,
            PasswordHash = "asdf",
            Category = category1,
        };
        
        // Contact 7 - Belongs to Category 4, which does not allow custom subcategories, but does not have any fixed
        // ones either.
        var contact7 = new Contact()
        {
            Name = "b",
            Surname = "c",
            Email = "c@d.pl",
            BirthDate = new DateTime(1990, 1, 1),
            Phone = "1234567890",
            CategoryId = 4,
            PasswordHash = "asdf",
            Category = category4,
        };
        
        // Contact 8 - Belongs to Category 4, which does not allow custom subcategories, but the contact has a
        // CustomSubCategory.
        var contact8 = new Contact()
        {
            Name = "b",
            Surname = "c",
            Email = "c@d.pl",
            BirthDate = new DateTime(1990, 1, 1),
            Phone = "1234567890",
            CategoryId = 4,
            CustomSubCategory = "aaaa",
            PasswordHash = "asdf",
            Category = category4,
        };
        
        // Here we set up our mock.
        _repositoryMock
            .Setup(r => r.GetCategory(1, CancellationToken.None))
            .ReturnsAsync(category1);
        _repositoryMock
            .Setup(r => r.GetCategory(2, CancellationToken.None))
            .ReturnsAsync(category2);
        _repositoryMock
            .Setup(r => r.GetCategory(3, CancellationToken.None))
            .ReturnsAsync(category3);
        _repositoryMock
            .Setup(r => r.GetCategory(4, CancellationToken.None))
            .ReturnsAsync(category4);
        _repositoryMock
            .Setup(r => r.GetSubCategory(1, CancellationToken.None))
            .ReturnsAsync(subCategory1);
        _repositoryMock
            .Setup(r => r.GetSubCategory(2, CancellationToken.None))
            .ReturnsAsync(subCategory2);
        _repositoryMock
            .Setup(r => r.GetSubCategory(3, CancellationToken.None))
            .ReturnsAsync(subCategory3);
        
        // Test time!
        Assert.That((await _validator.Validate(contact3)).Success, Is.False);
        Assert.That((await _validator.Validate(contact4)).Success, Is.True);
        Assert.That((await _validator.Validate(contact5)).Success, Is.True);
        Assert.That((await _validator.Validate(contact6)).Success, Is.False);
        Assert.That((await _validator.Validate(contact7)).Success, Is.True);
        Assert.That((await _validator.Validate(contact8)).Success, Is.False);
    }
}
using NetPCTest.Backend.Models;

namespace NetPCTest.Backend.Repositories;

public interface IRepository
{
    Task<List<ContactBrief>> GetContacts(int startIndex, int count);
    Task<Contact?> GetContact(int id);
    Task<int?> CreateContact(Contact contact);
    Task<Category?> GetCategory(int id);
    Task<SubCategory?> GetSubCategory(int id);
}
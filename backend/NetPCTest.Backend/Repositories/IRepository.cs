using NetPCTest.Backend.Models;

namespace NetPCTest.Backend.Repositories;

public interface IRepository
{
    Task<List<ContactBrief>> GetContacts(int startIndex, int count);
    Task<Contact?> GetContact(int id);
    Task<Contact?> GetContactByEmail(string email);
    Task<int?> CreateContact(Contact contact);
    Task<Category?> GetCategory(int id);
    Task<SubCategory?> GetSubCategory(int id);
    Task<bool> UpdateContact(int contactId, Contact contact);
}
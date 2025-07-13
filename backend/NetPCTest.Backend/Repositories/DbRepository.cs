using Microsoft.EntityFrameworkCore;
using NetPCTest.Backend.Data;
using NetPCTest.Backend.Dtos;
using NetPCTest.Backend.Models;
using NetPCTest.Backend.Services;

namespace NetPCTest.Backend.Repositories;

public class DbRepository(AppDbContext context) : IRepository
{
    public async Task<List<ContactBrief>> GetContacts(int startIndex, int count)
    {
        // Here we first .Select to make sure EF doesn't create a SQL query with columns that we are not going to use
        // anyway. Then, we .Select to create the DTOs.
        var contacts = await context.Contacts
            .Select(c => new { c.Id, c.Name, c.Surname })
            .OrderBy(c => c.Id)
            .Skip(startIndex)
            .Take(count)
            .Select(c => new ContactBrief
            {
                Id = c.Id,
                Name = c.Name,
                Surname = c.Surname,
            })
            .ToListAsync();

        return contacts;
    }

    public async Task<Contact?> GetContactByEmail(string email)
        => await context.Contacts
            .Where(c => c.Email == email)
            .FirstOrDefaultAsync();

    public async Task<Contact?> GetContact(int id)
        => await context.Contacts
            .Where(c => c.Id == id)
            .FirstOrDefaultAsync();

    public async Task<int?> CreateContact(Contact contact)
    {
        try
        {
            var result = await context.Contacts.AddAsync(contact);
            await context.SaveChangesAsync();

            return result.Entity.Id;
        }
        catch (DbUpdateException)
        {
            return null;
        }
    }

    public async Task<Category?> GetCategory(int id)
        => await context.Categories
            .Include(c => c.SubCategories)
            .FirstOrDefaultAsync(c => c.Id == id);
    
    public async Task<SubCategory?> GetSubCategory(int id)
        => await context.SubCategories
            .FirstOrDefaultAsync(s => s.Id == id);

    public async Task<bool> UpdateContact(int id, Contact contact)
    {
        try
        {
            var baseContact = await context.Contacts.FindAsync(id);
            if (baseContact == null)
                return false;

            baseContact.Id = contact.Id;
            baseContact.Name = contact.Name;
            baseContact.Surname = contact.Surname;
            baseContact.Email = contact.Email;
            baseContact.PasswordHash = contact.PasswordHash;
            baseContact.Phone = contact.Phone;
            baseContact.BirthDate = contact.BirthDate;
            baseContact.CategoryId = contact.CategoryId;
            baseContact.CustomSubCategory = contact.CustomSubCategory;

            await context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return false;
        }

        return true;
    }
}
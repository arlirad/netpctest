using Microsoft.EntityFrameworkCore;
using NetPCTest.Backend.Data;
using NetPCTest.Backend.Models;

namespace NetPCTest.Backend.Repositories;

/// <summary>
/// Entity Framework Core-based implementation of <see cref="IRepository"/>.
/// </summary>
public class DbRepository(AppDbContext context) : IRepository
{
    public async Task<int> GetContactCountAsync(CancellationToken cancellationToken) =>
        await context.Contacts.CountAsync(cancellationToken);
    
    public async Task<List<ContactBrief>> GetContactsAsync(int startIndex, int count, CancellationToken cancellationToken)
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
            .ToListAsync(cancellationToken);

        return contacts;
    }

    public async Task<Contact?> GetContactByEmailAsync(string email, CancellationToken cancellationToken) => 
        await context.Contacts
            .Where(c => c.Email == email)
            .FirstOrDefaultAsync(cancellationToken);

    public async Task<Contact?> GetContactAsync(int id, CancellationToken cancellationToken) => 
        await context.Contacts
            .Where(c => c.Id == id)
            .FirstOrDefaultAsync(cancellationToken);

    public async Task<int?> CreateContactAsync(Contact contact)
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

    public async Task<Category?> GetCategoryAsync(int id, CancellationToken cancellationToken) => 
        await context.Categories
            .Include(c => c.SubCategories)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    
    public async Task<SubCategory?> GetSubCategoryAsync(int id, CancellationToken cancellationToken) => 
        await context.SubCategories
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

    public async Task<int> GetCategoryCountAsync(CancellationToken cancellationToken) =>
        await context.Categories.CountAsync(cancellationToken);
    
    public async Task<List<Category>> GetCategoriesAsync(CancellationToken cancellationToken) =>
        await context.Categories
            .Include(c => c.SubCategories)
            .ToListAsync(cancellationToken);

    public async Task<bool> UpdateContactAsync(int id, Contact contact)
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

    public async Task<bool> DeleteContactAsync(int id)
    {
        try
        {
            var contact = await context.Contacts.FindAsync(id);
            if (contact == null)
                return false;

            context.Remove(contact);
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return false;
        }

        return true;
    }
}
using System.Net.Http.Json;
using NetPCTest.Frontend.Dtos;

namespace NetPCTest.Frontend.Services;

public class ContactsService(HttpClient httpClient) : IContactsService
{
    public async Task<int> GetContactCountAsync()
    {
        var response = 
            await httpClient.GetFromJsonAsync<ContactCountDto>($"contacts/count");

        if (response == null)
            return -1;

        return response.Count;
    }

    public async Task<ContactDto?> GetContactAsync(int id)
    {
        var contact = 
            await httpClient.GetFromJsonAsync<ContactDto>($"contacts/{id}");

        return contact;
    }
    
    public async Task<List<ContactBriefDto>?> GetContactsAsync(int start, int count)
    {
        var contacts = 
            await httpClient.GetFromJsonAsync<List<ContactBriefDto>>($"contacts?startIndex={start}&count={count}");

        return contacts;
    }

    public async Task<bool> UpdateContactAsync(int id, ContactUpdateDto newData)
    {
        var result = 
            await httpClient.PutAsJsonAsync($"contacts/{id}", newData);

        return result.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateContactPasswordAsync(int id, ContactPasswordChangeDto newPassword)
    {
        var result = 
            await httpClient.PutAsJsonAsync($"contacts/{id}/password", newPassword);

        return result.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteContactAsync(int id)
    {
        var response = 
            await httpClient.DeleteAsync($"contacts/{id}");

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> CreateContactAsync(ContactCreationDto newData)
    {
        var response = 
            await httpClient.PostAsJsonAsync($"contacts", newData);

        return response.IsSuccessStatusCode;
    }
}
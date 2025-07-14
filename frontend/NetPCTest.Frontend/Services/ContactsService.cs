using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using NetPCTest.Frontend.Configuration;
using NetPCTest.Frontend.Dtos;

namespace NetPCTest.Frontend.Services;

public class ContactsService(HttpClient httpClient, IOptions<ApiOptions> apiOptions)
{
    public async Task<int> GetContactCount()
    {
        var response = 
            await httpClient.GetFromJsonAsync<ContactCountDto>($"contacts/count");

        if (response == null)
            return -1;

        return response.Count;
    }

    public async Task<ContactDto?> GetContact(int id)
    {
        var contact = 
            await httpClient.GetFromJsonAsync<ContactDto>($"contacts/{id}");

        return contact;
    }
    
    public async Task<List<ContactBriefDto>?> GetContacts(int start, int count)
    {
        var contacts = 
            await httpClient.GetFromJsonAsync<List<ContactBriefDto>>($"contacts?startIndex={start}&count={count}");

        return contacts;
    }

    public async Task<bool> UpdateContact(int id, ContactUpdateDto newData)
    {
        var result = 
            await httpClient.PutAsJsonAsync($"contacts/{id}", newData);

        return result.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateContactPassword(int id, ContactPasswordChangeDto newPassword)
    {
        var result = 
            await httpClient.PutAsJsonAsync($"contacts/{id}/password", newPassword);

        return result.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteContact(int id)
    {
        var response = 
            await httpClient.DeleteAsync($"contacts/{id}");

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> CreateContact(ContactCreationDto newData)
    {
        var response = 
            await httpClient.PostAsJsonAsync($"contacts", newData);

        return response.IsSuccessStatusCode;
    }
}
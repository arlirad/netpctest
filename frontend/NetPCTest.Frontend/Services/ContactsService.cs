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

    public async Task<ContactDto> GetContact(int contactBriefId)
    {
        var contact = 
            await httpClient.GetFromJsonAsync<ContactDto>($"contacts/{contactBriefId}");

        return contact;
    }
    
    public async Task<List<ContactBriefDto>?> GetContacts(int start, int count)
    {
        var contacts = 
            await httpClient.GetFromJsonAsync<List<ContactBriefDto>>($"contacts?startIndex={start}&count={count}");

        return contacts;
    }
}
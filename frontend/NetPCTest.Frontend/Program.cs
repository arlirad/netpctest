using AutoMapper;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Options;
using NetPCTest.Frontend;
using NetPCTest.Frontend.Configuration;
using NetPCTest.Frontend.Handlers;
using NetPCTest.Frontend.Mappers;
using NetPCTest.Frontend.Providers;
using NetPCTest.Frontend.Services;
using NetPCTest.Frontend.Validators;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Here we load our app configuration (mainly for the API base path).
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Services.Configure<ApiOptions>(builder.Configuration.GetSection("api"));
builder.Services.Configure<LocaleOptions>(builder.Configuration.GetSection("locale"));

builder.Services.AddAuthorizationCore();
builder.Services.AddTransient<AuthTokenHandler>();
builder.Services.AddScoped<AuthenticationStateProvider, AppAuthStateProvider>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IContactsService, ContactsService>();
builder.Services.AddScoped<ICategoriesService, CategoriesService>();
builder.Services.AddScoped<ILocalisationService, LocalisationService>();
builder.Services.AddSingleton<IMapper>(provider =>
{
    var config = new MapperConfiguration(cfg =>
    {
        cfg.AddProfile(new MappingProfile());
    });
    
    return config.CreateMapper();
});
builder.Services.AddScoped<ContactFormValidator>();
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddHttpClient("AuthorizedClient", (sp, client) =>
    {
        var options = sp.GetRequiredService<IOptions<ApiOptions>>().Value;
        client.BaseAddress = new Uri(options.BaseUrl);
    }).AddHttpMessageHandler<AuthTokenHandler>();

builder.Services.AddScoped(sp =>
    {
        var factory = sp.GetRequiredService<IHttpClientFactory>();
        return factory.CreateClient("AuthorizedClient");
    });

await builder.Build().RunAsync();
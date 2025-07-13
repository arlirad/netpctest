using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Options;
using NetPCTest.Frontend;
using NetPCTest.Frontend.Configuration;
using NetPCTest.Frontend.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Here we load our app configuration (mainly for the API base path).
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Services.Configure<ApiOptions>(builder.Configuration.GetSection("api"));
builder.Services.Configure<LocaleOptions>(builder.Configuration.GetSection("locale"));

builder.Services.AddSingleton<ContactsService>();
builder.Services.AddSingleton<CategoryService>();
builder.Services.AddSingleton<LocalisationService>();
builder.Services.AddSingleton(sp =>
    {
        var options = sp.GetRequiredService<IOptions<ApiOptions>>().Value;
        return new HttpClient
            { BaseAddress = new Uri(options.BaseUrl) };
    }
);

await builder.Build().RunAsync();
using Blazored.LocalStorage;
using MarketplaceBlazor;
using MarketplaceBlazor.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5021") });
builder.Services.AddBlazoredLocalStorageAsSingleton();
builder.Services.AddScoped<ConversationsService>();

await builder.Build().RunAsync();

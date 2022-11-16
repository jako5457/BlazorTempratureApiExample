using BlazorApp;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;
using Flurl;
using Flurl.Http.Configuration;
using BlazorApp.Services;
using Polly;
using Flurl.Http;
using Microsoft.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();

builder.Services.AddTransient<TempratureApiService>();

builder.Services.AddHttpClient<TempratureApiService>(options =>
{
    options.BaseAddress = new Uri("https://localhost:7242");
})
.AddPolicyHandler(Policy.HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode).WaitAndRetryAsync(3, t => new TimeSpan(0, 0, 10 * t)));

builder.Services.AddOidcAuthentication(options =>
{
    options.ProviderOptions.MetadataUrl = "http://10.135.71.158:8080/realms/TestRealm/.well-known/openid-configuration";
    options.ProviderOptions.ClientId = "BlazorClient";
});

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();

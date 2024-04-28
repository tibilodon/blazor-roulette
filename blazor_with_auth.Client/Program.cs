using blazor_with_auth.Client;
using blazor_with_auth.Client.Repository;
using blazor_with_auth.Shared.Interfaces;
using blazor_with_auth.Shared.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();

//  add http client
builder.Services.AddScoped(http => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress),
});

//  jsInterop
builder.Services.AddScoped<ICookieService, CookieService>();

//  beer repo
builder.Services.AddScoped<IBeerRepository, ClientBeerRepository>();

//  randomizer
builder.Services.AddScoped<IRandomBeerService, RandomBeerService>();


await builder.Build().RunAsync();

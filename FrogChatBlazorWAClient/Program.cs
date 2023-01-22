using Blazored.LocalStorage;
using ClientStorage;
using ClientStorage.GlobalVariable;
using FrogChatBlazorWAClient;
using FrogChatService;
using FrogChatService.AuthStateProvider;
using FrogChatService.CustomHttpDelegate;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ViewModel;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();

//builder.Services.AddTransient(sp =>
//    new HttpClient { BaseAddress = new Uri(@"https://localhost:44305/") });
//builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(@"https://localhost:44305/") });
builder.Services.AddScoped<JwtTokenHandler>();
builder.Services.AddHttpClient<IUserService, UserService>(client=>
{
    client.BaseAddress = new Uri(@"https://localhost:44305/");
    
}).AddHttpMessageHandler<JwtTokenHandler>();


builder.Services.AddSingleton<IProfileViewModel,ProfileViewModel>();
builder.Services.AddSingleton<IContactsViewModel, ContactsViewModel>();
builder.Services.AddScoped<IClientStorage, BlazorLocalStorage>();

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
//builder.Services.AddScoped<IRuntimeConfiguration, RuntimeConfiguration>();



var host = builder.Build();
//var profileViewModel = host.Services.GetRequiredService<IProfileViewModel>();
//profileViewModel.GetProfile();

await host.RunAsync();

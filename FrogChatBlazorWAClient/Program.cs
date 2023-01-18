using FrogChatBlazorWAClient;
using FrogChatService;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ViewModel;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(@"https://localhost:44305/") });
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IProfileViewModel,ProfileViewModel>();

var host = builder.Build();
//var profileViewModel = host.Services.GetRequiredService<IProfileViewModel>();
//profileViewModel.GetProfile();

await host.RunAsync();

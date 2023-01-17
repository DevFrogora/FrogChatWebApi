using FrogChatBlazorWAClient;
using FrogChatService;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(@"https://localhost:44305/") });
builder.Services.AddScoped<IUserService, UserService>();

await builder.Build().RunAsync();

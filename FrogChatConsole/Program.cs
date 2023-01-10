using FrogChatService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FrogChatConsole
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                        .ConfigureServices(services =>
                        {
                            services.AddSingleton<UserService>();

                            services.AddHttpClient<IUserService, UserService>(client =>
                            {
                                client.BaseAddress = new Uri("https://localhost:44305/");
                            });
                        }).Build();

            var iUserService = host.Services.GetRequiredService<IUserService>();
            foreach(var user in await iUserService.GetUsersAsync())
            {
                Console.Write(user.Name + " ,");
                Console.Write(user.Identifier+" ,"); 
                Console.WriteLine(user.Email);
            }
            Console.WriteLine();
        }


    }
}
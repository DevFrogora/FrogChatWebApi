using FrogChatService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FrogChatModel.DomainModel;
namespace FrogChatConsole
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                        .ConfigureServices(services =>
                        {
                            //services.AddSingleton<UserService>();

                            services.AddHttpClient<IUserService, UserService>(client =>
                            {
                                client.BaseAddress = new Uri("https://localhost:44305/");
                            });
                        }).Build();

            IUserService userService = host.Services.GetRequiredService<IUserService>();
            await userService.UpdateUser(new DTOUser()
            {
                Id = 2,
                Name = "demo name",
                Email = "DemoEmailsadsa.com",
                Identifier = "109111229606383522361",
                PhotoPath = @"https://lh3.googleusercontent.com/a/AEdFTp5jcsydwm4AsQRoEruEyjnu9ic2B8vX1wc3zBC7=s96-c"
            });
            var user = await userService.GetUser("109111229606383522361");
            Console.WriteLine(user.Name);
            Console.WriteLine(user.Email);
        }


    }
}

//foreach(var user in await userService.GetUsersAsync())
//{
//    Console.Write(user.Name + " ,");
//    Console.Write(user.Identifier+" ,"); 
//    Console.WriteLine(user.Email);
//}
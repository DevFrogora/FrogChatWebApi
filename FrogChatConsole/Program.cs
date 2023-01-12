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
            var user1 = await userService.AddUserAsync(new SignUpUserDto()
            {
                //Id = 3,
                Name = "demo name",
                Email = "DemoEmailsadsa.com",
                Identifier = "109111229606383522361",
                PhotoPath = @"https://lh3.googleusercontent.com/a/AEdFTp5jcsydwm4AsQRoEruEyjnu9ic2B8vX1wc3zBC7=s96-c"
            });

            var user2 = await userService.AddUserAsync(new SignUpUserDto()
            {
                //Id = 4,
                Name = "Frogo name",
                Email = "FrogEmailS23.com",
                Identifier = "10911122960638352290",
                PhotoPath = @"https://lh3.googleusercontent.com/a/AEdFTp5jcsydwm4AsQRoEruEyjnu9ic2B8vX1wc3zBC7=s96-c"
            });
            var user3 = await userService.AddUserAsync(new SignUpUserDto()
            {
                //Id = 5,
                Name = "pano name",
                Email = "PenoEmails214324.com",
                Identifier = "109111229606383522456",
                PhotoPath = @"https://lh3.googleusercontent.com/a/AEdFTp5jcsydwm4AsQRoEruEyjnu9ic2B8vX1wc3zBC7=s96-c"
            });

            Console.WriteLine("Get ALl Users : ");
            foreach (var user in await userService.GetUsersAsync())
            {
                Console.Write(user.Name + " ,");
                Console.Write(user.Identifier + " ,");
                Console.WriteLine(user.Email);
            }
            var delUser = await userService.DeleteUserAsync(user1);
            Console.Write("Delete user: ",delUser.Name);
            Console.WriteLine(delUser.Email);

            Console.WriteLine("Get ALl Users : ");
            foreach (var user in await userService.GetUsersAsync())
            {
                Console.Write(user.Name + " ,");
                Console.Write(user.Identifier + " ,");
                Console.WriteLine(user.Email);
            }
        }


    }
}

//foreach(var user in await userService.GetUsersAsync())
//{
//    Console.Write(user.Name + " ,");
//    Console.Write(user.Identifier+" ,"); 
//    Console.WriteLine(user.Email);
//}
//await userService.UpdateUserAsync(new DTOUser()
//{
//    Id = 2,
//    Name = "demo name",
//    Email = "DemoEmailsadsa.com",
//    Identifier = "109111229606383522361",
//    PhotoPath = @"https://lh3.googleusercontent.com/a/AEdFTp5jcsydwm4AsQRoEruEyjnu9ic2B8vX1wc3zBC7=s96-c"
//});
//var user = await userService.GetUserAsync("109111229606383522361");
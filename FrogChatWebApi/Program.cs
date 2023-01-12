using FrogChatDAL;
using FrogChatDAL.Repositories;
//using FrogChatDAL.Repositories.EF;
using FrogChatDAL.Repositories.InMemory;
using FrogChatModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FrogChatWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // Ef core Db Config
            builder.Services.AddDbContext<FrogChatDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection")));

            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<FrogChatDbContext>();



            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
            builder.Services.AddControllers();

            //DI
            builder.Services.AddTransient<IAccountRepository, InMemoryAccountRepository>();


            var app = builder.Build();



            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
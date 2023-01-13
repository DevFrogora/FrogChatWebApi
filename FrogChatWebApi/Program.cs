using FrogChatDAL;
using FrogChatDAL.DomainModel;
using FrogChatDAL.Repositories;
//using FrogChatDAL.Repositories.EF;
using FrogChatDAL.Repositories.InMemory;
using FrogChatModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._+";
            }).AddEntityFrameworkStores<FrogChatDbContext>();

            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.User.RequireUniqueEmail = true;
            });

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                // it will check token is present or not
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration.GetValue<string>("Jwt:Issuer"),
                    ValidAudience = builder.Configuration.GetValue<string>("Jwt:Audience"),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("Jwt:key")))
                };
            });


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
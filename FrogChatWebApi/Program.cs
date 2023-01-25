using FrogChatDAL;
using FrogChatDAL.DomainModel;
using FrogChatDAL.Repositories;
using FrogChatDAL.Repositories.Identity;
//using FrogChatDAL.Repositories.EF;
using FrogChatDAL.Repositories.InMemory;
using FrogChatModel;
using FrogChatWebApi.Hubs;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace FrogChatWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigurationManager? configuration = builder.Configuration;

            // Add services to the container.

            // Ef core Db Config
            builder.Services.AddDbContext<FrogChatDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection")));
            builder.Services.AddSignalR();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            }).AddEntityFrameworkStores<FrogChatDbContext>();

            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.User.RequireUniqueEmail = true;
            });

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = GoogleDefaults.AuthenticationScheme;
            }).AddCookie(opt =>
            {
                //opt.Cookie.Name = "TryingoutGoogleOauth";
                //opt.LoginPath = "/auth/google-login";
            })
                .AddJwtBearer(options =>
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
            }).AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = configuration.GetValue<string>("Authentication:Google:ClientId");
                googleOptions.ClientSecret = configuration.GetValue<string>("Authentication:Google:ClientSecret");

                googleOptions.Scope.Add("profile");
                googleOptions.Events.OnCreatingTicket = context =>
                {
                    string pictureUri = context.User.GetProperty("picture").GetString();
                    context.Identity.AddClaim(new Claim("picture", pictureUri));

                    return Task.CompletedTask;
                };
            });


            builder.Services.AddAutoMapper(typeof(AutoMapperProfile),typeof(DomainAutoMapperProfile));
            builder.Services.AddControllers();

            //DI
            builder.Services.AddScoped<IAccountRepository, IdentityAccountRepository>();
            builder.Services.AddScoped<IRoleRepository, IdentityRoleRepository>();
            builder.Services.AddScoped<IUserRepository, IdentityUserRepository>();

            const string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins("https://localhost:7206").AllowAnyMethod().AllowAnyHeader().AllowCredentials();
                                      //builder.WithOrigins("https://devfrogora.github.io");
                                      //builder.AllowAnyOrigin();
                                      //builder.AllowAnyMethod().AllowAnyHeader();
                                  });
            });

            var app = builder.Build();



            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();
            app.UseCors(MyAllowSpecificOrigins);
            app.UseAuthentication();

            app.MapControllers();
            app.UseAuthorization();

            app.MapHub<ChatHub>("/chathub");
            app.Run();
        }
    }
}
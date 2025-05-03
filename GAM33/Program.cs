
using Gam33.Repositries.Data;
using Gam33.Repositries.Identity;
using Gam33.Repositries.Repos;
using GAM33.Helpers;
using Gma33.Core.Entites.IdentityEntites;
using Gma33.Core.Interfaces;
using Gma33.Core.Interfaces.IdentityServicesInterfaces;
using Gma33.Core.Specfication;
using Gma33.Services.TokenServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GAM33
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddDbContext<IdentityContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                               .AddDefaultTokenProviders()
                             .AddEntityFrameworkStores<IdentityContext>();
                            

            builder.Services.AddScoped<IToken, TokenServices>();
            builder.Services.AddAutoMapper(typeof(MappingProfile));


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var _Dbcontext = services.GetRequiredService<StoreContext>();
            var UserManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var _IdentityDbContext = services.GetRequiredService<IdentityContext>();


            var LoogerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                await _Dbcontext.Database.MigrateAsync();
                await StoreContextSeed.DataSeedAsync(_Dbcontext);
                await _IdentityDbContext.Database.MigrateAsync();
                await IdentityDBContextSeed.IdentityDataSeed(UserManager);

            }
            catch (Exception ex)
            {

                var Looger = LoogerFactory.CreateLogger<Program>();

                Looger.LogError(ex, "An Error Happend During Migrate");

            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseStaticFiles();

            app.MapControllers();

            app.Run();
        }
    }
}

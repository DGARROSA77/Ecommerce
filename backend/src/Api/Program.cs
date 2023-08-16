using System.Text;
using Ecommerce.Domain;
using Ecommerce.Persistence;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Stripe;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddDbContext<EcommerceDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString"),
            b => b.MigrationsAssembly(typeof(EcommerceDbContext).Assembly.FullName)
            )

        );

        // Add services to the container.

        builder.Services.AddControllers(opt =>
        {
            var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
            opt.Filters.Add(new AuthorizeFilter(policy));
        });

        IdentityBuilder identityBuilder = builder.Services.AddIdentityCore<User>();
        identityBuilder = new IdentityBuilder(identityBuilder.UserType, identityBuilder.Services);
        identityBuilder.AddRoles<IdentityRole>().AddDefaultTokenProviders();
        identityBuilder.AddClaimsPrincipalFactory<UserClaimsPrincipalFactory<User, IdentityRole>>();

        identityBuilder.AddEntityFrameworkStores<EcommerceDbContext>();
        identityBuilder.AddSignInManager<SignInManager<User>>();

        builder.Services.TryAddSingleton<ISystemClock, SystemClock>();


        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]!));
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt => {
            opt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateAudience = false,
                ValidateIssuer = false
            };
        });

        builder.Services.AddCors(options =>{
            options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        });


        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        //app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseCors("CorsPolicy");

        app.MapControllers();

        app.Run();
    }
}
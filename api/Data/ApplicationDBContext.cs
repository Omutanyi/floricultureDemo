using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using api.Services;


namespace api.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions) {}

        public void ConfigureServices(IServiceCollection services)
    {

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Secret phase"));
        // Encoding.ASCII.GetBytes("eyJhbGciOiJIUzI1NiJ9.eyJSb2xlIjoiVXNlciIsIklzc3VlciI6ImRldkphdmlEZW1vIiwiVXNlcm5hbWUiOiJGbG9yaWN1bHRyZSIsImV4cCI6MTgxNTU2NTQyOSwiaWF0IjoxNzIwOTU3NDI5fQ.ePUAs999ZO53EfG-G7Uksh_HLP-M1SsVCyyq-Ad8FCs");

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Secret phase")),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });

        // services.AddSingleton<JwtService>();
        // Other service configurations
    }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public DbSet<User> User { get; set;}
        public DbSet<Gender> Gender { get; set;}
        public DbSet<Order> Order { get; set;} 
        public DbSet<OrderDetail> OrderDetail { get; set;} 
        public DbSet<Category> Category { get; set;} 
        public DbSet<Product> Product { get; set;} 
    }
}
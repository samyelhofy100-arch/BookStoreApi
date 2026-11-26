using BookStore.Api.Repositories;
using BookStore.Api.Data;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using BookStore.Api.Repositories;
namespace BookStore.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Register DbContext
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")
                ));

            // Add services to the container.
            builder.Services.AddControllers();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IBookRepository, BookRepository>();
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            // OpenAPI
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}

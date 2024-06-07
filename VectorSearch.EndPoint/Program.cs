
using Microsoft.EntityFrameworkCore;
using VectorSearch.ApplicationService.Queries;
using VectorSearch.ApplicationService.Services;
using VectorSearch.EntityFramework.DbContexts;

namespace VectorSearch.EndPoint
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            var config = builder.Services.BuildServiceProvider().GetRequiredService<IConfiguration>();

            var connectionString = config.GetConnectionString("sqlite");

            builder.Services.AddSingleton<IVSDbContextFactory, VSDbContextFactory>();
            builder.Services.AddDbContextFactory<VSDbContext>(options =>
            {
                options.UseSqlite(connectionString);
            });

            builder.Services.AddSingleton<IWordService, WordService>();

            builder.Services.AddControllers();
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

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

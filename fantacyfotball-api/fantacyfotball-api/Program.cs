using Microsoft.EntityFrameworkCore;

namespace fantacyfotball_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

                builder.Services.AddDbContext<FantasyFootballDbContext>(x => x
            .UseMongoDB(builder.Configuration.GetConnectionString("ConnString"), builder.Configuration["DB"]));


            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            DbSeeder.Seed(app.Services.CreateScope().ServiceProvider);

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

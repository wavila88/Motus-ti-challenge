
using Domain.Repository;
using Domain.UserService;
using Microsoft.EntityFrameworkCore;
using RepositorySQL.DBContext;
using RepositorySQL.Queries;

namespace ChallengeBack
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();



            builder.Services.AddDbContext<RepositorySQL.DBContext.ApplicationDbContext>(options =>
              options.UseSqlServer(
                  builder.Configuration.GetConnectionString("Default"),
                  sqlOptions => sqlOptions.EnableRetryOnFailure()
              )
          );

            //Inject the repository allowing Dependenncy Inversion principle
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();

            //DataBasecontext
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("Default"),
                    sqlOptions => {
                        sqlOptions.EnableRetryOnFailure();
                        sqlOptions.MigrationsAssembly("RepositorySQL"); 
                    }
                )
            );

            // Add CORS policy
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:5173")
                              .AllowAnyHeader()
                              .AllowAnyMethod()
                              .AllowCredentials();
                    });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
                app.UseSwagger();
                app.UseSwaggerUI();
            //}
            //Habilitate CORS
            app.UseCors("AllowFrontend");

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

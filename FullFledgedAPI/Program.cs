
using AutoMapper;
using FullFledgedAPI.Container;
using FullFledgedAPI.Helper;
using FullFledgedAPI.Repos;
using FullFledgedAPI.Service;
using Microsoft.EntityFrameworkCore;

namespace FullFledgedAPI
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

            //Dependency Injection
            builder.Services.AddTransient<ICustomerService, CustomerService>();

            //Have to Register Database Service
            builder.Services.AddDbContext<FullFledgedAPIContext>(o => o.UseSqlServer(
                builder.Configuration.GetConnectionString("apicon"))
                );
            var automapper = new MapperConfiguration(item => item.AddProfile(new AutoMapperHandler()));
            IMapper mapper = automapper.CreateMapper();
            builder.Services.AddSingleton(mapper);




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
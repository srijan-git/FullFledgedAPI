using AutoMapper;
using FullFledgedAPI.Container;
using FullFledgedAPI.Helper;
using FullFledgedAPI.Repos;
using FullFledgedAPI.Service;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Serilog;

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

            //Automapper
            var automapper = new MapperConfiguration(item => item.AddProfile(new AutoMapperHandler()));
            IMapper mapper = automapper.CreateMapper();
            builder.Services.AddSingleton(mapper);

            //We can add CORS policy in different way
            //CORS policy
            builder.Services.AddCors(p => p.AddPolicy("corspolicy", build => { build.WithOrigins("https://domain1.com", "https://domain2.com").AllowAnyMethod().AllowAnyHeader(); }));

            //We can addd cors policy in controller also
            builder.Services.AddCors(p => p.AddPolicy("corspolicy1", build => { build.WithOrigins("https://domain3.com").AllowAnyMethod().AllowAnyHeader(); }));

            //To enable cors for multiple domains 
            builder.Services.AddCors(p => p.AddPolicy("corspolicy", build => { build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader(); }));

            //And to add default CORS policy we can write
            builder.Services.AddCors(p => p.AddDefaultPolicy(build => { build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader(); }));

            builder.Services.AddRateLimiter(_ => _.AddFixedWindowLimiter(policyName: "fixedwindow", options =>
            {
                options.Window = TimeSpan.FromSeconds(10);
                options.PermitLimit = 1;
                options.QueueLimit = 0;
                options.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
            }).RejectionStatusCode = 401);

            //Logger
            string logpath = builder.Configuration.GetSection("Logging:LogPath").Value;
            var _logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("microsoft", Serilog.Events.LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.File(logpath)
                .CreateLogger();

            builder.Logging.AddSerilog(_logger);
            var app = builder.Build();
            app.UseRateLimiter();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //Have to Incluse policy name to enable CORS 
            //app.UseCors("corspolicy");

            //And to Enable default CORS policy we can write
            app.UseCors();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
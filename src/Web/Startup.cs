using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MinuteOfHappiness.Frontend.Data.Context;
using Serilog;

namespace MinuteOfHappiness.Frontend.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // Add the configuration settings
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddJsonFile("appsettings.ConnectionStrings.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            // Add the logger to the pipeline
            var serilog = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration);

            loggerFactory.AddSerilog(serilog.CreateLogger());
        }

        public IConfigurationRoot Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services
            services.AddMvc();

            // Add AutoMapper
            services.AddAutoMapper();

            // Add customer services
            services.AddScoped<ApplicationDbContext>(factory => 
                new ApplicationDbContext(Configuration.GetConnectionString("DefaultConnection")));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Add the exception page when in development
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}

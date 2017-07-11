using MAF.FeaturesFlipping.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace SimpleWebSite
{
    public class Startup
    {

        public IConfiguration Configuration { get; set; }
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            Configuration = builder.Build();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddFeaturesFlipping()
                    .AddGlobalConfigurationActivator(Configuration.GetSection("Features"))
                .AddSpecificEntityFrameworkCoreActivator<string>(_ => _.UseInMemoryDatabase(), "TenantId",
                    featureContext =>
                    {
                        var tenantId = featureContext.GetPart<string>("TenantId");
                        return feature => feature.OtherColumn == tenantId;
                    })
                .AddGlobalEntityFrameworkCoreActivator(_ => { _.UseInMemoryDatabase(); },
                    _ => _.Schema("ee"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async context => { await context.Response.WriteAsync("Hello World!"); });
        }
    }
}
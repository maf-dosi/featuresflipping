using MAF.FeaturesFlipping;
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
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddFeaturesFlipping()
                    .AddGlobalConfigurationActivator(Configuration.GetSection("Features"))
                .AddSpecificEntityFrameworkCoreActivator<string>(_ => _.UseInMemoryDatabase("FeaturesFlipping"), "TenantId",
                    featureContext =>
                    {
                        var tenantId = featureContext.GetPart<string>("TenantId");
                        return feature => feature.OtherColumn == tenantId;
                    })
                .AddGlobalEntityFrameworkCoreActivator(_ => { _.UseInMemoryDatabase("FeaturesFlipping"); },
                    _ => _.Schema("ee"));

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMvcWithDefaultRoute();
            app.Run(async context =>
            {
                var featureService = context.RequestServices.GetService<IFeatureService>();
                if (await featureService.IsFeatureActiveAsync(new FeatureSpec("", "", "")))
                {
                    await context.Response.WriteAsync("Hello World!");
                }
                else
                {
                    await context.Response.WriteAsync("Not Hello World!");
                }
            });
        }
    }
}
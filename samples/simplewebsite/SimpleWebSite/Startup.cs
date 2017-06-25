using System.Threading.Tasks;
using MAF.FeaturesFlipping.Extensibility.Activators;
using MAF.FeaturesFlipping.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace SimpleWebSite
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddFeaturesFlipping()
                .AddSpecificEntityFrameworkCoreActivator<string>(_ => _.UseInMemoryDatabase(), "TenantId", (entity, featureContext) =>
               {
                   if (entity.OtherColumn == featureContext.GetPart<string>("TenantId"))
                   {
                       return entity.IsActive == null
                           ? Task.FromResult(FeatureActivationStatus.NotSet)
                           : entity.IsActive.Value
                               ? Task.FromResult(FeatureActivationStatus.Active)
                               : Task.FromResult(FeatureActivationStatus.Inactive);
                   }
                   return Task.FromResult(FeatureActivationStatus.NotSet);
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
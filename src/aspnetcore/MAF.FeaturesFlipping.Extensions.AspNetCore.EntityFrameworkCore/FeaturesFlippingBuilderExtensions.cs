using System;
using MAF.FeaturesFlipping.Activators.EntityFrameworkCore.Global;
using MAF.FeaturesFlipping.Extensibility.Activators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace MAF.FeaturesFlipping.Extensions.AspNetCore
{
    public static class FeaturesFlippingBuilderExtensions
    {
        public static void AddGlobalEntityFrameworkCoreActivator(this IFeaturesFlippingBuilder featureFlippingBuilder,
            Action<DbContextOptionsBuilder> dbContextBuilderAction)
        {
            featureFlippingBuilder.AddGlobalEntityFrameworkCoreActivator(dbContextBuilderAction, _ =>{});
        }
        public static void AddGlobalEntityFrameworkCoreActivator(this IFeaturesFlippingBuilder featureFlippingBuilder,
            Action<DbContextOptionsBuilder> dbContextBuilderAction, Action<GlobalDbContextConfiguration> globalDbContextConfigurer)
        {
            featureFlippingBuilder.Services.AddScoped<IFeatureActivator, GlobalEntityFrameworkCoreActivator>();
            featureFlippingBuilder.Services.AddEntityFramework()
                .AddDbContext<GlobalFeatureDbContext>();

            var globalDbContextConfiguration = new GlobalDbContextConfiguration(dbContextBuilderAction);
            globalDbContextConfigurer(globalDbContextConfiguration);
            featureFlippingBuilder.Services.AddSingleton(globalDbContextConfiguration);

        }
    }
}

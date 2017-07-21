using System;
using MAF.FeaturesFlipping.Activators.EntityFrameworkCore.Global;
using MAF.FeaturesFlipping.Extensibility.Activators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace MAF.FeaturesFlipping.Extensions.DependencyInjection
{
    public static class FeaturesFlippingBuilderGlobalEFCoreExtensions
    {
        public static IFeaturesFlippingBuilder AddGlobalEntityFrameworkCoreActivator(this IFeaturesFlippingBuilder featureFlippingBuilder,
            Action<DbContextOptionsBuilder> dbContextBuilderAction)
        {
            return featureFlippingBuilder.AddGlobalEntityFrameworkCoreActivator(dbContextBuilderAction, _ =>{});
        }
        public static IFeaturesFlippingBuilder AddGlobalEntityFrameworkCoreActivator(this IFeaturesFlippingBuilder featureFlippingBuilder,
            Action<DbContextOptionsBuilder> dbContextBuilderAction, Action<GlobalDbContextConfiguration> globalDbContextConfigurer)
        {
            featureFlippingBuilder.Services.AddScoped<IFeatureActivator, GlobalEntityFrameworkCoreActivator>();
            featureFlippingBuilder.Services.AddEntityFramework()
                .AddDbContext<GlobalFeatureDbContext>();

            var globalDbContextConfiguration = new GlobalDbContextConfiguration(dbContextBuilderAction);
            globalDbContextConfigurer(globalDbContextConfiguration);
            featureFlippingBuilder.Services.AddSingleton(globalDbContextConfiguration);
            return featureFlippingBuilder;
        }
    }
}

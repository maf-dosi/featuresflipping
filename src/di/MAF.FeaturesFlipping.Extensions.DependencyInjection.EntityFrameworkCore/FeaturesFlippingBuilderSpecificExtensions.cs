using System;
using System.Threading.Tasks;
using MAF.FeaturesFlipping.Activators.EntityFrameworkCore.Specific;
using MAF.FeaturesFlipping.Extensibility.Activators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace MAF.FeaturesFlipping.Extensions.DependencyInjection
{
    public static class FeaturesFlippingBuilderSpecificExtensions
    {
        public static IFeaturesFlippingBuilder AddSpecificEntityFrameworkCoreActivator<TOtherColumn>(
            this IFeaturesFlippingBuilder featureFlippingBuilder,
            Action<DbContextOptionsBuilder> dbContextBuilderAction, string otherColumnName,
            Func<SpecificFeatureEntity<TOtherColumn>, IFeatureContext, Task<FeatureActivationStatus>>
                filterSpecificFeature)
        {
            return featureFlippingBuilder.AddSpecificEntityFrameworkCoreActivator(dbContextBuilderAction, otherColumnName,
                filterSpecificFeature, _ => { });
        }

        public static IFeaturesFlippingBuilder AddSpecificEntityFrameworkCoreActivator<TOtherColumn>(
            this IFeaturesFlippingBuilder featureFlippingBuilder,
            Action<DbContextOptionsBuilder> dbContextBuilderAction, string otherColumnName,
            Func<SpecificFeatureEntity<TOtherColumn>, IFeatureContext, Task<FeatureActivationStatus>>
                filterSpecificFeature, Action<SpecificDbContextConfiguration<TOtherColumn>> specificDbContextConfigurer)
        {
            featureFlippingBuilder.Services
                .AddScoped<IFeatureActivator, SpecificEntityFrameworkCoreActivator<TOtherColumn>>();
            featureFlippingBuilder.Services.AddEntityFramework()
                .AddDbContext<SpecificFeatureDbContext<TOtherColumn>>();

            var specificDbContextConfiguration =
                new SpecificDbContextConfiguration<TOtherColumn>(dbContextBuilderAction, otherColumnName,
                    filterSpecificFeature);
            specificDbContextConfigurer(specificDbContextConfiguration);
            featureFlippingBuilder.Services.AddSingleton(specificDbContextConfiguration);
            return featureFlippingBuilder;
        }
    }
}
using System;
using System.Linq.Expressions;
using MAF.FeaturesFlipping.Activators.EntityFrameworkCore.Specific;
using MAF.FeaturesFlipping.Extensibility.Activators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace MAF.FeaturesFlipping.Extensions.DependencyInjection
{
    public static class FeaturesFlippingBuilderSpecificEFCoreExtensions
    {
        public static IFeaturesFlippingBuilder AddSpecificEntityFrameworkCoreActivator<TOtherColumn>(this IFeaturesFlippingBuilder featureFlippingBuilder, 
            Action<DbContextOptionsBuilder> dbContextBuilderAction, string otherColumnName,
            Func<IFeatureContext, Expression<Func<SpecificFeatureEntity<TOtherColumn>, bool>>> specificFeatureFilterWithContext)
        {
            return featureFlippingBuilder.AddSpecificEntityFrameworkCoreActivator(dbContextBuilderAction, otherColumnName,
                specificFeatureFilterWithContext, _ => { });
        }

        public static IFeaturesFlippingBuilder AddSpecificEntityFrameworkCoreActivator<TOtherColumn>(this IFeaturesFlippingBuilder featureFlippingBuilder,
            Action<DbContextOptionsBuilder> dbContextBuilderAction, string otherColumnName,
            Func<IFeatureContext, Expression<Func<SpecificFeatureEntity<TOtherColumn>, bool>>> specificFeatureFilterWithContext, Action<SpecificDbContextConfiguration<TOtherColumn>> specificDbContextConfigurer)
        {
            featureFlippingBuilder.Services.AddScoped<IFeatureActivator, SpecificEntityFrameworkCoreActivator<TOtherColumn>>();
            featureFlippingBuilder.Services.AddEntityFramework().AddDbContext<SpecificFeatureDbContext<TOtherColumn>>();

            var specificDbContextConfiguration =
                new SpecificDbContextConfiguration<TOtherColumn>(dbContextBuilderAction, otherColumnName,
                    specificFeatureFilterWithContext);
            specificDbContextConfigurer(specificDbContextConfiguration);
            featureFlippingBuilder.Services.AddSingleton(specificDbContextConfiguration);
            return featureFlippingBuilder;
        }
    }
}
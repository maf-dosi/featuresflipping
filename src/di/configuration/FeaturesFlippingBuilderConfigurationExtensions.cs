using MAF.FeaturesFlipping.Activators.Configuration;
using MAF.FeaturesFlipping.Extensibility.Activators;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace MAF.FeaturesFlipping.Extensions.DependencyInjection
{
    public static class FeaturesFlippingBuilderGlobalConfigurationExtensions
    {
        public static IFeaturesFlippingBuilder AddConfigurationActivator(
            this IFeaturesFlippingBuilder featureFlippingBuilder,
            IConfigurationSection configurationSection)
        {
            featureFlippingBuilder.Services.AddSingleton<IFeatureActivator>(
                new ConfigurationFeatureActivator(configurationSection));

            return featureFlippingBuilder;
        }
    }
}
using System;
using MAF.FeaturesFlipping;
using MAF.FeaturesFlipping.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IFeaturesFlippingBuilder AddFeaturesFlipping(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddFeaturesFlipping(_ => { });
        }

        public static IFeaturesFlippingBuilder AddFeaturesFlipping(this IServiceCollection serviceCollection,
            Action<FeaturesFlippingOptions> optionsSetup)
        {
            serviceCollection.AddScoped<IFeatureService, FeatureService>();
            var featuresFlippingOptions = new FeaturesFlippingOptions();
            optionsSetup(featuresFlippingOptions);
            serviceCollection.AddSingleton(featuresFlippingOptions);
            var featureFlippingBuilder = new FeaturesFlippingBuilder(serviceCollection);
            return featureFlippingBuilder;
        }
    }
}

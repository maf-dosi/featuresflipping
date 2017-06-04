using MAF.FeaturesFlipping;
using MAF.FeaturesFlipping.Extensions.AspNetCore;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IFeaturesFlippingBuilder AddFeaturesFlipping(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IFeatureService, FeatureService>();
            
            var featureFlippingBuilder = new FeaturesFlippingBuilder(serviceCollection);
            return featureFlippingBuilder;
        }
    }
}

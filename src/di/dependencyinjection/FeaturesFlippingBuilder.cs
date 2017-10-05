using Microsoft.Extensions.DependencyInjection;

namespace MAF.FeaturesFlipping.Extensions.DependencyInjection
{
    internal class FeaturesFlippingBuilder : IFeaturesFlippingBuilder
    {
        public FeaturesFlippingBuilder(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IFeatureService, FeatureService>();
            serviceCollection.AddScoped<IFeatureContextAccessor, FeatureContextAccessor>();

            Services = serviceCollection;
        }
        public IServiceCollection Services { get; }
    }
}
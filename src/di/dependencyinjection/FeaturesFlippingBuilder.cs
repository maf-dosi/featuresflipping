using Microsoft.Extensions.DependencyInjection;

namespace MAF.FeaturesFlipping.Extensions.DependencyInjection
{
    internal class FeaturesFlippingBuilder : IFeaturesFlippingBuilder
    {
        public FeaturesFlippingBuilder(IServiceCollection serviceCollection)
        {
            Services = serviceCollection;
        }
        public IServiceCollection Services { get; }
    }
}
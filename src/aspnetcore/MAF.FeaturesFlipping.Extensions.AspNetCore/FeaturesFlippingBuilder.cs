using Microsoft.Extensions.DependencyInjection;

namespace MAF.FeaturesFlipping.Extensions.AspNetCore
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
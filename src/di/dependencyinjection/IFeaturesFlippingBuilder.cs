using Microsoft.Extensions.DependencyInjection;

namespace MAF.FeaturesFlipping.Extensions.DependencyInjection
{
    public interface IFeaturesFlippingBuilder
    {
        IServiceCollection Services { get; }
    }
}
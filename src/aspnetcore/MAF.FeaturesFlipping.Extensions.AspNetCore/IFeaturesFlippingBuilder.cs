using Microsoft.Extensions.DependencyInjection;

namespace MAF.FeaturesFlipping.Extensions.AspNetCore
{
    public interface IFeaturesFlippingBuilder
    {
        IServiceCollection Services { get; }
    }
}
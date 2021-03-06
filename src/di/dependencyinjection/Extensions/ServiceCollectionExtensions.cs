﻿using MAF.FeaturesFlipping.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IFeaturesFlippingBuilder AddFeaturesFlipping(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddMemoryCache();
            
            var featureFlippingBuilder = new FeaturesFlippingBuilder(serviceCollection);
            return featureFlippingBuilder;
        }
    }
}

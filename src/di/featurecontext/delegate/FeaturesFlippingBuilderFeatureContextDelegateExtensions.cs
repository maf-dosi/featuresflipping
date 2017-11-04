using System;
using System.Threading.Tasks;
using MAF.FeaturesFlipping.Extensibility.Activators;
using MAF.FeaturesFlipping.Extensibility.FeatureContext;
using MAF.FeaturesFlipping.FeatureContext.Delegate;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace MAF.FeaturesFlipping.Extensions.DependencyInjection
{
    public static class FeaturesFlippingBuilderFeatureContextDelegateExtensions
    {
        public static IFeaturesFlippingBuilder AddDelegateFeatureContextPart(
            this IFeaturesFlippingBuilder featuresFlippingBuilder, Func<IFeatureContext, Task> addFeatureContextPart)
        {
            return featuresFlippingBuilder.AddDelegateFeatureContextPart(addFeatureContextPart, _ => { });
        }

        public static IFeaturesFlippingBuilder AddDelegateFeatureContextPart(
            this IFeaturesFlippingBuilder featuresFlippingBuilder, Func<IFeatureContext, Task> addFeatureContextPart,
            Action<IFeatureContext> releaseFeatureContextPart)
        {
            var delegateFeatureContextPartFactory = new DelegateFeatureContextPartFactory(
                addFeatureContextPart, releaseFeatureContextPart);
            featuresFlippingBuilder.Services.AddSingleton<IFeatureContextPartFactory>(delegateFeatureContextPartFactory);
            return featuresFlippingBuilder;
        }
    }
}
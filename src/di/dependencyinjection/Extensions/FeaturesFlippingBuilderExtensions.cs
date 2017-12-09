using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace MAF.FeaturesFlipping.Extensions.DependencyInjection
{
    public static class FeaturesFlippingBuilderExtensions
    {
        public static IFeaturesFlippingBuilder RegisterScopedService<TContract, TActiveFeatureImplementation, TInactiveFeatureImplementation>(
            this IFeaturesFlippingBuilder featureFlippingBuilder, FeatureSpec featureSpec)
        {
            var serviceDescriptors = CreateServiceDescriptors<TContract, TActiveFeatureImplementation, TInactiveFeatureImplementation>(featureSpec, ServiceLifetime.Scoped);
            foreach (var serviceDescriptor in serviceDescriptors)
            {
                featureFlippingBuilder.Services.Add(serviceDescriptor);
            }
            return featureFlippingBuilder;
        }

        public static IFeaturesFlippingBuilder RegisterTransientService<TContract, TActiveFeatureImplementation, TInactiveFeatureImplementation>(
            this IFeaturesFlippingBuilder featureFlippingBuilder, FeatureSpec featureSpec)
        {
            var serviceDescriptors = CreateServiceDescriptors<TContract, TActiveFeatureImplementation, TInactiveFeatureImplementation>(featureSpec, ServiceLifetime.Transient);
            foreach (var serviceDescriptor in serviceDescriptors)
            {
                featureFlippingBuilder.Services.Add(serviceDescriptor);
            }
            return featureFlippingBuilder;
        }

        public static IFeaturesFlippingBuilder RegisterScopedService<TContract, TActiveFeatureImplementation, TInactiveFeatureImplementation>(
            this IFeaturesFlippingBuilder featureFlippingBuilder, FeatureSpec featureSpec, Func<IServiceProvider, TActiveFeatureImplementation> activeFeatureFactory)
        {
            var serviceDescriptors = CreateServiceDescriptors<TContract, TActiveFeatureImplementation, TInactiveFeatureImplementation>(
                featureSpec, ServiceLifetime.Scoped, activeFeatureFactory);
            foreach (var serviceDescriptor in serviceDescriptors)
            {
                featureFlippingBuilder.Services.Add(serviceDescriptor);
            }
            return featureFlippingBuilder;
        }

        public static IFeaturesFlippingBuilder RegisterTransientService<TContract, TActiveFeatureImplementation, TInactiveFeatureImplementation>(
            this IFeaturesFlippingBuilder featureFlippingBuilder, FeatureSpec featureSpec, Func<IServiceProvider, TActiveFeatureImplementation> activeFeatureFactory)
        {
            var serviceDescriptors = CreateServiceDescriptors<TContract, TActiveFeatureImplementation, TInactiveFeatureImplementation>(
                featureSpec, ServiceLifetime.Transient, activeFeatureFactory);
            foreach (var serviceDescriptor in serviceDescriptors)
            {
                featureFlippingBuilder.Services.Add(serviceDescriptor);
            }
            return featureFlippingBuilder;
        }

        public static IFeaturesFlippingBuilder RegisterScopedService<TContract, TActiveFeatureImplementation, TInactiveFeatureImplementation>(
            this IFeaturesFlippingBuilder featureFlippingBuilder, FeatureSpec featureSpec,
            Func<IServiceProvider, TInactiveFeatureImplementation> inactiveFeatureFactory)
        {
            var serviceDescriptors = CreateServiceDescriptors<TContract, TActiveFeatureImplementation, TInactiveFeatureImplementation>(
                featureSpec, ServiceLifetime.Scoped, inactiveFeatureFactory);
            foreach (var serviceDescriptor in serviceDescriptors)
            {
                featureFlippingBuilder.Services.Add(serviceDescriptor);
            }
            return featureFlippingBuilder;
        }

        public static IFeaturesFlippingBuilder RegisterTransientService<TContract, TActiveFeatureImplementation, TInactiveFeatureImplementation>(
            this IFeaturesFlippingBuilder featureFlippingBuilder, FeatureSpec featureSpec,
            Func<IServiceProvider, TInactiveFeatureImplementation> inactiveFeatureFactory)
        {
            var serviceDescriptors = CreateServiceDescriptors<TContract, TActiveFeatureImplementation, TInactiveFeatureImplementation>(
                featureSpec, ServiceLifetime.Transient, inactiveFeatureFactory);
            foreach (var serviceDescriptor in serviceDescriptors)
            {
                featureFlippingBuilder.Services.Add(serviceDescriptor);
            }
            return featureFlippingBuilder;
        }

        public static IFeaturesFlippingBuilder RegisterScopedService<TContract, TActiveFeatureImplementation, TInactiveFeatureImplementation>(
            this IFeaturesFlippingBuilder featureFlippingBuilder, FeatureSpec featureSpec, Func<IServiceProvider, TActiveFeatureImplementation> activeFeatureFactory,
            Func<IServiceProvider, TInactiveFeatureImplementation> inactiveFeatureFactory)
        {
            var serviceDescriptors = CreateServiceDescriptors<TContract, TActiveFeatureImplementation, TInactiveFeatureImplementation>(
                featureSpec, ServiceLifetime.Scoped, activeFeatureFactory, inactiveFeatureFactory);
            foreach (var serviceDescriptor in serviceDescriptors)
            {
                featureFlippingBuilder.Services.Add(serviceDescriptor);
            }
            return featureFlippingBuilder;
        }

        public static IFeaturesFlippingBuilder RegisterTransientService<TContract, TActiveFeatureImplementation, TInactiveFeatureImplementation>(
            this IFeaturesFlippingBuilder featureFlippingBuilder, FeatureSpec featureSpec, Func<IServiceProvider, TActiveFeatureImplementation> activeFeatureFactory,
            Func<IServiceProvider, TInactiveFeatureImplementation> inactiveFeatureFactory)
        {
            var serviceDescriptors = CreateServiceDescriptors<TContract, TActiveFeatureImplementation, TInactiveFeatureImplementation>(
                featureSpec, ServiceLifetime.Transient, activeFeatureFactory, inactiveFeatureFactory);
            foreach (var serviceDescriptor in serviceDescriptors)
            {
                featureFlippingBuilder.Services.Add(serviceDescriptor);
            }
            return featureFlippingBuilder;
        }

        private static IEnumerable<ServiceDescriptor> CreateServiceDescriptors<TContract, TActiveFeatureImplementation,
            TInactiveFeatureImplementation>(FeatureSpec featureSpec, ServiceLifetime serviceLifetime)
        {
            return CreateServiceDescriptors<TContract>(featureSpec,
                new ServiceDescriptor(typeof(TActiveFeatureImplementation), typeof(TActiveFeatureImplementation), serviceLifetime),
                new ServiceDescriptor(typeof(TInactiveFeatureImplementation), typeof(TInactiveFeatureImplementation), serviceLifetime));
        }
        private static IEnumerable<ServiceDescriptor> CreateServiceDescriptors<TContract, TActiveFeatureImplementation,
            TInactiveFeatureImplementation>(FeatureSpec featureSpec, ServiceLifetime serviceLifetime, Func<IServiceProvider, TActiveFeatureImplementation> activeFeatureFactory)
        {
            object ActiveFeatureFactory(IServiceProvider sp) => activeFeatureFactory(sp);
            return CreateServiceDescriptors<TContract>(featureSpec,
                new ServiceDescriptor(typeof(TActiveFeatureImplementation), ActiveFeatureFactory, serviceLifetime),
                new ServiceDescriptor(typeof(TInactiveFeatureImplementation), typeof(TInactiveFeatureImplementation), serviceLifetime));
        }
        private static IEnumerable<ServiceDescriptor> CreateServiceDescriptors<TContract, TActiveFeatureImplementation,
            TInactiveFeatureImplementation>(FeatureSpec featureSpec, ServiceLifetime serviceLifetime,
            Func<IServiceProvider, TInactiveFeatureImplementation> inactiveFeatureFactory)
        {
            object InactiveFeatureFactory(IServiceProvider sp) => inactiveFeatureFactory(sp);
            return CreateServiceDescriptors<TContract>(featureSpec,
                new ServiceDescriptor(typeof(TActiveFeatureImplementation), typeof(TActiveFeatureImplementation), serviceLifetime),
                new ServiceDescriptor(typeof(TInactiveFeatureImplementation), InactiveFeatureFactory, serviceLifetime));
        }

        private static IEnumerable<ServiceDescriptor> CreateServiceDescriptors<TContract, TActiveFeatureImplementation,
            TInactiveFeatureImplementation>(FeatureSpec featureSpec, ServiceLifetime serviceLifetime, Func<IServiceProvider, TActiveFeatureImplementation> activeFeatureFactory,
            Func<IServiceProvider, TInactiveFeatureImplementation> inactiveFeatureFactory)
        {
            object ActiveFeatureFactory(IServiceProvider sp) => activeFeatureFactory(sp);
            object InactiveFeatureFactory(IServiceProvider sp) => inactiveFeatureFactory(sp);
            return CreateServiceDescriptors<TContract>(featureSpec,
                new ServiceDescriptor(typeof(TActiveFeatureImplementation), ActiveFeatureFactory, serviceLifetime),
                new ServiceDescriptor(typeof(TInactiveFeatureImplementation), InactiveFeatureFactory, serviceLifetime));
        }
        private static IEnumerable<ServiceDescriptor> CreateServiceDescriptors<TContract>(FeatureSpec featureSpec, 
            ServiceDescriptor activeFeatureServiceDescriptor, ServiceDescriptor inactiveFeatureServiceDescriptor)
        {
            yield return new ServiceDescriptor(typeof(TContract),
                serviceProvider =>
                {
                    var scopedServiceProvider = serviceProvider.CreateScope().ServiceProvider;
                    var featureService = scopedServiceProvider.GetService<IFeatureService>();
                    var isActive = featureService.IsFeatureActiveAsync(featureSpec).Result;
                    if (isActive)
                    {
                        return serviceProvider.GetService(activeFeatureServiceDescriptor.ServiceType);
                    }
                    return serviceProvider.GetService(inactiveFeatureServiceDescriptor.ServiceType);
                }, activeFeatureServiceDescriptor.Lifetime);
            yield return activeFeatureServiceDescriptor;
            yield return inactiveFeatureServiceDescriptor;
        }
    }
}
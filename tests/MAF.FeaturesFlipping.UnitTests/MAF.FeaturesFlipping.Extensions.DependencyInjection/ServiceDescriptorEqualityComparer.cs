﻿using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace MAF.FeaturesFlipping.Extensions.DependencyInjection
{
    public class ServiceDescriptorEqualityComparer : IEqualityComparer<ServiceDescriptor>
    {
        public bool Equals(ServiceDescriptor x, ServiceDescriptor y)
        {
            if (x == null)
            {
                return y == null;
            }
            if (y == null)
            {
                return false;
            }
            return x.ServiceType == y.ServiceType &&
                   x.ImplementationType == y.ImplementationType &&
                   (x.ImplementationFactory?.Equals(y.ImplementationFactory) ?? y.ImplementationFactory == null) &&
                   (x.ImplementationInstance?.Equals(y.ImplementationInstance) ?? y.ImplementationInstance == null) &&
                   x.Lifetime == y.Lifetime;
        }

        public int GetHashCode(ServiceDescriptor obj)
        {
            return obj.GetHashCode();
        }
    }
}
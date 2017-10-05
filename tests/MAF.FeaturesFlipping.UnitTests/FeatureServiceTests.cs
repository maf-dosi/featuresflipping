using System.Collections.Generic;
using System.Linq;
using MAF.FeaturesFlipping.Extensibility.Activators;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace MAF.FeaturesFlipping.UnitTests
{
    public partial class FeatureServiceTests
    {
        private static FeatureService CreateFeatureService(IFeatureContextAccessor featureContextAccessor, 
            IMemoryCache memoryCache = null,
            IEnumerable<IFeatureActivator> featureActivators = null)
        {
            return new FeatureService(
                memoryCache ?? new MemoryCache(new OptionsWrapper<MemoryCacheOptions>(new MemoryCacheOptions())),
                featureContextAccessor,
                featureActivators ?? Enumerable.Empty<IFeatureActivator>());
        }
    }
}
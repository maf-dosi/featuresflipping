using System.Collections.Generic;
using System.Linq;
using MAF.FeaturesFlipping.Extensibility.Activators;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;

namespace MAF.FeaturesFlipping
{
    public partial class FeatureServiceTests
    {
        private static FeatureService CreateFeatureService(IFeatureContextAccessor featureContextAccessor, 
            IMemoryCache memoryCache = null, ILogger<FeatureSpec> logger = null,
            IEnumerable<IFeatureActivator> featureActivators = null)
        {
            return new FeatureService(
                memoryCache ?? new MemoryCache(new OptionsWrapper<MemoryCacheOptions>(new MemoryCacheOptions())),
                logger ?? new Logger<FeatureSpec>(new NullLoggerFactory()),
                featureContextAccessor,
                featureActivators ?? Enumerable.Empty<IFeatureActivator>());
        }
    }
}
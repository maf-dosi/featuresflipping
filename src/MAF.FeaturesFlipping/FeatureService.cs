using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MAF.FeaturesFlipping.Extensibility.Activators;
using Microsoft.Extensions.Caching.Memory;

namespace MAF.FeaturesFlipping
{
    public sealed class FeatureService : IFeatureService, IDisposable
    {
        private readonly Dictionary<FeatureSpec, bool> _featureActivationResultCache =
            new Dictionary<FeatureSpec, bool>();

        private readonly IMemoryCache _memoryCache;
        private readonly IEnumerable<IFeatureActivator> _featureActivators;
        private readonly IFeatureContextAccessor _featureContextAccessor;
        private bool _isDisposed;

        public FeatureService(IMemoryCache memoryCache, IFeatureContextAccessor featureContextAccessor, IEnumerable<IFeatureActivator> featureActivators)
        {
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
            _featureContextAccessor = featureContextAccessor ?? throw new ArgumentNullException(nameof(featureContextAccessor));
            _featureActivators = featureActivators ?? throw new ArgumentNullException(nameof(featureActivators));
        }

        public async Task<bool> IsFeatureActiveAsync(FeatureSpec featureSpec)
        {
            if (!_featureActivationResultCache.ContainsKey(featureSpec))
            {
                var cacheKey = $"{nameof(FeatureService)}¤{featureSpec}";
                if (!_memoryCache.TryGetValue(cacheKey, out IList<AsyncLazy<IFeature>> lazyFeatures))
                {
                    lazyFeatures = ComputeLazyFeatures(featureSpec);
                    _memoryCache.Set(cacheKey, lazyFeatures);
                }
                var isFeatureActive = await ComputeFeatureActivationStatus(lazyFeatures);
                _featureActivationResultCache.Add(featureSpec, isFeatureActive);
            }
            return _featureActivationResultCache.TryGetValue(featureSpec, out var result) && result;
        }

        private IList<AsyncLazy<IFeature>> ComputeLazyFeatures(FeatureSpec featureSpec)
        {
            var lazyFeatures = new List<AsyncLazy<IFeature>>();
            foreach (var featureActivator in _featureActivators)
            {
                lazyFeatures.Add(new AsyncLazy<IFeature>(async () => await featureActivator.GetFeatureAsync(featureSpec)));
            }
            return lazyFeatures;
        }

        internal async Task<bool> ComputeFeatureActivationStatus(IList<AsyncLazy<IFeature>> lazyFeatures)
        {
            var featureContext = await _featureContextAccessor.GetCurrentFeatureContextAsync();
            var isFeatureActive = false;
            foreach (var lazyFeature in lazyFeatures)
            {
                var feature = await lazyFeature ?? NotSetFeature.Instance;
                var featureActivationStatus = await feature.GetStatusAsync(featureContext);
                switch (featureActivationStatus)
                {
                    case FeatureActivationStatus.Inactive:
                        return false;
                    case FeatureActivationStatus.Active:
                        isFeatureActive = true;
                        break;
                    case FeatureActivationStatus.NotSet:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            return isFeatureActive;
        }
        
        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }
            _featureContextAccessor.Dispose();

            _isDisposed = true;
        }
    }
}
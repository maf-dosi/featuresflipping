using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MAF.FeaturesFlipping.Extensibility.Activators;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace MAF.FeaturesFlipping
{
    public sealed class FeatureService : IFeatureService, IDisposable
    {
        private readonly Dictionary<FeatureSpec, bool> _featureActivationResultCache =
            new Dictionary<FeatureSpec, bool>();

        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<FeatureSpec> _logger;
        private readonly IEnumerable<IFeatureActivator> _featureActivators;
        private readonly IFeatureContextAccessor _featureContextAccessor;
        private bool _isDisposed;

        public FeatureService(IMemoryCache memoryCache, ILogger<FeatureSpec> logger, IFeatureContextAccessor featureContextAccessor, IEnumerable<IFeatureActivator> featureActivators)
        {
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _featureContextAccessor = featureContextAccessor ?? throw new ArgumentNullException(nameof(featureContextAccessor));
            _featureActivators = featureActivators ?? throw new ArgumentNullException(nameof(featureActivators));
        }

        public async Task<bool> IsFeatureActiveAsync(FeatureSpec featureSpec)
        {
            using (_logger.CreateScopeWithFeatureSpec(featureSpec))
            {
                _logger.StartComputationOfFeatureActivationStatus();
                if (!_featureActivationResultCache.ContainsKey(featureSpec))
                {
                    var cacheKey = $"{nameof(FeatureService)}¤{featureSpec}";
                    _logger.SearchFeatureFromMemoryCache(cacheKey);

                    if (!_memoryCache.TryGetValue(cacheKey, out IList<AsyncLazy<IFeature>> lazyFeatures))
                    {
                        lazyFeatures = ComputeLazyFeatures(featureSpec);
                        _logger.PutFeatureInMemoryCache(cacheKey);
                        _memoryCache.Set(cacheKey, lazyFeatures);
                    }
                    else
                    {
                        _logger.FoundFeatureInMemoryCache();
                    }
                    var isFeatureActive = await ComputeFeatureActivationStatus(lazyFeatures);
                    _logger.PutActivationStatusInCache();
                    _featureActivationResultCache.Add(featureSpec, isFeatureActive);
                }
                else
                {
                    _logger.GetValueFromScopedCache();
                }
                if (_featureActivationResultCache.TryGetValue(featureSpec, out var result))
                {
                    _logger.ActivationStatusForFeatureIs(result);
                    return result;
                }
                _logger.UnableToFindAndComputeFeature();
                return false;
            }
        }

        private IList<AsyncLazy<IFeature>> ComputeLazyFeatures(FeatureSpec featureSpec)
        {
            _logger.StartGettingAllFeatures(_featureActivators.Count());
            var lazyFeatures = new List<AsyncLazy<IFeature>>();
            foreach (var featureActivator in _featureActivators)
            {
                lazyFeatures.Add(new AsyncLazy<IFeature>(async () => await featureActivator.GetFeatureAsync(featureSpec)));
            }
            _logger.EndGettingAllFeatures();
            return lazyFeatures;
        }

        internal async Task<bool> ComputeFeatureActivationStatus(IList<AsyncLazy<IFeature>> lazyFeatures)
        {
            _logger.StartComputingFeatureActivationStatus();

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
            _logger.FeatureActivationStatusComputed(isFeatureActive);
            return isFeatureActive;
        }

        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }
            _logger.DisposeCurrentFeatureContext();
            _featureContextAccessor.Dispose();
            _isDisposed = true;
        }
    }
}
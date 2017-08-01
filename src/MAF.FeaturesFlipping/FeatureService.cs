using System.Collections.Generic;
using System.Threading.Tasks;
using MAF.FeaturesFlipping.Extensibility.Activators;

namespace MAF.FeaturesFlipping
{
    public sealed class FeatureService : IFeatureService
    {
        private readonly Dictionary<FeatureSpec, bool> _featureActivationResultCache =
            new Dictionary<FeatureSpec, bool>();
        private readonly IEnumerable<IFeatureActivator> _featureActivators;
        private readonly IFeatureContextAccessor _featureContextAccessor;

        public FeatureService(IEnumerable<IFeatureActivator> featureActivators,
            IFeatureContextAccessor featureContextAccessor)
        {
            _featureActivators = featureActivators;
            _featureContextAccessor = featureContextAccessor;
        }

        public async Task<bool> IsFeatureActiveAsync(FeatureSpec featureSpec)
        {
            if (!_featureActivationResultCache.ContainsKey(featureSpec))
            {
                var isFeatureActive = await ComputeIsFeatureActiveAsync(featureSpec);
                _featureActivationResultCache.Add(featureSpec, isFeatureActive);
            }
            return _featureActivationResultCache.TryGetValue(featureSpec, out var result) && result;
        }

        private async Task<bool> ComputeIsFeatureActiveAsync(FeatureSpec featureSpec)
        {
            var featureContext = _featureContextAccessor.GetCurrentFeatureContext();
            var isFeatureActive = false;
            try
            {
                foreach (var featureActivator in _featureActivators)
                {
                    var feature = await featureActivator.GetFeatureAsync(featureSpec) ?? NotSetFeature.Instance;
                    var featureActivationStatus = await feature.GetStatusAsync(featureContext);
                    switch (featureActivationStatus)
                    {
                        case FeatureActivationStatus.Inactive:
                            return false;
                        case FeatureActivationStatus.Active:
                            isFeatureActive = true;
                            break;
                    }
                }
            }
            finally
            {
                _featureContextAccessor.DisposeFeatureContext(featureContext);
            }
            return isFeatureActive;
        }
    }
}
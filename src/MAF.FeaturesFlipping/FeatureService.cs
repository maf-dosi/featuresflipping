using System.Collections.Generic;
using System.Threading.Tasks;
using MAF.FeaturesFlipping.Extensibility.Activators;

namespace MAF.FeaturesFlipping
{
    public sealed class FeatureService : IFeatureService
    {
        private readonly IEnumerable<IFeatureActivator> _featureActivators;
        private readonly IFeatureContextAccessor _featureContextAccessor;

        public FeatureService(IEnumerable<IFeatureActivator> featureActivators,
            IFeatureContextAccessor featureContextAccessor)
        {
            _featureActivators = featureActivators;
            _featureContextAccessor = featureContextAccessor;
        }

        public async Task<bool> IsFeatureActiveAsync(IFeatureName featureName)
        {
            var featureContext = _featureContextAccessor.GetCurrentFeatureContext();
            var isFeatureActive = false;
            foreach (var featureActivator in _featureActivators)
            {
                var feature = await featureActivator.GetFeatureAsync(featureName) ?? NotSetFeature.Instance;
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
            _featureContextAccessor.DisposeFeatureContext(featureContext);
            return isFeatureActive;
        }
    }
}
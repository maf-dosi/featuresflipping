using System.Collections.Generic;
using MAF.FeaturesFlipping.Extensibility.Activators;
using MAF.FeaturesFlipping.Extensibility.FeatureContext;

namespace MAF.FeaturesFlipping
{
    public sealed class FeatureContextAccessor : IFeatureContextAccessor
    {
        private readonly IEnumerable<IFeatureContextPartFactory> _featureContextPartFactories;

        public FeatureContextAccessor(IEnumerable<IFeatureContextPartFactory> featureContextPartFactories)
        {
            _featureContextPartFactories = featureContextPartFactories;
        }

        public IFeatureContext GetCurrentFeatureContext()
        {
            var featureContext = new FeatureContext();
            foreach (var featureContextPartFactory in _featureContextPartFactories)
            {
                featureContextPartFactory.AddFeatureContextPart(featureContext);
            }
            return featureContext;
        }

        public void DisposeFeatureContext(IFeatureContext featureContext)
        {
            foreach (var featureContextPartFactory in _featureContextPartFactories)
            {
                featureContextPartFactory.ReleaseFeatureContextPart(featureContext);
            }
        }
    }
}
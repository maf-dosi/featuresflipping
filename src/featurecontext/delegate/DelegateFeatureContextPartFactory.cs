using System;
using System.Threading.Tasks;
using MAF.FeaturesFlipping.Extensibility.Activators;
using MAF.FeaturesFlipping.Extensibility.FeatureContext;

namespace MAF.FeaturesFlipping.FeatureContext.Delegate
{
    public sealed class DelegateFeatureContextPartFactory : IFeatureContextPartFactory
    {
        private readonly Func<IFeatureContext, Task> _addFeatureContextPart;
        private readonly Action<IFeatureContext> _releaseFeatureContextPart;

        public DelegateFeatureContextPartFactory(
            Func<IFeatureContext, Task> addFeatureContextPart,
            Action<IFeatureContext> releaseFeatureContextPart)
        {
            _addFeatureContextPart = addFeatureContextPart ??  throw new ArgumentNullException(nameof(addFeatureContextPart));
            _releaseFeatureContextPart = releaseFeatureContextPart ?? throw new ArgumentNullException(nameof(releaseFeatureContextPart));
        }

        public Task AddFeatureContextPartAsync(IFeatureContext featureContext)
        {
            return _addFeatureContextPart(featureContext);
        }

        public void ReleaseFeatureContextPart(IFeatureContext featureContext)
        {
            _releaseFeatureContextPart(featureContext);
        }
    }
}
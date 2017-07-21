using MAF.FeaturesFlipping.Extensibility.Activators;

namespace MAF.FeaturesFlipping
{
    public interface IFeatureContextAccessor
    {
        IFeatureContext GetCurrentFeatureContext();
        void DisposeFeatureContext(IFeatureContext featureContext);
    }
}
using MAF.FeaturesFlipping.Extensibility.Activators;

namespace MAF.FeaturesFlipping.Extensibility.FeatureContext
{
    public interface IFeatureContextPartFactory
    {
        void AddFeatureContextPart(IFeatureContext featureContext);
        void ReleaseFeatureContextPart(IFeatureContext featureContext);
    }
}
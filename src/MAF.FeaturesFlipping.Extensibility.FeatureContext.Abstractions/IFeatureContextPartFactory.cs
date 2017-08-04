using System.Threading.Tasks;
using MAF.FeaturesFlipping.Extensibility.Activators;

namespace MAF.FeaturesFlipping.Extensibility.FeatureContext
{
    public interface IFeatureContextPartFactory
    {
        Task AddFeatureContextPartAsync(IFeatureContext featureContext);
        void ReleaseFeatureContextPart(IFeatureContext featureContext);
    }
}
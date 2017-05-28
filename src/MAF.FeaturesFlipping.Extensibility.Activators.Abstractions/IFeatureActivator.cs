using System.Threading.Tasks;
using MAF.Extensions.FeaturesFlipping;

namespace MAF.FeaturesFlipping.Extensibility.Activators
{
    public interface IFeatureActivator
    {
        Task<FeatureActivationStatus> GetFeatureStatus(IFeatureName featureName, IFeatureContext featureContext);
    }
}
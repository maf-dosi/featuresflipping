using System.Threading.Tasks;

namespace MAF.FeaturesFlipping.Extensibility.Activators
{
    public interface IFeature
    {
        Task<FeatureActivationStatus> GetStatusAsync(IFeatureContext featureContext);
    }
}
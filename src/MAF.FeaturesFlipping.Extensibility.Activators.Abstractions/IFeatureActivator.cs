using System.Threading.Tasks;

namespace MAF.FeaturesFlipping.Extensibility.Activators
{
    public interface IFeatureActivator
    {
        Task<IFeature> GetFeatureAsync(FeatureSpec featureSpec);
    }
}
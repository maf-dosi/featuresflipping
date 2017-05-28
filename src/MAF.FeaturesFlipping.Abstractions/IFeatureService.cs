using System.Threading.Tasks;

namespace MAF.Extensions.FeaturesFlipping
{
    public interface IFeatureService
    {
        Task<bool> IsFeatureActiveAsync(IFeatureName featureName);
    }
}
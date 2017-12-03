using System.Threading.Tasks;
using MAF.FeaturesFlipping.Extensibility.Activators;

namespace MAF.FeaturesFlipping.Activators.EntityFrameworkCore.Specific
{
    public class SpecificEntityFrameworkCoreActivator<TOtherColumn> : IFeatureActivator
    {
        public Task<IFeature> GetFeatureAsync(FeatureSpec featureSpec)
        {
            var specificFeature = new SpecificFeature<TOtherColumn>(featureSpec);
            return Task.FromResult<IFeature>(specificFeature);
        }
    }
}
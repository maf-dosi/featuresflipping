using System.Linq;
using System.Threading.Tasks;
using MAF.FeaturesFlipping.Extensibility.Activators;

namespace MAF.FeaturesFlipping.Activators.EntityFrameworkCore.Specific
{
    public class SpecificEntityFrameworkCoreActivator<TOtherColumn> : IFeatureActivator
    {
        private readonly SpecificFeatureDbContext<TOtherColumn> _specificFeatureDbContext;

        public SpecificEntityFrameworkCoreActivator(SpecificFeatureDbContext<TOtherColumn> specificFeatureDbContext)
        {
            _specificFeatureDbContext = specificFeatureDbContext;
        }

        public Task<IFeature> GetFeatureAsync(FeatureName featureName)
        {
            var specificFeatureQuery = _specificFeatureDbContext.Features.Where(
                feature => feature.Application == featureName.Application
                               && feature.Scope == featureName.Scope
                               && feature.Feature == featureName.Feature);
            
            var specificFeature = new SpecificFeature<TOtherColumn>(specificFeatureQuery, _specificFeatureDbContext.SpecificConfiguration);
            return Task.FromResult<IFeature>(specificFeature);
        }
    }
}
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

        public Task<IFeature> GetFeatureAsync(FeatureSpec featureSpec)
        {
            var specificFeatureQuery = _specificFeatureDbContext.Features.Where(
                feature => feature.Application == featureSpec.Application
                               && feature.Scope == featureSpec.Scope
                               && feature.FeatureName == featureSpec.FeatureName);
            
            var specificFeature = new SpecificFeature<TOtherColumn>(specificFeatureQuery, _specificFeatureDbContext.SpecificConfiguration);
            return Task.FromResult<IFeature>(specificFeature);
        }
    }
}
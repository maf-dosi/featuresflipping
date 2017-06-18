using System.Threading.Tasks;
using MAF.FeaturesFlipping.Extensibility.Activators;
using Microsoft.EntityFrameworkCore;

namespace MAF.FeaturesFlipping.Activators.EntityFrameworkCore.Specific
{
    public class SpecificEntityFrameworkCoreActivator<TOtherColumn> : IFeatureActivator
    {
        private readonly SpecificFeatureDbContext<TOtherColumn> _specificFeatureDbContext;

        public SpecificEntityFrameworkCoreActivator(SpecificFeatureDbContext<TOtherColumn> specificFeatureDbContext)
        {
            _specificFeatureDbContext = specificFeatureDbContext;
        }

        public async Task<IFeature> GetFeatureAsync(IFeatureName featureName)
        {
            var globalFeatureEntity = await _specificFeatureDbContext.Features.FirstOrDefaultAsync(
                feature => feature.Application == featureName.Application && feature.Scope ==
                           featureName.Scope && feature.Feature == featureName.Feature);
            var globalFeature = new SpecificFeature<TOtherColumn>(globalFeatureEntity, _specificFeatureDbContext.SpecificConfiguration);
            return globalFeature;
        }
    }
}
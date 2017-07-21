using System.Threading.Tasks;
using MAF.FeaturesFlipping.Extensibility.Activators;
using Microsoft.EntityFrameworkCore;

namespace MAF.FeaturesFlipping.Activators.EntityFrameworkCore.Global
{
    public class GlobalEntityFrameworkCoreActivator : IFeatureActivator
    {
        private readonly GlobalFeatureDbContext _globalFeatureDbContext;

        public GlobalEntityFrameworkCoreActivator(GlobalFeatureDbContext globalFeatureDbContext)
        {
            _globalFeatureDbContext = globalFeatureDbContext;
        }

        public async Task<IFeature> GetFeatureAsync(FeatureName featureName)
        {
            var globalFeatureEntity = await _globalFeatureDbContext.Features.FirstOrDefaultAsync(
                feature => feature.Application == featureName.Application && feature.Scope ==
                           featureName.Scope && feature.Feature == featureName.Feature);
            var featureFromEntity = new FeatureFromEntity(globalFeatureEntity);
            return featureFromEntity;
        }
    }
}
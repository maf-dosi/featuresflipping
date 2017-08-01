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

        public async Task<IFeature> GetFeatureAsync(FeatureSpec featureSpec)
        {
            var globalFeatureEntity = await _globalFeatureDbContext.Features.FirstOrDefaultAsync(
                feature => feature.Application == featureSpec.Application && feature.Scope ==
                           featureSpec.Scope && feature.FeatureName == featureSpec.FeatureName);
            var featureFromEntity = new FeatureFromEntity(globalFeatureEntity);
            return featureFromEntity;
        }
    }
}
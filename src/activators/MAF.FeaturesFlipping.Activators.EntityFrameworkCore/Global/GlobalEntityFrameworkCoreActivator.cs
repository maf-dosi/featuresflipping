using System.Threading.Tasks;
using MAF.FeaturesFlipping.Extensibility.Activators;
using Microsoft.EntityFrameworkCore;

namespace MAF.FeaturesFlipping.Activators.EntityFrameworkCore.Global
{
    public class GlobalEntityFrameworkCoreActivator : IFeatureActivator
    {
        private readonly GlobalDbContextConfigurer _globalDbContextConfigurer;

        public GlobalEntityFrameworkCoreActivator(GlobalDbContextConfigurer globalDbContextConfigurer)
        {
            _globalDbContextConfigurer = globalDbContextConfigurer;
        }

        public async Task<IFeature> GetFeatureAsync(IFeatureName featureName)
        {
            using (var globalFeatureDbContext = new GlobalFeatureDbContext(_globalDbContextConfigurer))
            {
                var globalFeatureEntity = await globalFeatureDbContext.Features.FirstOrDefaultAsync(
                    feature => feature.Application == featureName.Application && feature.Scope ==
                               featureName.Scope && feature.Feature == featureName.Feature);
                var globalFeature = new GlobalFeature(globalFeatureEntity);
                return globalFeature;
            }
        }
    }
}
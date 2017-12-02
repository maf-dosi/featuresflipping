using System.Threading.Tasks;
using MAF.FeaturesFlipping.Extensibility.Activators;

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
            var featureFromEntity = await _globalFeatureDbContext.GetGlobalFeatureEntity(featureSpec);
            return featureFromEntity;
        }
    }
}
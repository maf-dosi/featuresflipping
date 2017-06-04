using System.Threading.Tasks;
using MAF.FeaturesFlipping.Extensibility.Activators;

namespace MAF.FeaturesFlipping.Activators.EntityFrameworkCore.Global
{
    internal class GlobalFeature : IFeature
    {
        private readonly FeatureActivationStatus _featureActivationStatus;

        public GlobalFeature(GlobalFeatureEntity globalFeatureEntity)
        {
            if (globalFeatureEntity == null || globalFeatureEntity.IsActive == null)
            {
                _featureActivationStatus = FeatureActivationStatus.NotSet;
            }
            else
            {
                _featureActivationStatus = (bool)globalFeatureEntity.IsActive
                    ? FeatureActivationStatus.Active
                    : FeatureActivationStatus.Inactive;
            }
        }

        public Task<FeatureActivationStatus> GetStatusAsync(IFeatureContext featureContext)
        {
            return Task.FromResult(_featureActivationStatus);
        }
    }
}
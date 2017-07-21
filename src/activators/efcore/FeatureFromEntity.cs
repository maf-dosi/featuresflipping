using System.Threading.Tasks;
using MAF.FeaturesFlipping.Extensibility.Activators;

namespace MAF.FeaturesFlipping.Activators.EntityFrameworkCore
{
    internal class FeatureFromEntity : IFeature
    {
        private readonly FeatureActivationStatus _featureActivationStatus;

        public FeatureFromEntity(IFeatureEntity featureEntity)
        {
            if (featureEntity == null || featureEntity.IsActive == null)
            {
                _featureActivationStatus = FeatureActivationStatus.NotSet;
            }
            else
            {
                _featureActivationStatus = (bool)featureEntity.IsActive
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
using System.Threading.Tasks;
using MAF.FeaturesFlipping.Extensibility.Activators;
using Microsoft.Extensions.Logging;

namespace MAF.FeaturesFlipping.Activators.EntityFrameworkCore
{
    internal class FeatureFromEntity : IFeature
    {
        private readonly FeatureActivationStatus _featureActivationStatus;

        public FeatureFromEntity(IFeatureEntity featureEntity, ILogger logger)
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
            logger.FeatureActivationStatusForFeatureFromEntityIs(_featureActivationStatus);
        }

        public Task<FeatureActivationStatus> GetStatusAsync(IFeatureContext featureContext)
        {
            return Task.FromResult(_featureActivationStatus);
        }
    }
}
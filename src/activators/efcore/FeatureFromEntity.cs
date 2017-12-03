using System.Threading.Tasks;
using MAF.FeaturesFlipping.Extensibility.Activators;
using Microsoft.Extensions.Logging;

namespace MAF.FeaturesFlipping.Activators.EntityFrameworkCore
{
    internal class FeatureFromEntity<TFeatureEntity> : IFeature
        where TFeatureEntity : IFeatureEntity
    {
        private readonly FeatureActivationStatus _featureActivationStatus;

        public FeatureFromEntity(TFeatureEntity featureEntity, ILogger logger)
        {
            if (featureEntity == null)
            {
                logger.NoFeatureEntityFound(typeof(TFeatureEntity));
                _featureActivationStatus = FeatureActivationStatus.NotSet;
            }
            else if (featureEntity.IsActive == null)
            {
                logger.FeatureEntityIsNotSet(featureEntity.FeatureId);
                _featureActivationStatus = FeatureActivationStatus.NotSet;
            }
            else
            {
                logger.FeatureEntityIsActiveValue(featureEntity.FeatureId, featureEntity.IsActive.Value);
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
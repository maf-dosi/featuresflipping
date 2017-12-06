using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace MAF.FeaturesFlipping.Extensibility.Activators
{
    public sealed class NotSetFeature : IFeature
    {
        private NotSetFeature()
        {
        }

        public static NotSetFeature Instance { get; } = new NotSetFeature();

        public Task<FeatureActivationStatus> GetStatusAsync(IFeatureContext featureContext)
        {
            var logger = (ILogger<FeatureSpec>)featureContext.FeaturesServices.GetService(typeof(ILogger<FeatureSpec>));
            logger.GetActivationStatus();
            return Task.FromResult(FeatureActivationStatus.NotSet);
        }
    }
}
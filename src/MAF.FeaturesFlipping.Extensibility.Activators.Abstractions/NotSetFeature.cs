using System.Threading.Tasks;

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
            return Task.FromResult(FeatureActivationStatus.NotSet);
        }
    }
}
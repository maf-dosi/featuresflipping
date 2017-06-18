using System.Threading.Tasks;
using MAF.FeaturesFlipping.Extensibility.Activators;

namespace MAF.FeaturesFlipping.Activators.EntityFrameworkCore.Specific
{
    internal class SpecificFeature<TOtherColumn> : IFeature
    {
        private readonly SpecificFeatureEntity<TOtherColumn> _specificFeatureEntity;
        private readonly SpecificDbContextConfiguration<TOtherColumn> _specificDbContextConfiguration;

        public SpecificFeature(SpecificFeatureEntity<TOtherColumn> specificFeatureEntity, SpecificDbContextConfiguration<TOtherColumn> specificDbContextConfiguration)
        {
            _specificFeatureEntity = specificFeatureEntity;
            _specificDbContextConfiguration = specificDbContextConfiguration;
        }

        public Task<FeatureActivationStatus> GetStatusAsync(IFeatureContext featureContext)
        {
            return _specificDbContextConfiguration.FilterFeatureActivationStatusAsync(_specificFeatureEntity, featureContext);
        }
    }
}
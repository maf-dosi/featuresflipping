using System;
using System.Linq;
using System.Threading.Tasks;
using MAF.FeaturesFlipping.Extensibility.Activators;
using Microsoft.EntityFrameworkCore;

namespace MAF.FeaturesFlipping.Activators.EntityFrameworkCore.Specific
{
    internal class SpecificFeature<TOtherColumn> : IFeature
    {
        private readonly SpecificDbContextConfiguration<TOtherColumn> _specificDbContextConfiguration;
        private readonly IQueryable<SpecificFeatureEntity<TOtherColumn>> _specificFeatureQuery;

        public SpecificFeature(IQueryable<SpecificFeatureEntity<TOtherColumn>> specificFeatureQuery,
            SpecificDbContextConfiguration<TOtherColumn> specificDbContextConfiguration)
        {
            _specificFeatureQuery = specificFeatureQuery ??
                                    throw new ArgumentNullException(nameof(specificFeatureQuery));
            _specificDbContextConfiguration = specificDbContextConfiguration ??
                                              throw new ArgumentNullException(nameof(specificDbContextConfiguration));
        }

        public async Task<FeatureActivationStatus> GetStatusAsync(IFeatureContext featureContext)
        {
            var specificFeature = await _specificFeatureQuery
                .Where(_specificDbContextConfiguration.FilterFeatureWithScope(featureContext))
                .SingleOrDefaultAsync();

            var featureFromEntity = new FeatureFromEntity(specificFeature);

            return await featureFromEntity.GetStatusAsync(featureContext);
        }
    }
}
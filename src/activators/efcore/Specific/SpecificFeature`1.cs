using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MAF.FeaturesFlipping.Extensibility.Activators;
using Microsoft.Extensions.DependencyInjection;

namespace MAF.FeaturesFlipping.Activators.EntityFrameworkCore.Specific
{
    internal class SpecificFeature<TOtherColumn> : IFeature
    {
        private readonly Expression<Func<SpecificFeatureEntity<TOtherColumn>, bool>> _specificFeatureQuery;

        public SpecificFeature(FeatureSpec featureSpec)
        {
            _specificFeatureQuery = feature => feature.Application == featureSpec.Application
                           && feature.Scope == featureSpec.Scope
                           && feature.FeatureName == featureSpec.FeatureName;
        }

        public async Task<FeatureActivationStatus> GetStatusAsync(IFeatureContext featureContext)
        {
            var specificFeatureDbContext = featureContext.FeaturesServices.GetService<SpecificFeatureDbContext<TOtherColumn>>();
            var featureFromEntity = await specificFeatureDbContext.LoadSpecificFeatureEntity(_specificFeatureQuery, featureContext);
            return await featureFromEntity.GetStatusAsync(featureContext);
        }
    }
}
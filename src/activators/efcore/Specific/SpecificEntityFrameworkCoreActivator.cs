using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MAF.FeaturesFlipping.Extensibility.Activators;

namespace MAF.FeaturesFlipping.Activators.EntityFrameworkCore.Specific
{
    public class SpecificEntityFrameworkCoreActivator<TOtherColumn> : IFeatureActivator
    {
        public Task<IFeature> GetFeatureAsync(FeatureSpec featureSpec)
        {
            Expression<Func<SpecificFeatureEntity<TOtherColumn>, bool>> specificFeatureQuery =
                feature => feature.Application == featureSpec.Application
                           && feature.Scope == featureSpec.Scope
                           && feature.FeatureName == featureSpec.FeatureName;

            var specificFeature =
                new SpecificFeature<TOtherColumn>(specificFeatureQuery);
            return Task.FromResult<IFeature>(specificFeature);
        }
    }
}
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MAF.FeaturesFlipping.Extensibility.Activators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;

namespace MAF.FeaturesFlipping.Activators.EntityFrameworkCore.Specific
{
    internal class SpecificFeature<TOtherColumn> : IFeature
    {
        private readonly Expression<Func<SpecificFeatureEntity<TOtherColumn>, bool>> _genericFeatureQuery;

        public SpecificFeature(Expression<Func<SpecificFeatureEntity<TOtherColumn>, bool>> genericFeatureQuery)
        {
            _genericFeatureQuery = genericFeatureQuery ??
                                    throw new ArgumentNullException(nameof(genericFeatureQuery));
        }

        public async Task<FeatureActivationStatus> GetStatusAsync(IFeatureContext featureContext)
        {
            var specificFeatureDbContext = featureContext.FeaturesServices.GetService<SpecificFeatureDbContext<TOtherColumn>>();
            var specificDbContextConfiguration = featureContext.FeaturesServices.GetService<SpecificDbContextConfiguration<TOtherColumn>>();
            
            var specificFeature = await specificFeatureDbContext.Features.Where(_genericFeatureQuery)
                .Where(specificDbContextConfiguration.FilterFeatureWithScope(featureContext))
                .SingleOrDefaultAsync();

            var featureFromEntity = new FeatureFromEntity(specificFeature, new NullLogger<FeatureSpec>());

            return await featureFromEntity.GetStatusAsync(featureContext);
        }
    }
}
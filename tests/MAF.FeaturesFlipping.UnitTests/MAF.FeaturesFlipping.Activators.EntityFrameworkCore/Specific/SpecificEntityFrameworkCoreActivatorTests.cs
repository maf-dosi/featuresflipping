using System;
using System.Linq;
using System.Linq.Expressions;
using MAF.FeaturesFlipping.Extensibility.Activators;
using Microsoft.EntityFrameworkCore;

namespace MAF.FeaturesFlipping.Activators.EntityFrameworkCore.Specific
{
    public partial class SpecificEntityFrameworkCoreActivatorTests
    {
        private static (SpecificFeatureDbContext<TOtherColumn> Context, SpecificDbContextConfiguration<TOtherColumn> Configuration) CreateNewContext<TOtherColumn>(string databaseName, string otherColumnName,
            Func<IFeatureContext, Expression<Func<SpecificFeatureEntity<TOtherColumn>, bool>>> specificFeatureFilterWithContext)
        {
            var configuration = new SpecificDbContextConfiguration<TOtherColumn>(
                builder => builder.UseInMemoryDatabase(databaseName), otherColumnName,
                specificFeatureFilterWithContext);
            var context = new SpecificFeatureDbContext<TOtherColumn>(configuration);
            return (context, configuration);
        }

        private static void PopulateContext(SpecificFeatureDbContext<string> context)
        {
            context.Features.RemoveRange(context.Features.ToList());
            context.Features.AddRange(
                Enumerable.Range(1, 10).Select(i => new SpecificFeatureEntity<string>
                {
                    Application = "App" + i,
                    Scope = "Scope" + i,
                    FeatureName = "FeatureName" + i,
                    OtherColumn = "OtherColumn" + i,
                    IsActive = i % 3 == 1 ? true : (i % 3 == 2 ? false : (bool?)null)
                }));
            context.SaveChanges();
        }

        private static (SpecificFeatureDbContext<string> Context, SpecificDbContextConfiguration<string> Configuration) PopulateNewContext(string databaseName, string otherColumnName,
            Func<IFeatureContext, Expression<Func<SpecificFeatureEntity<string>, bool>>> specificFeatureFilterWithContext)
        {
            var contextAndConfiguration = CreateNewContext(databaseName, otherColumnName, specificFeatureFilterWithContext);
            PopulateContext(contextAndConfiguration.Context);
            return contextAndConfiguration;
        }
    }
}
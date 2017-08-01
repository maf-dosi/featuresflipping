using System;
using System.Linq;
using System.Linq.Expressions;
using MAF.FeaturesFlipping.Activators.EntityFrameworkCore.Specific;
using MAF.FeaturesFlipping.Extensibility.Activators;
using Microsoft.EntityFrameworkCore;

namespace MAF.FeaturesFlipping.Activators.EntityFrameworkCore.UnitTests.Specific
{
    public partial class SpecificEntityFrameworkCoreActivatorTests
    {
        private static SpecificFeatureDbContext<TOtherColumn> CreateNewContext<TOtherColumn>(string databaseName, string otherColumnName,
            Func<IFeatureContext, Expression<Func<SpecificFeatureEntity<TOtherColumn>, bool>>> specificFeatureFilterWithContext)
        {
            var context = new SpecificFeatureDbContext<TOtherColumn>(new SpecificDbContextConfiguration<TOtherColumn>(
                builder => builder.UseInMemoryDatabase(databaseName), otherColumnName, specificFeatureFilterWithContext));
            return context;
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

        private static SpecificFeatureDbContext<string> PopulateNewContext(string databaseName, string otherColumnName,
            Func<IFeatureContext, Expression<Func<SpecificFeatureEntity<string>, bool>>> specificFeatureFilterWithContext)
        {
            var context = CreateNewContext(databaseName, otherColumnName, specificFeatureFilterWithContext);
            PopulateContext(context);
            return context;
        }
    }
}
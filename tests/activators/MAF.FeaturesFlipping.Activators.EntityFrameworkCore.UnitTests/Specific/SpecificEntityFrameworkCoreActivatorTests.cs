using System;
using System.Linq;
using System.Threading.Tasks;
using MAF.FeaturesFlipping.Activators.EntityFrameworkCore.Specific;
using MAF.FeaturesFlipping.Extensibility.Activators;
using Microsoft.EntityFrameworkCore;

namespace MAF.FeaturesFlipping.Activators.EntityFrameworkCore.UnitTests.Specific
{
    public partial class SpecificEntityFrameworkCoreActivatorTests
    {
        private static SpecificFeatureDbContext<TOtherColumn> CreateNewContext<TOtherColumn>(string databaseName, string otherColumnName,
            Func<SpecificFeatureEntity<TOtherColumn>, IFeatureContext, Task<FeatureActivationStatus>> filterSpecificFeature)
        {
            var context = new SpecificFeatureDbContext<TOtherColumn>(new SpecificDbContextConfiguration<TOtherColumn>(
                builder => builder.UseInMemoryDatabase(databaseName), otherColumnName, filterSpecificFeature));
            return context;
        }

        private static void PopulateContext(SpecificFeatureDbContext<string> context)
        {
            context.Features.AddRange(
                Enumerable.Range(1, 10).Select(i => new SpecificFeatureEntity<string>
                {
                    Application = "App" + i,
                    Scope = "Scope" + i,
                    Feature = "Feature" + i,
                    OtherColumn = "OtherComunm" + i,
                    IsActive = i % 3 == 0
                }));
            context.SaveChanges();
        }

        private static SpecificFeatureDbContext<string> PopulateNewContext(string databaseName, string otherColumnName,
            Func<SpecificFeatureEntity<string>, IFeatureContext, Task<FeatureActivationStatus>> filterSpecificFeature)
        {
            var context = CreateNewContext<string>(databaseName, otherColumnName, filterSpecificFeature);
            PopulateContext(context);
            return context;
        }
    }
}
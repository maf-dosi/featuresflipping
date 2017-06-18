using System.Linq;
using MAF.FeaturesFlipping.Activators.EntityFrameworkCore.Specific;
using Microsoft.EntityFrameworkCore;

namespace MAF.FeaturesFlipping.Activators.EntityFrameworkCore.UnitTests.Specific
{
    public partial class SpecificEntityFrameworkCoreActivatorTests
    {
        private static SpecificFeatureDbContext CreateNewContext(string databaseName)
        {
            var context = new SpecificFeatureDbContext(new SpecificDbContextConfiguration(
                builder => builder.UseInMemoryDatabase(databaseName)));
            return context;
        }

        private static void PopulateContext(SpecificFeatureDbContext context)
        {
            context.Features.AddRange(
                Enumerable.Range(1, 10).Select(i => new SpecificFeatureEntity
                {
                    Application = "App" + i,
                    Scope = "Scope" + i,
                    Feature = "Feature" + i,
                    IsActive = i % 3 == 0
                }));
            context.SaveChanges();
        }

        private static void PopulateNewContext(string databaseName)
        {
            var context = CreateNewContext(databaseName);
            PopulateContext(context);
        }
    }
}
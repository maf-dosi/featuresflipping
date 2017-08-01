using System.Linq;
using MAF.FeaturesFlipping.Activators.EntityFrameworkCore.Global;
using Microsoft.EntityFrameworkCore;

namespace MAF.FeaturesFlipping.Activators.EntityFrameworkCore.UnitTests.Global
{
    public partial class GlobalEntityFrameworkCoreActivatorTests
    {
        private static GlobalFeatureDbContext CreateNewContext(string databaseName)
        {
            var context = new GlobalFeatureDbContext(new GlobalDbContextConfiguration(
                builder => builder.UseInMemoryDatabase(databaseName)));
            return context;
        }

        private static void PopulateContext(GlobalFeatureDbContext context)
        {
            context.Features.RemoveRange(context.Features.ToList());
            context.Features.AddRange(
                Enumerable.Range(1, 10).Select(i => new GlobalFeatureEntity
                {
                    Application = "App" + i,
                    Scope = "Scope" + i,
                    FeatureName = "FeatureName" + i,
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
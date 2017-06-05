using MAF.FeaturesFlipping.Activators.EntityFrameworkCore.Global;
using MAF.FeaturesFlipping.Extensibility.Activators;
using Xunit;

namespace MAF.FeaturesFlipping.Activators.EntityFrameworkCore.UnitTests.Global
{
    public partial class GlobalEntityFrameworkCoreActivatorTests
    {
        [Trait("Category", "UnitTest")]
        public class GetFeatureAsync
        {
            [Fact]
            public async void Searching_A_Non_Existing_Feature_Return_A_NotSet_Feature()
            {
                // Arrange
                var databaseName = $"{nameof(GetFeatureAsync)}:{nameof(Searching_A_Non_Existing_Feature_Return_A_NotSet_Feature)}";
                PopulateNewContext(databaseName);
                var context = CreateNewContext(databaseName);
                var activator = new GlobalEntityFrameworkCoreActivator(context);

                // Act
                var feature = await activator.GetFeatureAsync(new FeatureName("", "", ""));

                // Assert
                Assert.NotNull(feature);
                var featureActivationStatus = await feature.GetStatusAsync(null);
                Assert.Equal(FeatureActivationStatus.NotSet, featureActivationStatus);
            }

            [Fact]
            public async void Searching_A_Active_Existing_Feature_Return_A_Active_Feature()
            {
                // Arrange
                var databaseName = $"{nameof(GetFeatureAsync)}:{nameof(Searching_A_Active_Existing_Feature_Return_A_Active_Feature)}";
                PopulateNewContext(databaseName);
                var context = CreateNewContext(databaseName);
                var activator = new GlobalEntityFrameworkCoreActivator(context);

                // Act
                var feature = await activator.GetFeatureAsync(new FeatureName("App3", "Scope3", "Feature3"));

                // Assert
                Assert.NotNull(feature);
                var featureActivationStatus = await feature.GetStatusAsync(null);
                Assert.Equal(FeatureActivationStatus.Active, featureActivationStatus);
            }
        }
    }
}

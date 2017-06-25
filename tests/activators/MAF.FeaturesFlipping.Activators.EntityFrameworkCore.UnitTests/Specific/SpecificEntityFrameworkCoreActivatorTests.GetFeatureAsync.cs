using System.Threading.Tasks;
using MAF.FeaturesFlipping.Activators.EntityFrameworkCore.Specific;
using MAF.FeaturesFlipping.Extensibility.Activators;
using Xunit;

namespace MAF.FeaturesFlipping.Activators.EntityFrameworkCore.UnitTests.Specific
{
    public partial class SpecificEntityFrameworkCoreActivatorTests
    {
        [Trait("Category", "UnitTest")]
        public class GetFeatureAsync
        {
            [Fact]
            public async void Searching_A_Non_Existing_Feature_Return_A_NotSet_Feature()
            {
                // Arrange
                var databaseName = $"{nameof(GetFeatureAsync)}:{nameof(Searching_A_Non_Existing_Feature_Return_A_NotSet_Feature)}";
                
                var context = PopulateNewContext(databaseName, "otherColumnName", (_, __) => Task.FromResult(FeatureActivationStatus.NotSet));
                var activator = new SpecificEntityFrameworkCoreActivator<string>(context);

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
                var context = PopulateNewContext(databaseName, "otherColumnName", (_, __) => Task.FromResult(FeatureActivationStatus.Active));
                var activator = new SpecificEntityFrameworkCoreActivator<string>(context);

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

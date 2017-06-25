using System.Threading.Tasks;
using MAF.FeaturesFlipping.Activators.EntityFrameworkCore.Specific;
using MAF.FeaturesFlipping.Extensibility.Activators;
using Xunit;

namespace MAF.FeaturesFlipping.Activators.EntityFrameworkCore.UnitTests.Specific
{
    public partial class SpecificFeatureTests
    {
        [Trait("Category", "UnitTest")]
        public class GetStatusAsync
        {
            [Fact]
            public void A_Null_SpecificFeatureEntity_Defines_A_NotSet_Status()
            {
                // Arrange
                var specificFeature = new SpecificFeature<object>(null, null);
                var expected = FeatureActivationStatus.NotSet;

                // Act
                var actual = specificFeature.GetStatusAsync(null).Result;

                // Assert
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void A_Null_SpecificDbContextConfiguration_Defines_A_NotSet_Status()
            {
                // Arrange
                var specificFeature = new SpecificFeature<object>(new SpecificFeatureEntity<object>(), null);
                var expected = FeatureActivationStatus.NotSet;

                // Act
                var actual = specificFeature.GetStatusAsync(null).Result;

                // Assert
                Assert.Equal(expected, actual);
            }

            [Theory]
            [InlineData(FeatureActivationStatus.NotSet)]
            [InlineData(FeatureActivationStatus.Active)]
            [InlineData(FeatureActivationStatus.Inactive)]
            public void A_SpecificFeatureEntity_Calls_SpecificDbContextConfiguration_To_Get_FeatureActivationStatus(FeatureActivationStatus featureActivationStatus)
            {
                // Arrange
                var entity = new SpecificFeatureEntity<object>();
                var configuration = new SpecificDbContextConfiguration<object>(_=>{}, "Other", (_, __) => Task.FromResult(featureActivationStatus));
                var specificFeature = new SpecificFeature<object>(entity, configuration);
                var expected = featureActivationStatus;

                // Act
                var actual = specificFeature.GetStatusAsync(null).Result;

                // Assert
                Assert.Equal(expected, actual);
            }
        }
    }
}
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
                var SpecificFeature = new SpecificFeature(null);
                var expected = FeatureActivationStatus.NotSet;

                // Act
                var actual = SpecificFeature.GetStatusAsync(null).Result;

                // Assert
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void A_SpecificFeatureEntity_With_Null_IsActive_Defines_A_NotSet_Status()
            {
                // Arrange
                var entity = new SpecificFeatureEntity();
                var SpecificFeature = new SpecificFeature(entity);
                var expected = FeatureActivationStatus.NotSet;

                // Act
                var actual = SpecificFeature.GetStatusAsync(null).Result;

                // Assert
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void A_SpecificFeatureEntity_With_True_IsActive_Defines_A_Active_Status()
            {
                // Arrange
                SpecificFeatureEntity entity = new SpecificFeatureEntity { IsActive = true };
                var SpecificFeature = new SpecificFeature(entity);
                var expected = FeatureActivationStatus.Active;

                // Act
                var actual = SpecificFeature.GetStatusAsync(null).Result;

                // Assert
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void A_SpecificFeatureEntity_With_False_IsActive_Defines_A_Inactive_Status()
            {
                // Arrange
                SpecificFeatureEntity entity = new SpecificFeatureEntity { IsActive = false };
                var SpecificFeature = new SpecificFeature(entity);
                var expected = FeatureActivationStatus.Inactive;

                // Act
                var actual = SpecificFeature.GetStatusAsync(null).Result;

                // Assert
                Assert.Equal(expected, actual);
            }
        }
    }
}
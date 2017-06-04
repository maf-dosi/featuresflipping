using MAF.FeaturesFlipping.Activators.EntityFrameworkCore.Global;
using MAF.FeaturesFlipping.Extensibility.Activators;
using Xunit;

namespace MAF.FeaturesFlipping.Activators.EntityFrameworkCore.UnitTests.Global
{
    public partial class GlobalFeatureTests
    {
        [Trait("Category", "UnitTest")]
        public class GetStatusAsync
        {
            [Fact]
            public void A_Null_GlobalFeatureEntity_Defines_A_NotSet_Status()
            {
                // Arrange
                var globalFeature = new GlobalFeature(null);
                var expected = FeatureActivationStatus.NotSet;

                // Act
                var actual = globalFeature.GetStatusAsync(null).Result;

                // Assert
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void A_GlobalFeatureEntity_With_Null_IsActive_Defines_A_NotSet_Status()
            {
                // Arrange
                var entity = new GlobalFeatureEntity();
                var globalFeature = new GlobalFeature(entity);
                var expected = FeatureActivationStatus.NotSet;

                // Act
                var actual = globalFeature.GetStatusAsync(null).Result;

                // Assert
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void A_GlobalFeatureEntity_With_True_IsActive_Defines_A_Active_Status()
            {
                // Arrange
                GlobalFeatureEntity entity = new GlobalFeatureEntity { IsActive = true };
                var globalFeature = new GlobalFeature(entity);
                var expected = FeatureActivationStatus.Active;

                // Act
                var actual = globalFeature.GetStatusAsync(null).Result;

                // Assert
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void A_GlobalFeatureEntity_With_False_IsActive_Defines_A_Inactive_Status()
            {
                // Arrange
                GlobalFeatureEntity entity = new GlobalFeatureEntity { IsActive = false };
                var globalFeature = new GlobalFeature(entity);
                var expected = FeatureActivationStatus.Inactive;

                // Act
                var actual = globalFeature.GetStatusAsync(null).Result;

                // Assert
                Assert.Equal(expected, actual);
            }
        }
    }
}
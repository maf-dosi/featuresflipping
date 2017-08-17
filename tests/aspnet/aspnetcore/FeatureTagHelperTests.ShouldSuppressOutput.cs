using Moq;
using Xunit;

namespace MAF.FeaturesFlipping.AspNetCore.UnitTests
{
    public partial class FeatureTagHelperTests
    {
        [Trait("Category", "UnitTest")]
        public class ShouldSuppressOutput
        {
            [Fact]
            public void Feature_Active_And_Inverse_Should_Suppress_Output()
            {
                // Arrange
                var featureServiceMock = new Mock<IFeatureService>();
                var tagHelper = new FeatureTagHelper(featureServiceMock.Object)
                {
                    Inverse = true
                };

                // Act
                var actual = tagHelper.ShouldSuppressOutput(true);

                // Assert
                Assert.True(actual);
            }

            [Fact]
            public void Feature_Active_And_Not_Inverse_Should_Not_Suppress_Output()
            {
                // Arrange
                var featureServiceMock = new Mock<IFeatureService>();
                var tagHelper = new FeatureTagHelper(featureServiceMock.Object)
                {
                    Inverse = false
                };

                // Act
                var actual = tagHelper.ShouldSuppressOutput(true);

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void Feature_Inactive_And_Inverse_Should_Not_Suppress_Output()
            {
                // Arrange
                var featureServiceMock = new Mock<IFeatureService>();
                var tagHelper = new FeatureTagHelper(featureServiceMock.Object)
                {
                    Inverse = true
                };

                // Act
                var actual = tagHelper.ShouldSuppressOutput(false);

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void Feature_Inactive_And_Not_Inverse_Should_Suppress_Output()
            {
                // Arrange
                var featureServiceMock = new Mock<IFeatureService>();
                var tagHelper = new FeatureTagHelper(featureServiceMock.Object)
                {
                    Inverse = false
                };

                // Act
                var actual = tagHelper.ShouldSuppressOutput(false);

                // Assert
                Assert.True(actual);
            }
        }
    }
}
using Moq;
using Xunit;

namespace MAF.FeaturesFlipping.AspNetCore.UnitTests
{
    public partial class FeatureTagHelperTests
    {
        [Trait("Category", "UnitTest")]
        public class GetFeatureToEvaluate
        {
            [Fact]
            public void Always_Take_The_Specified_FeatureName_When_Provided()
            {
                // Arrange
                var expected = new FeatureName("App", "Scope", "Feature");
                var featureServiceMock = new Mock<IFeatureService>();
                var featureTagHelper = new FeatureTagHelper(featureServiceMock.Object)
                {
                    Feature = expected,
                    Application = "MyApp",
                    Scope = "MyScope",
                    FeatureName = "MyFeature"
                };

                // Act
                var actual = featureTagHelper.GetFeatureToEvaluate();

                // Assert
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void Creates_A_New_FeatureName_When_No_One_Is_Provided()
            {
                // Arrange
                var expected = new FeatureName("App", "Scope", "Feature");
                var featureServiceMock = new Mock<IFeatureService>();
                var featureTagHelper = new FeatureTagHelper(featureServiceMock.Object)
                {
                    Application = expected.Application,
                    Scope = expected.Scope,
                    FeatureName = expected.Feature
                };

                // Act
                var actual = featureTagHelper.GetFeatureToEvaluate();

                // Assert
                Assert.Equal(expected, actual);
            }
        }
    }
}
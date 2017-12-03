using Moq;
using Xunit;

namespace MAF.FeaturesFlipping.AspNetCore
{
    public partial class FeatureTagHelperTests
    {
        [Trait("Category", "UnitTest")]
        public class GetFeatureSpecToEvaluate
        {
            [Fact]
            public void Always_Take_The_Specified_FeatureSpec_When_Provided()
            {
                // Arrange
                var expected = new FeatureSpec("App", "Scope", "FeatureName");
                var featureServiceMock = new Mock<IFeatureService>();
                var featureTagHelper = new FeatureTagHelper(featureServiceMock.Object, Factory.NullLoggerFactory())
                {
                    FeatureSpec = expected,
                    Application = "MyApp",
                    Scope = "MyScope",
                    FeatureName = "MyFeature"
                };

                // Act
                var actual = featureTagHelper.GetFeatureSpecToEvaluate();

                // Assert
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void Creates_A_New_FeatureSpec_When_No_One_Is_Provided()
            {
                // Arrange
                var expected = new FeatureSpec("App", "Scope", "FeatureName");
                var featureServiceMock = new Mock<IFeatureService>();
                var featureTagHelper = new FeatureTagHelper(featureServiceMock.Object, Factory.NullLoggerFactory())
                {
                    Application = expected.Application,
                    Scope = expected.Scope,
                    FeatureName = expected.FeatureName
                };

                // Act
                var actual = featureTagHelper.GetFeatureSpecToEvaluate();

                // Assert
                Assert.Equal(expected, actual);
            }
        }
    }
}
using System;
using Xunit;

namespace MAF.FeaturesFlipping.AspNetCore
{
    [Trait("Category", "UnitTest")]
    public partial class FeatureTagHelperTests
    {
        [Fact]
        public void Throws_ArgumentNullException_When_IFeatureService_Is_Null()
        {
            // Arrange
            var expectedParamName = "featureService";

            // Act && Assert
            var actual = Assert.Throws<ArgumentNullException>(() => new FeatureTagHelper(
                null, Factory.NullLoggerFactory()));
            Assert.Equal(expectedParamName, actual.ParamName);
        }
    }
}
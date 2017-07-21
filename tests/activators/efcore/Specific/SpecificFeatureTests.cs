using System;
using System.Linq;
using MAF.FeaturesFlipping.Activators.EntityFrameworkCore.Specific;
using Xunit;

namespace MAF.FeaturesFlipping.Activators.EntityFrameworkCore.UnitTests.Specific
{
    [Trait("Category", "UnitTest")]
    public class SpecificFeatureTests
    {
        [Fact]
        public void A_Null_SpecificFeatureQuery_Throws_Argument_Null_Exception()
        {
            // Arrange
            var expectedParamName = "specificFeatureQuery";

            // Act & Assert
            var actualException = Assert.Throws<ArgumentNullException>(() => new SpecificFeature<object>(null, null));
            Assert.Equal(expectedParamName, actualException.ParamName);
        }

        [Fact]
        public void A_Null_SpecificDbContextConfiguration_Throws_Argument_Null_Exception()
        {
            // Arrange
            var expectedParamName = "specificDbContextConfiguration";

            // Act & Assert
            var actualException = Assert.Throws<ArgumentNullException>(() => new SpecificFeature<object>(Enumerable.Empty<SpecificFeatureEntity<object>>().AsQueryable(), null));
            Assert.Equal(expectedParamName, actualException.ParamName);
        }
    }
}
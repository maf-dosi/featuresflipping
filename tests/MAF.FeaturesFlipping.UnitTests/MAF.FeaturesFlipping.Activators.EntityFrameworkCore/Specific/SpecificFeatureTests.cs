using System;
using Xunit;

namespace MAF.FeaturesFlipping.Activators.EntityFrameworkCore.Specific
{
    [Trait("Category", "UnitTest")]
    public class SpecificFeatureTests
    {
        [Fact]
        public void A_Null_SpecificFeatureQuery_Throws_Argument_Null_Exception()
        {
            // Arrange
            var expectedParamName = "genericFeatureQuery";

            // Act & Assert
            var actualException = Assert.Throws<ArgumentNullException>(() => new SpecificFeature<object>(null));
            Assert.Equal(expectedParamName, actualException.ParamName);
        }
    }
}
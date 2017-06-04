using System;
using Xunit;

namespace MAF.FeaturesFlipping.UnitTests
{
    [Trait("Category", "UnitTest")]
    public class FeatureNameTests
    {
        [Fact]
        public void The_Constructor_Correctly_Sets_The_Properties()
        {
            // Arrange
            var expectedApplication = "Application" + Guid.NewGuid();
            var expectedScope = "Scope" + Guid.NewGuid();
            var expectedFeature = "Feature" + Guid.NewGuid();

            // Act
            var actual = new FeatureName(expectedApplication, expectedScope, expectedFeature);

            // Assert
            Assert.Equal(expectedApplication, actual.Application);
            Assert.Equal(expectedScope, actual.Scope);
            Assert.Equal(expectedFeature, actual.Feature);
        }
    }
}
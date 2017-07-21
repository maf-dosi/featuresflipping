using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace MAF.FeaturesFlipping.Extensions.DependencyInjection.UnitTests
{
    [Trait("Category", "UnitTest")]
    public class FeaturesFlippingBuilderTests
    {
        [Fact]
        public void The_Constructor_Sets_The_Services_Property()
        {
            // Arrange
            var serviceCollectionMock = new Mock<IServiceCollection>();
            var expectedServiceCollection = serviceCollectionMock.Object;

            // Act
            var featuresFlippingBuilder = new FeaturesFlippingBuilder(expectedServiceCollection);

            // Assert
            Assert.Equal(expectedServiceCollection.GetHashCode(), featuresFlippingBuilder.Services.GetHashCode());
        }
    }
}

using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace MAF.FeaturesFlipping.Extensions.DependencyInjection
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
        [Fact]
        public void Calling_AddFeaturesFlipping_Add_The_Core_FeatureService_And_Create_The_FeaturesFlippingBuilder()
        {
            // Arrange
            var serviceCollection = new ServiceCollection();
            var expecteds = new List<ServiceDescriptor>
            {
                ServiceDescriptor.Scoped<IFeatureService, FeatureService>(),
                ServiceDescriptor.Scoped<IFeatureContextAccessor, FeatureContextAccessor>()
            };
            IEqualityComparer<ServiceDescriptor> comparer = new ServiceDescriptorEqualityComparer();

            // Act
            var actual = new FeaturesFlippingBuilder(serviceCollection); //serviceCollection.AddFeaturesFlipping();

            // Assert
            Assert.NotNull(actual);
            Assert.Equal(serviceCollection, actual.Services);
            Assert.Equal(expecteds.Count, actual.Services.Count);
            foreach (var expected in expecteds)
            {
                Assert.Contains(expected, actual.Services, comparer);
            }
        }
    }
}

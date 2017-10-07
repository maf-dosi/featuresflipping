using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace MAF.FeaturesFlipping.Extensions.DependencyInjection
{
    public partial class ServiceCollectionExtensionsTests
    {
        [Trait("Category", "UnitTest")]
        public class AddFeaturesFlipping
        {
            [Fact]
            public void Calling_AddFeaturesFlipping_Add_The_Core_FeatureService_And_Create_The_FeaturesFlippingBuilder()
            {
                // Arrange
                var serviceCollection = new ServiceCollection();

                // Act
                var actual = serviceCollection.AddFeaturesFlipping();

                // Assert
                Assert.NotNull(actual);
                Assert.Equal(serviceCollection, actual.Services);
                Assert.True(2 < actual.Services.Count);
                Assert.True(actual.Services.SingleOrDefault(sd => sd.ServiceType == typeof(IMemoryCache)) != null);
            }
        }
    }
}

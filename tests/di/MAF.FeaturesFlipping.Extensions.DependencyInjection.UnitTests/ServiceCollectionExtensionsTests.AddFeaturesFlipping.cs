using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace MAF.FeaturesFlipping.Extensions.DependencyInjection.UnitTests
{
    public partial class ServiceCollectionExtensionsTests
    {
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
                Assert.Equal(1, actual.Services.Count);
                var serviceDescriptor = actual.Services.First();
                Assert.Equal(typeof(IFeatureService), serviceDescriptor.ServiceType);
                Assert.Equal(typeof(FeatureService), serviceDescriptor.ImplementationType);
                Assert.Equal(ServiceLifetime.Scoped, serviceDescriptor.Lifetime);
            }
        }
    }
}
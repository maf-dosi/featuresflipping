using System.Collections.Generic;
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
                var expecteds = new List<ServiceDescriptor>
                {
                    new ServiceDescriptor(typeof(IFeatureService), typeof(FeatureService), ServiceLifetime.Scoped),
                    new ServiceDescriptor(typeof(FeaturesFlippingOptions), new FeaturesFlippingOptions())
                };
                IEqualityComparer<ServiceDescriptor> comparer = new ServiceDescriptorEqualityComparer();

                // Act
                var actual = serviceCollection.AddFeaturesFlipping();

                // Assert
                Assert.NotNull(actual);
                Assert.Equal(serviceCollection, actual.Services);
                Assert.Equal(2, actual.Services.Count);
                foreach (var expected in expecteds)
                {
                    Assert.Contains(expected, actual.Services, comparer);
                }
            }
        }
    }
}
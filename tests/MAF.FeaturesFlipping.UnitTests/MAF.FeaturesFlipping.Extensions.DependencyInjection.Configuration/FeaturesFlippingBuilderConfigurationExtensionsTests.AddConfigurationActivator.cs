using System.Collections.Generic;
using System.Linq;
using MAF.FeaturesFlipping.Activators.Configuration;
using MAF.FeaturesFlipping.Extensibility.Activators;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace MAF.FeaturesFlipping.Extensions.DependencyInjection.Configuration
{
    public partial class FeaturesFlippingBuilderConfigurationExtensionsTests
    {
        [Trait("Category", "UnitTest")]
        public class AddConfigurationActivator
        {
            [Fact]
            public void A_Call_To_AddConfigurationActivator_Adds_The_Right_Services()
            {
                // Arrange
                var serviceCollection = new ServiceCollection();
                var featuresFlippingBuilderMock = new Mock<IFeaturesFlippingBuilder>();
                featuresFlippingBuilderMock.SetupGet(_ => _.Services)
                    .Returns(serviceCollection);
                var configurationSection = new ConfigurationSection(
                    new ConfigurationRoot(
                        new List<IConfigurationProvider>()),
                    "somePath");
                var expectedLifetime = ServiceLifetime.Singleton;

                // Act
                var actualFeaturesFlippingBuilder =
                    featuresFlippingBuilderMock.Object.AddConfigurationActivator(configurationSection);

                // Assert
                Assert.NotNull(actualFeaturesFlippingBuilder);
                Assert.Equal(serviceCollection, actualFeaturesFlippingBuilder.Services);
                Assert.Equal(1, actualFeaturesFlippingBuilder.Services.Count);
                var actualServiceDescriptor = actualFeaturesFlippingBuilder.Services.First();
                Assert.Equal(typeof(IFeatureActivator), actualServiceDescriptor.ServiceType);
                Assert.IsType<ConfigurationFeatureActivator>(actualServiceDescriptor.ImplementationInstance);
                Assert.Equal(expectedLifetime, actualServiceDescriptor.Lifetime);
                Assert.Equal(configurationSection,
                    ((ConfigurationFeatureActivator) actualServiceDescriptor.ImplementationInstance)
                    .ConfigurationSection);
            }
        }
    }
}
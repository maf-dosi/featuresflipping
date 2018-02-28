using System.Linq;
using System.Threading.Tasks;
using MAF.FeaturesFlipping.Extensibility.FeatureContext;
using MAF.FeaturesFlipping.FeatureContext.Delegate;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

// ReSharper disable once CheckNamespace
namespace MAF.FeaturesFlipping.Extensions.DependencyInjection.FeatureContext.Delegate
{
    public partial class FeaturesFlippingBuilderFeatureContextDelegateExtensionsTests
    {
        public class AddDelegateFeatureContextPart
        {
            [Fact]
            public void Adding_The_Delegate_Based_Factory_Add_To_The_ServiceCollection()
            {
                // Arrange
                var serviceCollection = new ServiceCollection();
                var featuresFlippingBuilderMock = new Mock<IFeaturesFlippingBuilder>();
                featuresFlippingBuilderMock.SetupGet(_ => _.Services)
                    .Returns(serviceCollection);
                var expectedLifetime = ServiceLifetime.Singleton;

                // Act
                var actualFeaturesFlippingBuilder =
                    featuresFlippingBuilderMock.Object.AddDelegateFeatureContextPart(_ => Task.CompletedTask);

                // Assert
                Assert.NotNull(actualFeaturesFlippingBuilder);
                Assert.Equal(serviceCollection, actualFeaturesFlippingBuilder.Services);
                Assert.Equal(1, actualFeaturesFlippingBuilder.Services.Count);
                var actualServiceDescriptor = actualFeaturesFlippingBuilder.Services.First();
                Assert.Equal(typeof(IFeatureContextPartFactory), actualServiceDescriptor.ServiceType);
                Assert.IsType<DelegateFeatureContextPartFactory>(actualServiceDescriptor.ImplementationInstance);
                Assert.Equal(expectedLifetime, actualServiceDescriptor.Lifetime);
            }
        }
    }
}
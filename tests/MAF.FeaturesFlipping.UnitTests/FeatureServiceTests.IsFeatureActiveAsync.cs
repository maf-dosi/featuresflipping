using System.Linq;
using System.Threading.Tasks;
using MAF.Extensions.FeaturesFlipping;
using MAF.FeaturesFlipping.Extensibility.Activators;
using Moq;
using Xunit;

namespace MAF.FeaturesFlipping.UnitTests
{
    public partial class FeatureServiceTests
    {
        [Trait("Category", "UnitTest")]
        public class IsFeatureActiveAsync
        {
            [Fact]
            public void Returns_False_When_No_FeatureActivators_Are_Provided()
            {
                // Arrange
                var featureContextAccessorMock = new Mock<IFeatureContextAccessor>();
                featureContextAccessorMock.Setup(_ => _.GetCurrentFeatureContext())
                    .Returns(() => null);
                var featureService = new FeatureService(Enumerable.Empty<IFeatureActivator>(),
                    featureContextAccessorMock.Object);

                // Act
                var actual = featureService.IsFeatureActiveAsync(new FeatureName("", "", "")).Result;

                // Assert
                Assert.False(actual);
            }
            [Fact]
            public void Returns_False_When_No_Features_Are_Found()
            {
                // Arrange
                var featureContextAccessorMock = new Mock<IFeatureContextAccessor>();
                featureContextAccessorMock.Setup(_ => _.GetCurrentFeatureContext())
                    .Returns(() => null);
                var featureActivatorMock = new Mock<IFeatureActivator>();
                featureActivatorMock
                    .Setup(_ => _.GetFeatureStatus(It.IsAny<IFeatureName>(), It.IsAny<IFeatureContext>()))
                    .Returns(() => Task.FromResult(FeatureActivationStatus.NotSet));
                var featureService = new FeatureService(Enumerable.Repeat(featureActivatorMock.Object, 2),
                    featureContextAccessorMock.Object);

                // Act
                var actual = featureService.IsFeatureActiveAsync(new FeatureName("", "", "")).Result;

                // Assert
                Assert.False(actual);
            }
            [Fact]
            public void Returns_False_When_One_FeatureAccessor_Are_Active_NotSet_Inactive()
            {
                // Arrange
                var featureContextAccessorMock = new Mock<IFeatureContextAccessor>();
                featureContextAccessorMock.Setup(_ => _.GetCurrentFeatureContext())
                    .Returns(() => null);
                var featureActivatorInactiveMock = new Mock<IFeatureActivator>();
                featureActivatorInactiveMock
                    .Setup(_ => _.GetFeatureStatus(It.IsAny<IFeatureName>(), It.IsAny<IFeatureContext>()))
                    .Returns(() => Task.FromResult(FeatureActivationStatus.NotSet));
                var featureActivatorNotSetMock = new Mock<IFeatureActivator>();
                featureActivatorNotSetMock
                    .Setup(_ => _.GetFeatureStatus(It.IsAny<IFeatureName>(), It.IsAny<IFeatureContext>()))
                    .Returns(() => Task.FromResult(FeatureActivationStatus.NotSet));
                var featureActivatorActiveMock = new Mock<IFeatureActivator>();
                featureActivatorActiveMock
                    .Setup(_ => _.GetFeatureStatus(It.IsAny<IFeatureName>(), It.IsAny<IFeatureContext>()))
                    .Returns(() => Task.FromResult(FeatureActivationStatus.NotSet));
                var featureService = new FeatureService(new[] { featureActivatorActiveMock.Object, featureActivatorNotSetMock.Object, featureActivatorInactiveMock.Object },
                    featureContextAccessorMock.Object);

                // Act
                var actual = featureService.IsFeatureActiveAsync(new FeatureName("", "", "")).Result;

                // Assert
                Assert.False(actual);
            }
            [Fact]
            public void No_Other_FeatureAccessor_Are_Called_After_Ones_Returns_Inactive()
            {
                // Arrange
                var featureContextAccessorMock = new Mock<IFeatureContextAccessor>();
                featureContextAccessorMock.Setup(_ => _.GetCurrentFeatureContext())
                    .Returns(() => null);
                var featureActivatorInactiveMock = new Mock<IFeatureActivator>();
                featureActivatorInactiveMock
                    .Setup(_ => _.GetFeatureStatus(It.IsAny<IFeatureName>(), It.IsAny<IFeatureContext>()))
                    .Returns(() => Task.FromResult(FeatureActivationStatus.Inactive));
                var featureActivatorNeverCalledMock = new Mock<IFeatureActivator>();
                featureActivatorNeverCalledMock
                    .Setup(_ => _.GetFeatureStatus(It.IsAny<IFeatureName>(), It.IsAny<IFeatureContext>()))
                    .Verifiable("hould never be called after inactive");
                var featureActivatorActiveMock = new Mock<IFeatureActivator>();
                featureActivatorActiveMock
                    .Setup(_ => _.GetFeatureStatus(It.IsAny<IFeatureName>(), It.IsAny<IFeatureContext>()))
                    .Returns(() => Task.FromResult(FeatureActivationStatus.NotSet));
                var featureService = new FeatureService(new[] { featureActivatorInactiveMock.Object, featureActivatorNeverCalledMock.Object },
                    featureContextAccessorMock.Object);

                // Act
                var actual = featureService.IsFeatureActiveAsync(new FeatureName("", "", "")).Result;

                // Assert
                Assert.False(actual);
                featureActivatorNeverCalledMock.Verify(_ => _.GetFeatureStatus(It.IsAny<IFeatureName>(), It.IsAny<IFeatureContext>()), Times.Never);
            }
        }
    }
}
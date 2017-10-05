using System.Collections.Generic;
using MAF.FeaturesFlipping.Extensibility.Activators;
using Moq;
using Xunit;

namespace MAF.FeaturesFlipping.UnitTests
{
    public partial class FeatureServiceTests
    {
        [Trait("Category", "UnitTest")]
        public class ComputeFeatureActivationStatus
        {
            [Fact]
            public void No_Other_FeatureStatus_Are_Computed_After_Ones_Returns_Inactive()
            {
                // Arrange
                var featureContextAccessorMock = new Mock<IFeatureContextAccessor>();
                featureContextAccessorMock.Setup(_ => _.GetCurrentFeatureContextAsync())
                    .ReturnsAsync(() => null);
                var inactiveFeatureMock = new Mock<IFeature>();
                inactiveFeatureMock.Setup(_ => _.GetStatusAsync(It.IsAny<IFeatureContext>()))
                    .ReturnsAsync(FeatureActivationStatus.Inactive);
                var featureNeverCalledMock = new Mock<IFeature>();
                featureNeverCalledMock
                    .Setup(_ => _.GetStatusAsync(It.IsAny<IFeatureContext>()))
                    .Verifiable("Should never be called after inactive");

                var featureService = CreateFeatureService(featureContextAccessorMock.Object);

                // Act
                var actual = featureService.ComputeFeatureActivationStatus(
                    new List<AsyncLazy<IFeature>>
                    {
                        new AsyncLazy<IFeature>(() => inactiveFeatureMock.Object),
                        new AsyncLazy<IFeature>(() => featureNeverCalledMock.Object)
                    }).Result;

                // Assert
                Assert.False(actual);
                featureNeverCalledMock.Verify(_ => _.GetStatusAsync(It.IsAny<IFeatureContext>()), Times.Never);
            }
            [Fact]
            public void Returns_False_When_Features_Are_Inactive_NotSet_Active()
            {
                // Arrange
                var featureContextAccessorMock = new Mock<IFeatureContextAccessor>();
                featureContextAccessorMock.Setup(_ => _.GetCurrentFeatureContextAsync())
                    .ReturnsAsync(() => null);

                var inactiveFeatureMock = new Mock<IFeature>();
                inactiveFeatureMock.Setup(_ => _.GetStatusAsync(It.IsAny<IFeatureContext>()))
                    .ReturnsAsync(FeatureActivationStatus.Inactive);
                var notSetFeatureMock = new Mock<IFeature>();
                notSetFeatureMock.Setup(_ => _.GetStatusAsync(It.IsAny<IFeatureContext>()))
                    .ReturnsAsync(FeatureActivationStatus.NotSet);
                var activeFeatureMock = new Mock<IFeature>();
                activeFeatureMock.Setup(_ => _.GetStatusAsync(It.IsAny<IFeatureContext>()))
                    .ReturnsAsync(FeatureActivationStatus.Active);

                var featureService = CreateFeatureService(featureContextAccessorMock.Object);

                // Act
                var actual = featureService.ComputeFeatureActivationStatus(
                    new List<AsyncLazy<IFeature>>
                    {
                        new AsyncLazy<IFeature>(() => inactiveFeatureMock.Object),
                        new AsyncLazy<IFeature>(() => notSetFeatureMock.Object),
                    new AsyncLazy<IFeature>(() => activeFeatureMock.Object)
                    }).Result;

                // Assert
                Assert.False(actual);
            }
            [Fact]
            public void Returns_False_When_No_Features_Are_Provided()
            {
                // Arrange
                var featureContextAccessorMock = new Mock<IFeatureContextAccessor>();
                featureContextAccessorMock.Setup(_ => _.GetCurrentFeatureContextAsync())
                    .ReturnsAsync(() => null);
                var featureService = CreateFeatureService(featureContextAccessorMock.Object);

                // Act
                var actual = featureService.ComputeFeatureActivationStatus(
                    new List<AsyncLazy<IFeature>>()).Result;

                // Assert
                Assert.False(actual);
            }
            [Fact]
            public void Returns_True_When_Features_Are_Active()
            {
                // Arrange
                var featureContextAccessorMock = new Mock<IFeatureContextAccessor>();
                featureContextAccessorMock.Setup(_ => _.GetCurrentFeatureContextAsync())
                    .ReturnsAsync(() => null);

                var activeFeatureMock = new Mock<IFeature>();
                activeFeatureMock.Setup(_ => _.GetStatusAsync(It.IsAny<IFeatureContext>()))
                    .ReturnsAsync(FeatureActivationStatus.Active);

                var featureService = CreateFeatureService(featureContextAccessorMock.Object);

                // Act
                var actual = featureService.ComputeFeatureActivationStatus(new List<AsyncLazy<IFeature>>
                {
                    new AsyncLazy<IFeature>(() => activeFeatureMock.Object)
                }).Result;

                // Assert
                Assert.True(actual);
            }
            [Fact]
            public void Returns_True_When_Features_Are_Active_And_NotSet()
            {
                // Arrange
                var featureContextAccessorMock = new Mock<IFeatureContextAccessor>();
                featureContextAccessorMock.Setup(_ => _.GetCurrentFeatureContextAsync())
                    .ReturnsAsync(() => null);

                var activeFeatureMock = new Mock<IFeature>();
                activeFeatureMock.Setup(_ => _.GetStatusAsync(It.IsAny<IFeatureContext>()))
                    .ReturnsAsync(FeatureActivationStatus.Active);
                var notSetFeatureMock = new Mock<IFeature>();
                notSetFeatureMock.Setup(_ => _.GetStatusAsync(It.IsAny<IFeatureContext>()))
                    .ReturnsAsync(FeatureActivationStatus.NotSet);

                var featureService = CreateFeatureService(featureContextAccessorMock.Object);

                // Act
                var actual = featureService.ComputeFeatureActivationStatus(new List<AsyncLazy<IFeature>>
                {
                    new AsyncLazy<IFeature>(() => activeFeatureMock.Object),
                    new AsyncLazy<IFeature>(() => notSetFeatureMock.Object)
                }).Result;

                // Assert
                Assert.True(actual);
            }
            [Fact]
            public void Returns_True_When_Features_Are_NotSet_And_Active()
            {
                // Arrange
                var featureContextAccessorMock = new Mock<IFeatureContextAccessor>();
                featureContextAccessorMock.Setup(_ => _.GetCurrentFeatureContextAsync())
                    .ReturnsAsync(() => null);

                var activeFeatureMock = new Mock<IFeature>();
                activeFeatureMock.Setup(_ => _.GetStatusAsync(It.IsAny<IFeatureContext>()))
                    .ReturnsAsync(FeatureActivationStatus.Active);
                var notSetFeatureMock = new Mock<IFeature>();
                notSetFeatureMock.Setup(_ => _.GetStatusAsync(It.IsAny<IFeatureContext>()))
                    .ReturnsAsync(FeatureActivationStatus.NotSet);

                var featureService = CreateFeatureService(featureContextAccessorMock.Object);

                // Act
                var actual = featureService.ComputeFeatureActivationStatus(new List<AsyncLazy<IFeature>>
                {
                    new AsyncLazy<IFeature>(() => notSetFeatureMock.Object),
                    new AsyncLazy<IFeature>(() => activeFeatureMock.Object)
                }).Result;

                // Assert
                Assert.True(actual);
            }
        }
    }
}
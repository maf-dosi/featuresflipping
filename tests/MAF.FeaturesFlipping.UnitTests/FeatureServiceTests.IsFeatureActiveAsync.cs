﻿using System.Linq;
using System.Threading.Tasks;
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
            public void No_Other_FeatureAccessor_Are_Called_After_Ones_Returns_Inactive()
            {
                // Arrange
                var featureContextAccessorMock = new Mock<IFeatureContextAccessor>();
                featureContextAccessorMock.Setup(_ => _.GetCurrentFeatureContextAsync())
                    .ReturnsAsync(() => null);
                var inactiveFeatureMock = new Mock<IFeature>();
                inactiveFeatureMock.Setup(_ => _.GetStatusAsync(It.IsAny<IFeatureContext>()))
                    .ReturnsAsync(FeatureActivationStatus.Inactive);
                var featureActivatorInactiveMock = new Mock<IFeatureActivator>();
                featureActivatorInactiveMock
                    .Setup(_ => _.GetFeatureAsync(It.IsAny<FeatureSpec>()))
                    .ReturnsAsync(inactiveFeatureMock.Object);
                var featureActivatorNeverCalledMock = new Mock<IFeatureActivator>();
                featureActivatorNeverCalledMock
                    .Setup(_ => _.GetFeatureAsync(It.IsAny<FeatureSpec>()))
                    .Verifiable("hould never be called after inactive");

                var featureService = new FeatureService(
                    new[] { featureActivatorInactiveMock.Object, featureActivatorNeverCalledMock.Object },
                    featureContextAccessorMock.Object);

                // Act
                var actual = featureService.IsFeatureActiveAsync(new FeatureSpec("", "", "")).Result;
                featureService.Dispose();

                // Assert
                Assert.False(actual);
                featureActivatorNeverCalledMock.Verify(_ => _.GetFeatureAsync(It.IsAny<FeatureSpec>()), Times.Never);
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
                var featureActivatorInactiveMock = new Mock<IFeatureActivator>();
                featureActivatorInactiveMock
                    .Setup(_ => _.GetFeatureAsync(It.IsAny<FeatureSpec>()))
                    .ReturnsAsync(inactiveFeatureMock.Object);

                var notSetFeatureMock = new Mock<IFeature>();
                notSetFeatureMock.Setup(_ => _.GetStatusAsync(It.IsAny<IFeatureContext>()))
                    .ReturnsAsync(FeatureActivationStatus.NotSet);
                var featureActivatorNotSetMock = new Mock<IFeatureActivator>();
                featureActivatorNotSetMock
                    .Setup(_ => _.GetFeatureAsync(It.IsAny<FeatureSpec>()))
                    .ReturnsAsync(notSetFeatureMock.Object);

                var activeFeatureMock = new Mock<IFeature>();
                activeFeatureMock.Setup(_ => _.GetStatusAsync(It.IsAny<IFeatureContext>()))
                    .ReturnsAsync(FeatureActivationStatus.Active);
                var featureActivatorActiveMock = new Mock<IFeatureActivator>();
                featureActivatorActiveMock
                    .Setup(_ => _.GetFeatureAsync(It.IsAny<FeatureSpec>()))
                    .ReturnsAsync(activeFeatureMock.Object);

                var featureService = new FeatureService(
                    new[]
                    {
                        featureActivatorInactiveMock.Object, featureActivatorNotSetMock.Object,
                        featureActivatorActiveMock.Object
                    },
                    featureContextAccessorMock.Object);

                // Act
                var actual = featureService.IsFeatureActiveAsync(new FeatureSpec("", "", "")).Result;

                // Assert
                Assert.False(actual);
            }
            [Fact]
            public void Returns_False_When_No_FeatureActivators_Are_Provided()
            {
                // Arrange
                var featureContextAccessorMock = new Mock<IFeatureContextAccessor>();
                featureContextAccessorMock.Setup(_ => _.GetCurrentFeatureContextAsync())
                    .ReturnsAsync(() => null);
                var featureService = new FeatureService(Enumerable.Empty<IFeatureActivator>(),
                    featureContextAccessorMock.Object);

                // Act
                var actual = featureService.IsFeatureActiveAsync(new FeatureSpec("", "", "")).Result;

                // Assert
                Assert.False(actual);
            }
            [Fact]
            public void Returns_False_When_No_Features_Are_Found()
            {
                // Arrange
                var featureContextAccessorMock = new Mock<IFeatureContextAccessor>();
                featureContextAccessorMock.Setup(_ => _.GetCurrentFeatureContextAsync())
                    .ReturnsAsync(() => null);
                var featureActivatorMock = new Mock<IFeatureActivator>();
                featureActivatorMock
                    .Setup(_ => _.GetFeatureAsync(It.IsAny<FeatureSpec>()))
                    .ReturnsAsync(() => null);
                var featureService = new FeatureService(Enumerable.Repeat(featureActivatorMock.Object, 2),
                    featureContextAccessorMock.Object);

                // Act
                var actual = featureService.IsFeatureActiveAsync(new FeatureSpec("", "", "")).Result;

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
                var featureActivatorActiveMock = new Mock<IFeatureActivator>();
                featureActivatorActiveMock
                    .Setup(_ => _.GetFeatureAsync(It.IsAny<FeatureSpec>()))
                    .ReturnsAsync(activeFeatureMock.Object);

                var featureService = new FeatureService(
                    new[]
                    {
                        featureActivatorActiveMock.Object
                    },
                    featureContextAccessorMock.Object);

                // Act
                var actual = featureService.IsFeatureActiveAsync(new FeatureSpec("", "", "")).Result;

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
                var featureActivatorActiveMock = new Mock<IFeatureActivator>();
                featureActivatorActiveMock
                    .Setup(_ => _.GetFeatureAsync(It.IsAny<FeatureSpec>()))
                    .ReturnsAsync(activeFeatureMock.Object);

                var notSetFeatureMock = new Mock<IFeature>();
                notSetFeatureMock.Setup(_ => _.GetStatusAsync(It.IsAny<IFeatureContext>()))
                    .ReturnsAsync(FeatureActivationStatus.NotSet);
                var featureActivatorNotSetMock = new Mock<IFeatureActivator>();
                featureActivatorNotSetMock
                    .Setup(_ => _.GetFeatureAsync(It.IsAny<FeatureSpec>()))
                    .ReturnsAsync(notSetFeatureMock.Object);

                var featureService = new FeatureService(
                    new[]
                    {
                        featureActivatorActiveMock.Object,
                        featureActivatorNotSetMock.Object
                    },
                    featureContextAccessorMock.Object);

                // Act
                var actual = featureService.IsFeatureActiveAsync(new FeatureSpec("", "", "")).Result;

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
                var featureActivatorActiveMock = new Mock<IFeatureActivator>();
                featureActivatorActiveMock
                    .Setup(_ => _.GetFeatureAsync(It.IsAny<FeatureSpec>()))
                    .ReturnsAsync(activeFeatureMock.Object);

                var notSetFeatureMock = new Mock<IFeature>();
                notSetFeatureMock.Setup(_ => _.GetStatusAsync(It.IsAny<IFeatureContext>()))
                    .ReturnsAsync(FeatureActivationStatus.NotSet);
                var featureActivatorNotSetMock = new Mock<IFeatureActivator>();
                featureActivatorNotSetMock
                    .Setup(_ => _.GetFeatureAsync(It.IsAny<FeatureSpec>()))
                    .ReturnsAsync(notSetFeatureMock.Object);

                var featureService = new FeatureService(
                    new[]
                    {
                        featureActivatorNotSetMock.Object,
                        featureActivatorActiveMock.Object
                    },
                    featureContextAccessorMock.Object);

                // Act
                var actual = featureService.IsFeatureActiveAsync(new FeatureSpec("", "", "")).Result;

                // Assert
                Assert.True(actual);
            }

            [Fact]
            public async Task DisposeFeatureContext_Is_Called_After_All_Features_Are_Checked()
            {
                // Arrange
                var featureContextMock = new Mock<IFeatureContext>();
                var mockSequence = new MockSequence();
                var featureContextAccessorMock = new Mock<IFeatureContextAccessor>();
                featureContextAccessorMock.InSequence(mockSequence).Setup(_ => _.GetCurrentFeatureContextAsync())
                    .ReturnsAsync(featureContextMock.Object);
                featureContextAccessorMock.InSequence(mockSequence).Setup(_ => _.Dispose())
                    .Verifiable();

                var notSetFeatureMock = new Mock<IFeature>();
                notSetFeatureMock.Setup(_ => _.GetStatusAsync(featureContextMock.Object))
                    .ReturnsAsync(FeatureActivationStatus.NotSet);
                var featureActivatorNotSetMock = new Mock<IFeatureActivator>();
                featureActivatorNotSetMock
                    .Setup(_ => _.GetFeatureAsync(It.IsAny<FeatureSpec>()))
                    .ReturnsAsync(notSetFeatureMock.Object);

                var activeFeatureMock = new Mock<IFeature>();
                activeFeatureMock.Setup(_ => _.GetStatusAsync(featureContextMock.Object))
                    .ReturnsAsync(FeatureActivationStatus.Active);
                var featureActivatorActiveMock = new Mock<IFeatureActivator>();
                featureActivatorActiveMock
                    .Setup(_ => _.GetFeatureAsync(It.IsAny<FeatureSpec>()))
                    .ReturnsAsync(activeFeatureMock.Object);
                var featureService = new FeatureService(
                    new[]
                    {
                        featureActivatorNotSetMock.Object,
                        featureActivatorActiveMock.Object
                    }, featureContextAccessorMock.Object);

                // Act
                var actual = await featureService.IsFeatureActiveAsync(new FeatureSpec("", "", ""));
                featureService.Dispose();

                // Assert
                Assert.True(actual);

                featureContextAccessorMock.Verify(_ => _.GetCurrentFeatureContextAsync(), Times.Once);
                featureContextAccessorMock.Verify(_ => _.Dispose(), Times.Once);
            }

            [Theory]
            [InlineData(FeatureActivationStatus.Active, true)]
            [InlineData(FeatureActivationStatus.Inactive, false)]
            [InlineData(FeatureActivationStatus.NotSet, false)]
            public async Task The_Feature_Activation_Status_Is_Only_Computed_Once_And_Result_Is_Cached(
                FeatureActivationStatus featureActivationStatus, bool expected)
            {
                // Arrange
                var featureContextMock = new Mock<IFeatureContext>();
                var mockSequence = new MockSequence();
                var featureContextAccessorMock = new Mock<IFeatureContextAccessor>();
                featureContextAccessorMock.InSequence(mockSequence).Setup(_ => _.GetCurrentFeatureContextAsync())
                    .ReturnsAsync(featureContextMock.Object);
                featureContextAccessorMock.InSequence(mockSequence).Setup(_ => _.Dispose())
                    .Verifiable();

                var featureMock = new Mock<IFeature>();
                featureMock.Setup(_ => _.GetStatusAsync(featureContextMock.Object))
                    .ReturnsAsync(featureActivationStatus);
                var featureActivatorMock = new Mock<IFeatureActivator>();
                featureActivatorMock
                    .Setup(_ => _.GetFeatureAsync(It.IsAny<FeatureSpec>()))
                    .ReturnsAsync(featureMock.Object);
                var featureService = new FeatureService(
                    new[]
                    {
                        featureActivatorMock.Object
                    }, featureContextAccessorMock.Object);

                // Act
                var actual = await featureService.IsFeatureActiveAsync(new FeatureSpec("", "", ""));
                var actual2 = await featureService.IsFeatureActiveAsync(new FeatureSpec("", "", ""));
                featureService.Dispose();

                // Assert
                Assert.Equal(expected, actual);
                Assert.Equal(expected, actual2);

                featureContextAccessorMock.Verify(_ => _.GetCurrentFeatureContextAsync(), Times.Once);
                featureContextAccessorMock.Verify(_ => _.Dispose(), Times.Once);
                featureActivatorMock.Verify(_ => _.GetFeatureAsync(It.IsAny<FeatureSpec>()), Times.Once);
            }
        }
    }
}
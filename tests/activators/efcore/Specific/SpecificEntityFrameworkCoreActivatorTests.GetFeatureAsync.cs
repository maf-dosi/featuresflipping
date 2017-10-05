using System;
using MAF.FeaturesFlipping.Activators.EntityFrameworkCore.Specific;
using MAF.FeaturesFlipping.Extensibility.Activators;
using Moq;
using Xunit;

namespace MAF.FeaturesFlipping.Activators.EntityFrameworkCore.UnitTests.Specific
{
    public partial class SpecificEntityFrameworkCoreActivatorTests
    {
        [Trait("Category", "UnitTest")]
        public class GetFeatureAsync
        {
            [Theory]
            [InlineData("OtherColumn0", FeatureActivationStatus.NotSet)]
            [InlineData("OtherColumn1", FeatureActivationStatus.Active)]
            [InlineData("OtherColumn2", FeatureActivationStatus.Inactive)]
            [InlineData("OtherColumn3", FeatureActivationStatus.NotSet)]
            public async void Searching_A_Feature_Return_A_Correct_Activation_Status(
                string otherColumnValue, FeatureActivationStatus featureActivationStatus)
            {
                // Arrange
                var databaseName = $"{nameof(GetFeatureAsync)}:{nameof(Searching_A_Feature_Return_A_Correct_Activation_Status)}";

                var contextAndConfiguration = PopulateNewContext(databaseName, "otherColumnName", _ => f => f.OtherColumn == otherColumnValue);
                var featureContextServiceProviderMock = new Mock<IServiceProvider>();
                featureContextServiceProviderMock.Setup(_ => _.GetService(typeof(SpecificFeatureDbContext<string>)))
                    .Returns(contextAndConfiguration.Context);
                featureContextServiceProviderMock
                    .Setup(_ => _.GetService(typeof(SpecificDbContextConfiguration<string>)))
                    .Returns(contextAndConfiguration.Configuration);
                var featureContextMock = new Mock<IFeatureContext>();
                featureContextMock.SetupGet(_ => _.FeaturesServices).Returns(featureContextServiceProviderMock.Object);
                var activator = new SpecificEntityFrameworkCoreActivator<string>();

                // Act
                var actualFeature = await activator.GetFeatureAsync(new FeatureSpec(otherColumnValue.Replace("OtherColumn", "App"),
                    otherColumnValue.Replace("OtherColumn", "Scope"),
                    otherColumnValue.Replace("OtherColumn", "FeatureName")));

                // Assert
                Assert.NotNull(actualFeature);
                var actualActivationStatus = await actualFeature.GetStatusAsync(featureContextMock.Object);
                Assert.Equal(featureActivationStatus, actualActivationStatus);
            }
        }
    }
}

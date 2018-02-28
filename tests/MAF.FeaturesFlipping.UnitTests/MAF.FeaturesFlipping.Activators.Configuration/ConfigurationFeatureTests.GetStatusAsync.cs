using System.Collections.Generic;
using System.Threading.Tasks;
using MAF.FeaturesFlipping.Extensibility.Activators;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using Xunit;

namespace MAF.FeaturesFlipping.Activators.Configuration
{
    public partial class ConfigurationFeatureTests
    {
        [Trait("Category", "UnitTest")]
        public class GetStatusAsync
        {
            [Theory]
            [InlineData(null, FeatureActivationStatus.NotSet)]
            [InlineData("", FeatureActivationStatus.NotSet)]
            [InlineData("true", FeatureActivationStatus.Active)]
            [InlineData("false", FeatureActivationStatus.Inactive)]
            [InlineData("value", FeatureActivationStatus.NotSet)]
            public async Task Read_Of_A_Simple_Configuration_Section_Returns_The_Correct_Status(string configurationSectionValue,
                FeatureActivationStatus expectedFeatureActivationStatus)
            {
                // Arrange
                var memoryConfigurationSource = new MemoryConfigurationSource
                {
                    InitialData = new[] {new KeyValuePair<string, string>("SomePath", configurationSectionValue) }
                };
                var memoryConfigurationProvider = new MemoryConfigurationProvider(memoryConfigurationSource);
                var configurationRoot = new ConfigurationRoot(new List<IConfigurationProvider> { memoryConfigurationProvider });
                var configurationSection = new ConfigurationSection(configurationRoot, "somePath");
                var feature = new ConfigurationFeature(configurationSection);

                // Act
                var actual = await feature.GetStatusAsync(Factory.FeatureContext());

                // Assert
                Assert.Equal(expectedFeatureActivationStatus, actual);
            }

            [Theory]
            [InlineData(null, FeatureActivationStatus.NotSet)]
            [InlineData("", FeatureActivationStatus.NotSet)]
            [InlineData("true", FeatureActivationStatus.Active)]
            [InlineData("false", FeatureActivationStatus.Inactive)]
            [InlineData("value", FeatureActivationStatus.NotSet)]
            public async Task Complex_Configuration_Section_With_Matching_Context_Returns_Specified_Status(string configurationSectionValue,
                FeatureActivationStatus expectedFeatureActivationStatus)
            {
                // Arrange
                var memoryConfigurationSource = new MemoryConfigurationSource
                {
                    InitialData = new[]
                    {
                        new KeyValuePair<string, string>("Features:App1:Scope1:Feature1:0:Context:UserId", "123"),
                        new KeyValuePair<string, string>("Features:App1:Scope1:Feature1:0:Value", configurationSectionValue),
                    }
                };
                var memoryConfigurationProvider = new MemoryConfigurationProvider(memoryConfigurationSource);
                var configurationRoot = new ConfigurationRoot(new List<IConfigurationProvider> { memoryConfigurationProvider });
                var configurationSection = new ConfigurationSection(configurationRoot, "Features:App1:Scope1:Feature1");
                var feature = new ConfigurationFeature(configurationSection);
                var featureContextMock = Factory.FeatureContextMock();
                featureContextMock.Setup(_ => _.GetPart<object>("UserId"))
                    .Returns("123");
                var featureContext= featureContextMock.Object;

                // Act
                var actual = await feature.GetStatusAsync(featureContext);

                // Assert
                Assert.Equal(expectedFeatureActivationStatus, actual);
            }

            [Fact]
            public async Task Complex_Configuration_Section_Without_Matching_ContextPart_Value_Returns_NotSet_Status()
            {
                // Arrange
                var memoryConfigurationSource = new MemoryConfigurationSource
                {
                    InitialData = new[]
                    {
                        new KeyValuePair<string, string>("Features:App1:Scope1:Feature1:0:Context:UserId", "124"),
                        new KeyValuePair<string, string>("Features:App1:Scope1:Feature1:0:Value", "true"),
                    }
                };
                var memoryConfigurationProvider = new MemoryConfigurationProvider(memoryConfigurationSource);
                var configurationRoot = new ConfigurationRoot(new List<IConfigurationProvider> { memoryConfigurationProvider });
                var configurationSection = new ConfigurationSection(configurationRoot, "Features:App1:Scope1:Feature1");
                var feature = new ConfigurationFeature(configurationSection);
                var featureContextMock = Factory.FeatureContextMock();
                featureContextMock.Setup(_ => _.GetPart<object>("UserId"))
                    .Returns("123");
                var featureContext = featureContextMock.Object;

                // Act
                var actual = await feature.GetStatusAsync(featureContext);

                // Assert
                Assert.Equal(FeatureActivationStatus.NotSet, actual);
            }

            [Fact]
            public async Task Complex_Configuration_Section_With_Missing_ContextPart_Returns_NotSet_Status()
            {
                // Arrange
                var memoryConfigurationSource = new MemoryConfigurationSource
                {
                    InitialData = new[]
                    {
                        new KeyValuePair<string, string>("Features:App1:Scope1:Feature1:0:Context:GroupId", "147"),
                        new KeyValuePair<string, string>("Features:App1:Scope1:Feature1:0:Value", "true"),
                    }
                };
                var memoryConfigurationProvider = new MemoryConfigurationProvider(memoryConfigurationSource);
                var configurationRoot = new ConfigurationRoot(new List<IConfigurationProvider> { memoryConfigurationProvider });
                var configurationSection = new ConfigurationSection(configurationRoot, "Features:App1:Scope1:Feature1");
                var feature = new ConfigurationFeature(configurationSection);
                var featureContextMock = Factory.FeatureContextMock();
                featureContextMock.Setup(_ => _.GetPart<object>("UserId"))
                    .Returns("123");
                var featureContext = featureContextMock.Object;

                // Act
                var actual = await feature.GetStatusAsync(featureContext);

                // Assert
                Assert.Equal(FeatureActivationStatus.NotSet, actual);
            }
        }
    }
}
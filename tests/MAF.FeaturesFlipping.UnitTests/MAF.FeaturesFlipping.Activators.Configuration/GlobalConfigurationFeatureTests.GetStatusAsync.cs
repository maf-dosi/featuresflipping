using System.Collections.Generic;
using MAF.FeaturesFlipping.Extensibility.Activators;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using Xunit;

namespace MAF.FeaturesFlipping.Activators.Configuration
{
    public partial class GlobalConfigurationFeatureTests
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
            public void Read_Of_The_Configuration_Section_Returns_The_Correct_Status(string configurationSectionValue,
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
                var feature = new GlobalConfigurationFeature(configurationSection);

                // Act
                var actual = feature.GetStatusAsync(Factory.FeatureContext()).Result;

                // Assert
                Assert.Equal(expectedFeatureActivationStatus, actual);
            }
        }
    }
}
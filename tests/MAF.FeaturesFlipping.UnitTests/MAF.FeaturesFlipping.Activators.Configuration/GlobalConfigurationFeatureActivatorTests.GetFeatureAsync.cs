using System.Collections.Generic;
using MAF.FeaturesFlipping.Extensibility.Activators;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using Xunit;

namespace MAF.FeaturesFlipping.Activators.Configuration
{
    public partial class GlobalConfigurationFeatureActivatorTests
    {
        [Trait("Category", "UnitTest")]
        public class GetFeatureAsync
        {
            [Fact]
            public void A_Non_Existing_Application_Returns_NotSet()
            {
                // Arrange
                var memoryConfigurationSource = new MemoryConfigurationSource
                {
                    InitialData = new[] {new KeyValuePair<string, string>("SomePath", "")}
                };
                var memoryConfigurationProvider = new MemoryConfigurationProvider(memoryConfigurationSource);
                var configurationRoot =
                    new ConfigurationRoot(new List<IConfigurationProvider> {memoryConfigurationProvider});
                var configurationSection = new ConfigurationSection(configurationRoot, "somePath");
                var featureActivator = new GlobalConfigurationFeatureActivator(configurationSection);

                // Act
                var actualFeature = featureActivator.GetFeatureAsync(new FeatureSpec("App1", "Scope1", "Feature1"))
                    .Result;

                // Assert
                var actual = actualFeature.GetStatusAsync(Factory.FeatureContext()).Result;
                Assert.Equal(FeatureActivationStatus.NotSet, actual);
            }
        }
    }
}
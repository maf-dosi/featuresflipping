using System;
using System.Threading.Tasks;
using MAF.FeaturesFlipping.Extensibility.Activators;
using Microsoft.Extensions.Configuration;

namespace MAF.FeaturesFlipping.Activators.Configuration
{
    public class ConfigurationFeatureActivator : IFeatureActivator
    {
        public ConfigurationFeatureActivator(IConfigurationSection rootConfigurationSection)
        {
            ConfigurationSection = rootConfigurationSection ??
                                   throw new ArgumentNullException(nameof(rootConfigurationSection));
        }

        internal IConfigurationSection ConfigurationSection { get; }

        public Task<IFeature> GetFeatureAsync(FeatureSpec featureSpec)
        {
            var applicationSection = ConfigurationSection.GetSection(featureSpec.Application);
            var scopeSection = applicationSection.GetSection(featureSpec.Scope);
            var featureSection = scopeSection.GetSection(featureSpec.FeatureName);
            var globalConfigurationFeature = new ConfigurationFeature(featureSection);
            return Task.FromResult<IFeature>(globalConfigurationFeature);
        }
    }
}
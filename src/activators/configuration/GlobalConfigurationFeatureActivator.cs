using System;
using System.Threading.Tasks;
using MAF.FeaturesFlipping.Extensibility.Activators;
using Microsoft.Extensions.Configuration;

namespace MAF.FeaturesFlipping.Activators.Configuration
{
    public class GlobalConfigurationFeatureActivator : IFeatureActivator
    {
        public GlobalConfigurationFeatureActivator(IConfigurationSection rootConfigurationSection)
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
            var globalConfigurationFeature = new GlobalConfigurationFeature(featureSection);
            return Task.FromResult<IFeature>(globalConfigurationFeature);
        }
    }
}
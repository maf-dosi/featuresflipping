using System;
using System.Threading.Tasks;
using MAF.FeaturesFlipping.Extensibility.Activators;
using Microsoft.Extensions.Configuration;

namespace MAF.FeaturesFlipping.Activators.Configuration
{
    public class GlobalConfigurationFeatureActivator : IFeatureActivator
    {
        private readonly IConfigurationSection _rootConfigurationSection;

        public GlobalConfigurationFeatureActivator(IConfigurationSection rootConfigurationSection)
        {
            _rootConfigurationSection = rootConfigurationSection ?? throw new ArgumentNullException(nameof(rootConfigurationSection));
        }

        public Task<IFeature> GetFeatureAsync(IFeatureName featureName)
        {
            var applicationSection = _rootConfigurationSection.GetSection(featureName.Application);
            var scopeSection = applicationSection.GetSection(featureName.Scope);
            var featureSection = scopeSection.GetSection(featureName.Feature);
            var globalConfigurationFeature = new GlobalConfigurationFeature(featureSection);
            return Task.FromResult<IFeature>(globalConfigurationFeature);
        }
    }
}
using System;
using System.Threading.Tasks;
using MAF.FeaturesFlipping.Extensibility.Activators;
using Microsoft.Extensions.Configuration;

namespace MAF.FeaturesFlipping.Activators.Configuration
{
    public class GlobalConfigurationFeatureActivator : IFeatureActivator
    {
        private readonly IConfigurationSection _configurationSection;

        public GlobalConfigurationFeatureActivator(IConfigurationSection configurationSection)
        {
            _configurationSection = configurationSection;
        }

        public Task<IFeature> GetFeatureAsync(IFeatureName featureName)
        {
            throw new NotImplementedException();
        }
    }
}
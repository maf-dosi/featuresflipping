using System;
using System.Threading.Tasks;
using MAF.FeaturesFlipping.Extensibility.Activators;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MAF.FeaturesFlipping.Activators.Configuration
{
    internal class GlobalConfigurationFeature : IFeature
    {
        private readonly IConfigurationSection _configurationSection;

        public GlobalConfigurationFeature(IConfigurationSection configurationSection)
        {
            _configurationSection = configurationSection ?? throw new ArgumentNullException(nameof(configurationSection));
        }

        public Task<FeatureActivationStatus> GetStatusAsync(IFeatureContext featureContext)
        {
            var logger = featureContext.FeaturesServices.GetService<ILogger<FeatureSpec>>();
            var value = _configurationSection.Value;
            logger.RetrieveValueOfConfigurationSection(value);
            if (string.IsNullOrEmpty(value) || !bool.TryParse(value, out var isActive))
            {
                logger.ReturnNotSetActivationStatus();
                return Task.FromResult(FeatureActivationStatus.NotSet);
            }
            if (isActive)
            {
                logger.ReturnActiveActivationStatus();
                return Task.FromResult(FeatureActivationStatus.Active);
            }
            logger.ReturnInactiveActivationStatus();
            return Task.FromResult(FeatureActivationStatus.Inactive);
        }
    }
}
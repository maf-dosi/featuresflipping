using System;
using System.Linq;
using System.Threading.Tasks;
using MAF.FeaturesFlipping.Extensibility.Activators;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MAF.FeaturesFlipping.Activators.Configuration
{
    internal class ConfigurationFeature : IFeature
    {
        private readonly IConfigurationSection _configurationSection;

        public ConfigurationFeature(IConfigurationSection configurationSection)
        {
            _configurationSection = configurationSection ?? throw new ArgumentNullException(nameof(configurationSection));
        }

        public Task<FeatureActivationStatus> GetStatusAsync(IFeatureContext featureContext)
        {
            var logger = featureContext.FeaturesServices.GetService<ILogger<FeatureSpec>>();
            var value = _configurationSection.Value;
            logger.RetrieveValueOfConfigurationSection(value);
            if (string.IsNullOrEmpty(value))
            {
                return Task.FromResult(GetSpecificStatus(featureContext, logger));
            }
            return Task.FromResult(GetStatusFromValue(value, logger));
        }

        private FeatureActivationStatus GetStatusFromValue(string value, ILogger<FeatureSpec> logger)
        {
            if (!bool.TryParse(value, out var isActive))
            {
                logger.ReturnNotSetActivationStatus();
                return FeatureActivationStatus.NotSet;
            }

            if (isActive)
            {
                logger.ReturnActiveActivationStatus();
                return FeatureActivationStatus.Active;
            }

            logger.ReturnInactiveActivationStatus();
            return FeatureActivationStatus.Inactive;
        }

        private FeatureActivationStatus GetSpecificStatus(IFeatureContext featureContext, ILogger<FeatureSpec> logger)
        {
            foreach (var specificFeatureConfigurationSection in _configurationSection.GetChildren())
            {
                var statusComputation =
                    GetSpecificStatusForOneItem(specificFeatureConfigurationSection, featureContext, logger);
                if (statusComputation.HasValue)
                {
                    return statusComputation.Status;
                }
            }
            return ReturnNotSetActivationStatus(logger);
        }
        private (bool HasValue, FeatureActivationStatus Status) GetSpecificStatusForOneItem(IConfigurationSection itemConfigurationSection, IFeatureContext featureContext, ILogger<FeatureSpec> logger)
        {
            var childrenSection = itemConfigurationSection.GetChildren().ToList();
            if (childrenSection.Count != 2)
            {
                // Log warning
                return ReturnNotComputedActivationStatus(logger, "Configuration section should have two parts.");
            }

            var contextSection = childrenSection.FirstOrDefault(section => section.Key == "Context");
            if (contextSection == null)
            {
                // Log warning
                return ReturnNotComputedActivationStatus(logger, "Configuration section should have 'Context' part");
            }

            var valueSection = childrenSection.FirstOrDefault(section => section.Key == "Value");
            if (valueSection == null)
            {
                // Log warning
                return ReturnNotComputedActivationStatus(logger, "Configuration section should have 'Value' part");
            }

            return GetSpecificStatusForItem(contextSection, valueSection, featureContext, logger);
        }

        private (bool HasValue, FeatureActivationStatus Status) GetSpecificStatusForItem(
            IConfigurationSection contextSection, IConfigurationSection valueSection, IFeatureContext featureContext, ILogger<FeatureSpec> logger)
        {
            foreach (var contextItemSection in contextSection.GetChildren())
            {
                var featureContextPart = featureContext.GetPart<object>(contextItemSection.Key);
                if (featureContextPart == null || !featureContextPart.ToString()
                        .Equals(contextItemSection.Value, StringComparison.InvariantCulture))
                {
                    return ReturnNotComputedActivationStatus(logger, $"FeatureContextPart '{contextItemSection.Key}' not found, or value doesn't match");
                }
            }

            var value = GetStatusFromValue(valueSection.Value, logger);
            return (true, value);
        }

        private static (bool HasValue, FeatureActivationStatus Status) ReturnNotComputedActivationStatus(ILogger<FeatureSpec> logger, string reason)
        {
            logger.ReturnNotComputedActivationStatus(reason);
            return (false, FeatureActivationStatus.NotSet);
        }
        private static FeatureActivationStatus ReturnNotSetActivationStatus(ILogger<FeatureSpec> logger)
        {
            logger.ReturnNotSetActivationStatus();
            return FeatureActivationStatus.NotSet;
        }
    }
}
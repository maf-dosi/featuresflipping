# Configuration based activators

## Principles
The `ConfigurationActivator` reads the status value of the `IFeature` from an `IConfigurationSection`.
The format of the configuration can have two formats (shown here as JSON):
```
{
    "App1": {
        "Scope1": {
            "FeatureName1": true,
            "FeatureName2": [
                {"Context": {
                    "PartName":"PartValueAsString",
                    ...
                    },
                 "Value": true },
                ...
            ]
        }
    }
}
```
The outer section contains several applications,
which include several scopes,
which includes several feature items.

Each feature items can be:
- a boolean indicating if the feature is active or not,
- an array of objects containing two parts:
  - the description of the context:
each properties of the context part should be present in the `FeatureContext` and its _`ToString()`_ value must match,
  - the value which is a boolean indicating if the feature is active or not,

Any missing element (application, scope, feature name or not matching context part) defines the feature status as `NotSet`.

## Adding configuration activator
### Via the DI system
To add the configuration activator,
you should call the extension method `AddConfigurationActivator` on the `IFeaturesFlippingBuilder`
(in the package `MAF.FeaturesFlipping.Extensions.DependencyInjection.Configuration`).
The method take as parameter the section of configuration to use.
The creation of the configuration section doesn't matter (JSON, environment variables, etc.).

### Without the DI system
To create the configuration activator,
you just have to create an instance of `MAF.FeaturesFlipping.Activators.Configuration.ConfigurationFeatureActivator`
(in the package `MAF.FeaturesFlipping.Activators.Configuration`)
and passing in the constructor the `IConfigurationSection` to use.

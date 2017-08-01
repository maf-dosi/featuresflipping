# Configuration based activators
## Global configuration
The `GlobalConfigurationActivator` reads the status value of the `IFeature` from an `IConfigurationSection`.
The format of the configuration should be:
```
{
    "App1": {
        "Scope1": {
            "FeatureName1": true,
            "FeatureName2": false
        }
    }
}
```
The outer section contains several application includ items, which include several scope items, which includes several feature items. Each feature value must be a boolean indicating if the feature is active or not.

Any missing element (application, scope or feature name) defines the feature status as `NotSet`.



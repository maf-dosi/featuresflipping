# Configuration based activators
## Global configuration
The `GlobalConfigurationActivator` reads the status value of the `IFeature` from an `IConfigurationSection`.
The format of the configuration should be:
```
{
    "App1": {
        "Scope1": {
            "Feature1": true,
            "Feature2": false
        }
    }
}
```
The outer section contains several application includ items, which include several scope items, which includes several feature items. Each feature value must be a boolean indicating if the feature is active or not.

Any missing element (application, scope or feature) defines the feature status as `NotSet`.



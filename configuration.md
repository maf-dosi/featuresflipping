# Configuration based activators
## Global configuration
The `GlobalConfigurationActivator` reads the status value of the `IFeature` from a `IConfigurationSection`.
The format of the configuration should be:

The outer section contains several application includ items, which include several scope items, which includes several feature items. Each feature value must be a boolean indicating if the feature is active or not.

Any missing element (application, scope or feature) define the feature status as `NotSet`.



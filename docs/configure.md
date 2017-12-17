# Configuring Features Flipping

## In ASP.NET Core
Configure the features flipping is as simple as adding `AddFeaturesFlipping` in your `Startup.ConfigureServices` method.
This will add the services used to compute the status of a feature.
The method will return an `IFeaturesFlippingBuilder` that will be used in the next steps.

The next step is to specify how to compute this status:
1. [By adding activators](activators/index.md)
2. By populating the `IFeatureContext`


## Register different implementations of services
You can also register two implementations of a service that will be resolved depending of the status of a feature.

Registring these services can be done by calling one the overload of the extension method to `IFeaturesFlippingBuilder` `Register<Scoped|Transient>Service` .
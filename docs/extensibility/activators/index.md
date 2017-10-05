# Create an activator
1. Create an implementation of `IFeature`


The first step is to create an implementation of  the interface `MAF.FeaturesFlipping.Extensibility.Activators.IFeature` (in the package `MAF.FeaturesFlipping.Extensibility.Activators.Abstractions`).
It has only one method `Task<FeatureActivationStatus> GetStatusAsync(IFeatureContext featureContext)`. This method is responsible to compute the activation status of a feature given a context.
The context contains two main "properties" : a `FeatureServices` (of type `IServiceProvider`) and parts accessible through the method `GetPart`. The parts are items added to the context (and depending of the current context of execution (eg HTTP context for ASP.NET based applications)).
The items can be used by the feature to determine if a feature should be activated based on them (it can be the user, a tenant...)
The activation status will be computed only once per context of execution: this means that a feature is (in)active for the entire lifetime of one context of execution.

2. Create an implementation of `IFeatureActivator`


The second step is to create an implementation of the interface `MAF.FeaturesFlipping.Extensibility.Activators.IFeatureActivator` (in the package `MAF.FeaturesFlipping.Extensibility.Activators.Abstractions`).
It has only one method : `Task<IFeature> GetFeatureAsync(FeatureSpec featureSpec)`.
The purpose of this method is to create a feature (the one defined in the first step) based on a spec (application name, scope, feature name).
Ths feature will be cached with the spec as key and not be resolved for each context of execution.
It should be independant of the context of execution.

3. Optinal - Create an implementation of `IFeatureContextPartFactory`

The third (optional) step is to create an implementation of the interface `MAF.FeaturesFlipping.Extensibility.FeatureContext.IFeatureContextPartFactory` (in the package `MAF.FeaturesFlipping.Extensibility.FeatureContext.Abstractions`).
This interface provides two method (`Task AddFeatureContextPartAsync(IFeatureContext featureContext)` and `void ReleaseFeatureContextPart(IFeatureContext featureContext)`) that are responsible to add (and release/dispose) parts in the feature context.
These parts can be used by the feature to obtain information about the context (eg. the user, the tenant...).

4. Optional - Create extension methods to help register your `IFeatureActivator`

The last step is to create some extension methods to help register your `IFeatureActivator` (and optionally your `IFeatureContextPartFactory`) to the DI system.
[See DI for more info](../dependencyinjection/index.md).
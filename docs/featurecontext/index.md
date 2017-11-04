# The FeatureContext
The FeatureContext is a context created to contextualize the computation of the status of a feature.
It is populated once per scope, and is passed to the method `GetStatusAsync` of each `IFeature`.
The context contains two main items:
* a scoped `IServiceProvider` (through the property `FeaturesServices`)
* some parts accessible through the methods `GetPart` and `SetPart`, and a name to identify the part.

The parts are populated in the context via implementation of interface `MAF.FeaturesFlipping.Extensibility.FeatureContext.IFeatureContextPartFactory` (in the package `MAF.FeaturesFlipping.Extensibility.FeatureContext.Abstractions`). There is some built-in [implementations of this interface](implementations.md).
# Delegate-based

The delegate-based feature context part factory (`MAF.FeaturesFlipping.FeatureContext.Delegate.DelegateFeatureContextPartFactory` in the package `MAF.FeaturesFlipping.FeatureContext.Delegate`) is an implementation of `IFeatureContextPartFactory` that use delegates to add and release part of feature context.

This factory has a constructor that takes two delegate: one for adding part(s) to the context, and one to dispose the part(s). Both of them take a `MAF.FeaturesFlipping.Extensibility.Activators.IFeatureContext`, but the first one that add part(s) to the context must returns a `Task` that complete when all parts have been added.

To simplify registring this factory, the package `MAF.FeaturesFlipping.Extensions.DependencyInjection.FeatureContext.Delegate` contains an extension method (`AddDelegateFeatureContextPart`) that add the factory to the DI system.
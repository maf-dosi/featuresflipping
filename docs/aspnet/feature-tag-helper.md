# Features flipping
## ASP.NET Core
### Tag helper

The Feature Tag Helper conditionally renders its enclosed content based on the activation status of the specified feature. The feature can be specified via its `feature-spec` attribute (given a `FeatureSpec`), or `application`, `scope` and `feature-name` properties which will build the corresponding `FeatureSpec`.
The Feature Tag Helper can also be used to render content when the feature is inactive thanks to the `inverse` attribute.
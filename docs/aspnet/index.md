# Features flipping
## ASP.NET Core

1. Configure MAF.FeaturesFlipping (see [Configuring MAF.FeaturesFlipping](../Configure.md))
2. Install the standard Nuget package into your ASP.Net Core application
```
Install-Package MAF.FeaturesFlipping.AspNetCore
```
3. Add the attribute `[MAF.FeaturesFlipping.AspNetCore.FeatureFilterAttribute]` to the action you want to put behind a feature.

If the feature is inactive the action will not be found


#
#
Try the [Feature Tag Helper](feature-tag-helper.md).
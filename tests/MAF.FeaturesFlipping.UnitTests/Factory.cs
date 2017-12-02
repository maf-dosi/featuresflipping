using System;
using MAF.FeaturesFlipping;
using MAF.FeaturesFlipping.Extensibility.Activators;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

internal static class Factory
{
    public static IFeatureContext FeatureContext()
    {
        var serviceProviderMock = new Mock<IServiceProvider>();
        serviceProviderMock.Setup(_ => _.GetService(typeof(ILogger<FeatureSpec>)))
            .Returns(new NullLogger<FeatureSpec>());
        var featureContext = new FeatureContext(serviceProviderMock.Object);
        return featureContext;
    }

    public static ILogger NullLogger()
    {
        return new NullLogger<FeatureSpec>();
    }
}
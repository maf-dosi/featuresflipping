using System;
using System.Diagnostics;
using MAF.FeaturesFlipping;
using MAF.FeaturesFlipping.Extensibility.Activators;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

internal static class Factory
{
    [DebuggerStepThrough]
    public static IFeatureContext FeatureContext()
    {
        var featureContextMock = FeatureContextMock();
        return featureContextMock.Object;
    }
    public static Mock<IFeatureContext> FeatureContextMock()
    {
        var serviceProviderMock = new Mock<IServiceProvider>();
        serviceProviderMock.Setup(_ => _.GetService(typeof(ILogger<FeatureSpec>)))
            .Returns(new NullLogger<FeatureSpec>());
        var featureContextMock = new Mock<IFeatureContext>();
        featureContextMock.Setup(_ => _.FeaturesServices)
            .Returns(serviceProviderMock.Object);
        return featureContextMock;
    }

    public static ILogger NullLogger()
    {
        return new NullLogger<FeatureSpec>();
    }

    public static ILoggerFactory NullLoggerFactory()
    {
        return new NullLoggerFactory();
    }
}
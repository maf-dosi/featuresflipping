using System;

namespace MAF.FeaturesFlipping.Extensibility.Activators
{
    public interface IFeatureContext
    {
        IServiceProvider FeaturesServices { get; }
        T GetPart<T>(string partName);
        void SetPart<T>(T featureContextPart, string partName);
    }
}
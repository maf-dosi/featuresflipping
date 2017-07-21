namespace MAF.FeaturesFlipping.Extensibility.Activators
{
    public interface IFeatureContext
    {
        T GetPart<T>(string partName);
        void SetPart<T>(T featureContextPart, string partName);
    }
}
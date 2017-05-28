namespace MAF.FeaturesFlipping.Extensibility.Activators
{
    public interface IFeatureContextAccessor
    {
        IFeatureContext GetCurrentFeatureContext();
    }
}
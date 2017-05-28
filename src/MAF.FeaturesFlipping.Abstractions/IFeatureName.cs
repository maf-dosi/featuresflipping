namespace MAF.Extensions.FeaturesFlipping
{
    public interface IFeatureName
    {
        string Application { get; }
        string Scope { get; }
        string Feature { get; }
    }
}
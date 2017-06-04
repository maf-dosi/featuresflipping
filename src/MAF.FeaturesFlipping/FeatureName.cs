namespace MAF.FeaturesFlipping
{
    public sealed class FeatureName : IFeatureName
    {
        public FeatureName(string application, string scope, string feature)
        {
            Application = application;
            Scope = scope;
            Feature = feature;
        }

        public string Application { get; }
        public string Scope { get; }
        public string Feature { get; }
    }
}
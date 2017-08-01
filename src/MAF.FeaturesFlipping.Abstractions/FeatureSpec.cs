using System;

namespace MAF.FeaturesFlipping
{
    public struct FeatureSpec
    {
        private readonly string _application;
        private readonly string _scope;
        private readonly string _featureName;

        public FeatureSpec(string application, string scope, string featureName)
        {
            _application = application;
            _scope = scope;
            _featureName = featureName;
        }

        public string Application => _application ?? string.Empty;

        public string Scope => _scope ?? string.Empty;

        public string FeatureName => _featureName ?? string.Empty;

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + Application.GetHashCode();
                hash = hash * 23 + Scope.GetHashCode();
                hash = hash * 23 + FeatureName.GetHashCode();
                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            return obj is FeatureSpec featureSpec
                   && Application.Equals(featureSpec.Application, StringComparison.CurrentCulture)
                   && Scope.Equals(featureSpec.Scope, StringComparison.CurrentCulture)
                   && FeatureName.Equals(featureSpec.FeatureName, StringComparison.CurrentCulture);
        }

        public override string ToString()
        {
            return $"<Application={Application};Scope={Scope};FeatureName={FeatureName}>";
        }
    }
}
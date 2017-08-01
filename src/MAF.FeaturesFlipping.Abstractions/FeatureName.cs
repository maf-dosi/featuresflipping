using System;

namespace MAF.FeaturesFlipping
{
    public struct FeatureName
    {
        private readonly string _application;
        private readonly string _scope;
        private readonly string _feature;

        public FeatureName(string application, string scope, string feature)
        {
            _application = application;
            _scope = scope;
            _feature = feature;
        }

        public string Application => _application ?? string.Empty;

        public string Scope => _scope ?? string.Empty;

        public string Feature => _feature ?? string.Empty;

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + Application.GetHashCode();
                hash = hash * 23 + Scope.GetHashCode();
                hash = hash * 23 + Feature.GetHashCode();
                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            return obj is FeatureName featureName
                   && Application.Equals(featureName.Application, StringComparison.CurrentCulture)
                   && Scope.Equals(featureName.Scope, StringComparison.CurrentCulture)
                   && Feature.Equals(featureName.Feature, StringComparison.CurrentCulture);
        }

        public override string ToString()
        {
            return $"<Application={Application};Scope={Scope};Feature={Feature}>";
        }
    }
}
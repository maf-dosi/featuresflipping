using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MAF.FeaturesFlipping.AspNetCore
{
    [HtmlTargetElement(FeatureTagName, Attributes = FeatureSpecAttributeName)]
    [HtmlTargetElement(FeatureTagName, Attributes = ApplicationAttributeName)]
    [HtmlTargetElement(FeatureTagName, Attributes = ScopeAttributeName)]
    [HtmlTargetElement(FeatureTagName, Attributes = FeatureNameAttributeName)]
    [HtmlTargetElement(FeatureTagName, Attributes = InverseAttributeName)]
    public sealed class FeatureTagHelper : TagHelper
    {
        private readonly IFeatureService _featureService;

        private const string FeatureTagName = "feature";

        private const string FeatureSpecAttributeName = "feature-spec";
        [HtmlAttributeName(FeatureSpecAttributeName)]
        public FeatureSpec FeatureSpec { get; set; }

        private const string ApplicationAttributeName = "application";
        [HtmlAttributeName(ApplicationAttributeName)]
        public string Application { get; set; }

        private const string ScopeAttributeName = "scope";
        [HtmlAttributeName(ScopeAttributeName)]
        public string Scope { get; set; }

        private const string FeatureNameAttributeName = "feature-name";
        [HtmlAttributeName(FeatureNameAttributeName)]
        public string FeatureName { get; set; }

        private const string InverseAttributeName = "inverse";
        [HtmlAttributeName(InverseAttributeName)]
        public bool Inverse { get; set; }

        public FeatureTagHelper(IFeatureService featureService)
        {
            _featureService = featureService ?? throw new ArgumentNullException(nameof(featureService));
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (output == null)
            {
                throw new ArgumentNullException(nameof(output));
            }
            
            output.TagName = null;

            var featureSpec = GetFeatureSpecToEvaluate();
            var isFeatureActive = await _featureService.IsFeatureActiveAsync(featureSpec);
            var suppressOutput = ShouldSuppressOutput(isFeatureActive);

            if (suppressOutput)
            {
                output.SuppressOutput();
            }
        }

        internal bool ShouldSuppressOutput(bool isFeatureActive)
        {
            return !(isFeatureActive ^ Inverse);
        }

        internal FeatureSpec GetFeatureSpecToEvaluate()
        {
            var featureSpec = FeatureSpec.Equals(default(FeatureSpec))
                ? new FeatureSpec(Application, Scope, FeatureName)
                : FeatureSpec;
            return featureSpec;
        }
    }
}

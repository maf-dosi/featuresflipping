using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Logging;

namespace MAF.FeaturesFlipping.AspNetCore
{
    [HtmlTargetElement(FeatureTagName, Attributes = FeatureSpecAttributeName)]
    [HtmlTargetElement(FeatureTagName, Attributes = ApplicationAttributeName)]
    [HtmlTargetElement(FeatureTagName, Attributes = ScopeAttributeName)]
    [HtmlTargetElement(FeatureTagName, Attributes = FeatureNameAttributeName)]
    [HtmlTargetElement(FeatureTagName, Attributes = InverseAttributeName)]
    public sealed class FeatureTagHelper : TagHelper
    {
        private readonly ILoggerFactory _loggerFactory;
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

        public FeatureTagHelper(IFeatureService featureService, ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
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

            var tagName = output.TagName;
            output.TagName = null;

            var featureSpec = GetFeatureSpecToEvaluate();
            var isFeatureActive = await _featureService.IsFeatureActiveAsync(featureSpec);
            var suppressOutput = ShouldSuppressOutput(isFeatureActive);

            var logger = _loggerFactory.CreateLogger<FeatureSpec>();
            if (suppressOutput)
            {
                logger.SuppressOutputOfTag(tagName);
                output.SuppressOutput();
            }
            else
            {
                logger.DoNotSuppressOutputOfTag(tagName);
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

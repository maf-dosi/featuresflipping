using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace MAF.FeaturesFlipping.AspNetCore
{
    public class FeatureFilterAttribute : Attribute, IFilterFactory
    {
        public string Application { get; }
        public string Scope { get; }
        public string FeatureName { get; }

        public FeatureFilterAttribute(string application, string scope, string featureName)
        {
            Application = application;
            Scope = scope;
            FeatureName = featureName;
        }

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            var featureService = serviceProvider.GetRequiredService<IFeatureService>();
            return new FeatureResourceFilter(featureService, new FeatureSpec(Application, Scope, FeatureName));
        }

        public bool IsReusable => false;

        private class FeatureResourceFilter : IAsyncResourceFilter
        {
            private readonly IFeatureService _featureService;
            private readonly FeatureSpec _featureSpec;

            public FeatureResourceFilter(IFeatureService featureService, FeatureSpec featureSpec)
            {
                _featureService = featureService ?? throw new ArgumentNullException(nameof(featureService));
                _featureSpec = featureSpec;
            }

            public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
            {
                if (!await _featureService.IsFeatureActiveAsync(_featureSpec))
                {
                    context.Result = new NotFoundResult();
                }
                else
                {
                    await next();
                }
            }
        }
    }
}

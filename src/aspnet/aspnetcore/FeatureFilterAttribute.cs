using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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
            var featureSpec = new FeatureSpec(Application, Scope, FeatureName);
            var logger = serviceProvider.GetRequiredService<ILogger<FeatureSpec>>();
            logger.CreateFeatureResourceFilterForFeatureSpec(featureSpec);
            return new FeatureResourceFilter(featureService, featureSpec, logger);
        }

        public bool IsReusable => false;

        private class FeatureResourceFilter : IAsyncResourceFilter
        {
            private readonly IFeatureService _featureService;
            private readonly FeatureSpec _featureSpec;
            private readonly ILogger<FeatureSpec> _logger;

            public FeatureResourceFilter(IFeatureService featureService, FeatureSpec featureSpec, ILogger<FeatureSpec> logger)
            {
                _featureService = featureService ?? throw new ArgumentNullException(nameof(featureService));
                _featureSpec = featureSpec;
                _logger = logger;
            }

            public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
            {
                using (_logger.CreateScopeForRequest(context.HttpContext.Request.Path))
                {
                    var isFeatureActive = await _featureService.IsFeatureActiveAsync(_featureSpec);
                    using (_logger.CreateScopeWithFeatureSpec(_featureSpec))
                    {
                        if (isFeatureActive)
                        {
                            _logger.ContinueExecutionOfTheResource();
                            await next();
                        }
                        else
                        {
                            _logger.SkipExecutionOfTheResource();
                            context.Result = new NotFoundResult();
                        }
                    }
                }
            }
        }
    }
}

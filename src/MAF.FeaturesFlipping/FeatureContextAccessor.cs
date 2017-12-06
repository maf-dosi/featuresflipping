using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MAF.FeaturesFlipping.Extensibility.Activators;
using MAF.FeaturesFlipping.Extensibility.FeatureContext;
using Microsoft.Extensions.Logging;

namespace MAF.FeaturesFlipping
{
    internal sealed class FeatureContextAccessor : IFeatureContextAccessor
    {
        private readonly ILogger<FeatureSpec> _logger;
        private readonly IEnumerable<IFeatureContextPartFactory> _featureContextPartFactories;
        private readonly IServiceProvider _serviceProvider;
        private readonly AsyncLazy<IFeatureContext> _featureContextLazy;
        private bool _isDisposed;

        public FeatureContextAccessor(ILogger<FeatureSpec> logger, IServiceProvider serviceProvider, IEnumerable<IFeatureContextPartFactory> featureContextPartFactories)
        {
            _logger = logger;
            _featureContextPartFactories = featureContextPartFactories;
            _serviceProvider = serviceProvider;
            _featureContextLazy = new AsyncLazy<IFeatureContext>(async () => await CreateFeatureContextAsync());
        }

        public async Task<IFeatureContext> GetCurrentFeatureContextAsync()
        {
            _logger.GetCurrentFeatureContext();
            return await _featureContextLazy;
        }
        private async Task<IFeatureContext> CreateFeatureContextAsync()
        {
            _logger.CreateFeatureContext(_featureContextPartFactories.Count());
            var featureContext = new FeatureContext(_serviceProvider);
            foreach (var featureContextPartFactory in _featureContextPartFactories)
            {
                await featureContextPartFactory.AddFeatureContextPartAsync(featureContext);
            }

            _logger.ReturnNewlyCreatedFeatureContext();
            return featureContext;
        }

        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }
            _logger.StartDisposingFeatureContext();
            var featureContext = _featureContextLazy.Value.Result;
            foreach (var featureContextPartFactory in _featureContextPartFactories)
            {
                featureContextPartFactory.ReleaseFeatureContextPart(featureContext);
            }
            _logger.EndDisposingFeatureContext();
            _isDisposed = true;
        }
    }
}
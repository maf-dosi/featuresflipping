﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MAF.FeaturesFlipping.Extensibility.Activators;
using MAF.FeaturesFlipping.Extensibility.FeatureContext;

namespace MAF.FeaturesFlipping
{
    internal sealed class FeatureContextAccessor : IFeatureContextAccessor
    {
        private readonly IEnumerable<IFeatureContextPartFactory> _featureContextPartFactories;
        private readonly IServiceProvider _serviceProvider;
        private readonly AsyncLazy<IFeatureContext> _featureContextLazy;
        private bool _isDisposed;

        public FeatureContextAccessor(IEnumerable<IFeatureContextPartFactory> featureContextPartFactories, IServiceProvider serviceProvider)
        {
            _featureContextPartFactories = featureContextPartFactories;
            _serviceProvider = serviceProvider;
            _featureContextLazy = new AsyncLazy<IFeatureContext>(async () => await CreateFeatureContextAsync());
        }

        public async Task<IFeatureContext> GetCurrentFeatureContextAsync()
        {
            return await _featureContextLazy;
        }
        private async Task<IFeatureContext> CreateFeatureContextAsync()
        {
            var featureContext = new FeatureContext(_serviceProvider);
            foreach (var featureContextPartFactory in _featureContextPartFactories)
            {
                await featureContextPartFactory.AddFeatureContextPartAsync(featureContext);
            }
            return featureContext;
        }

        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }
            var featureContext = _featureContextLazy.Value.Result;
            foreach (var featureContextPartFactory in _featureContextPartFactories)
            {
                featureContextPartFactory.ReleaseFeatureContextPart(featureContext);
            }
            _isDisposed = true;
        }
    }
}
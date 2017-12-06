using System;
using Microsoft.Extensions.Logging;

namespace MAF.FeaturesFlipping
{
    internal static class LoggingExtensions
    {
        private static readonly Func<ILogger, FeatureSpec, IDisposable> _createScopeWithFeatureSpec;
        private static readonly Action<ILogger, Exception> _startComputationOfFeatureActivationStatus;
        private static readonly Action<ILogger, string, Exception> _searchFeatureFromMemoryCache;
        private static readonly Action<ILogger, string, Exception> _putFeatureInMemoryCache;
        private static readonly Action<ILogger, Exception> _foundFeatureInMemoryCache;
        private static readonly Action<ILogger, Exception> _putActivationStatusInCache;
        private static readonly Action<ILogger, Exception> _getValueFromScopedCache;
        private static readonly Action<ILogger, bool, Exception> _activationStatusForFeatureIs;
        private static readonly Action<ILogger, Exception> _unableToFindAndComputeFeature;
        private static readonly Action<ILogger, int, Exception> _startGettingAllFeatures;
        private static readonly Action<ILogger, Exception> _endGettingAllFeatures;
        private static readonly Action<ILogger, Exception> _startComputingFeatureActivationStatus;
        private static readonly Action<ILogger, bool, Exception> _featureActivationStatusComputed;
        private static readonly Action<ILogger, Exception> _disposeCurrentFeatureContext;
        private static readonly Action<ILogger, Exception> _getCurrentFeatureContext;
        private static readonly Action<ILogger, int, Exception> _createFeatureContext;
        private static readonly Action<ILogger, Exception> _returnNewlyCreatedFeatureContext;
        private static readonly Action<ILogger, Exception> _startDisposingFeatureContext;
        private static readonly Action<ILogger, Exception> _endDisposingFeatureContext;

        static LoggingExtensions()
        {
            int eventId = 1;
            _createScopeWithFeatureSpec = LoggerMessage.DefineScope<FeatureSpec>("FeatureSpec: {featureSpec}");
            _startComputationOfFeatureActivationStatus = LoggerMessage.Define(
                LogLevel.Information,
                new EventId(eventId++, nameof(StartComputationOfFeatureActivationStatus)),
                "Start computation of feature.");
            _searchFeatureFromMemoryCache = LoggerMessage.Define<string>(
                LogLevel.Debug,
                new EventId(eventId++, nameof(SearchFeatureFromMemoryCache)),
                "Search feature from memory cache with key '{cacheKey}'.");
            _putFeatureInMemoryCache = LoggerMessage.Define<string>(
                LogLevel.Debug,
                new EventId(eventId++, nameof(PutFeatureInMemoryCache)),
                "Put features in memory cache with key '{cacheKey}'.");
            _foundFeatureInMemoryCache = LoggerMessage.Define(
                LogLevel.Debug,
                new EventId(eventId++, nameof(FoundFeatureInMemoryCache)),
                "Found features in cache.");
            _putActivationStatusInCache = LoggerMessage.Define(
                LogLevel.Debug,
                new EventId(eventId++, nameof(PutActivationStatusInCache)),
                "Put activation status in cache.");
            _getValueFromScopedCache = LoggerMessage.Define(
                LogLevel.Debug,
                new EventId(eventId++, nameof(GetValueFromScopedCache)),
                "Get value from scoped cache.");
            _activationStatusForFeatureIs = LoggerMessage.Define<bool>(
                LogLevel.Information,
                new EventId(eventId++, nameof(ActivationStatusForFeatureIs)),
                "Activation status for feature is '{isFeatureActive}'.");
            _unableToFindAndComputeFeature = LoggerMessage.Define(
                LogLevel.Warning,
                new EventId(eventId++, nameof(UnableToFindAndComputeFeature)),
                "Unable to find and compute activation status. Please consider open an issue at https://github.com/maf-dosi/featuresflipping/issues.");
            _startGettingAllFeatures = LoggerMessage.Define<int>(
                LogLevel.Debug,
                new EventId(eventId++, nameof(StartGettingAllFeatures)),
                "Start getting all features from {numberOfFeatureActivators} feature activators.");
            _endGettingAllFeatures = LoggerMessage.Define(
                LogLevel.Debug,
                new EventId(eventId++, nameof(EndGettingAllFeatures)),
                "End getting all features.");
            _startComputingFeatureActivationStatus = LoggerMessage.Define(
                LogLevel.Information,
                new EventId(eventId++, nameof(StartComputingFeatureActivationStatus)),
                "Start computing feature activation status for feature.");
            _featureActivationStatusComputed = LoggerMessage.Define<bool>(
                LogLevel.Information,
                new EventId(eventId++, nameof(FeatureActivationStatusComputed)),
                "Feature activation status computed for feature. Result is: '{featureActive}'.");
            _disposeCurrentFeatureContext = LoggerMessage.Define(
                LogLevel.Debug,
                new EventId(eventId++, nameof(DisposeCurrentFeatureContext)),
                "Dispose current feature context.");
            _getCurrentFeatureContext = LoggerMessage.Define(
                LogLevel.Debug,
                new EventId(eventId++, nameof(GetCurrentFeatureContext)),
                "Get current feature context.");
            _createFeatureContext = LoggerMessage.Define<int>(
                LogLevel.Debug,
                new EventId(eventId++, nameof(CreateFeatureContext)),
                "Create a new context with {numberOfFeatureContextPartFactory} feature context part factories.");
            _returnNewlyCreatedFeatureContext = LoggerMessage.Define(
                LogLevel.Debug,
                new EventId(eventId++, nameof(CreateFeatureContext)),
                "Return newly created context.");
            _startDisposingFeatureContext = LoggerMessage.Define(
                LogLevel.Debug,
                new EventId(eventId++, nameof(StartDisposingFeatureContext)),
                "Start disposing context.");
            _endDisposingFeatureContext = LoggerMessage.Define(
                LogLevel.Debug,
                new EventId(eventId++, nameof(EndDisposingFeatureContext)),
                "End disposing context.");
        }

        public static IDisposable CreateScopeWithFeatureSpec(this ILogger logger, FeatureSpec featureSpec)
        {
            return _createScopeWithFeatureSpec(logger, featureSpec);
        }
        public static void StartComputationOfFeatureActivationStatus(this ILogger logger)
        {
            _startComputationOfFeatureActivationStatus(logger, null);
        }
        public static void SearchFeatureFromMemoryCache(this ILogger logger, string cacheKey)
        {
            _searchFeatureFromMemoryCache(logger, cacheKey, null);
        }
        public static void PutFeatureInMemoryCache(this ILogger logger, string cacheKey)
        {
            _putFeatureInMemoryCache(logger, cacheKey, null);
        }
        public static void FoundFeatureInMemoryCache(this ILogger logger)
        {
            _foundFeatureInMemoryCache(logger, null);
        }
        public static void PutActivationStatusInCache(this ILogger logger)
        {
            _putActivationStatusInCache(logger, null);
        }
        public static void GetValueFromScopedCache(this ILogger logger)
        {
            _getValueFromScopedCache(logger, null);
        }
        public static void ActivationStatusForFeatureIs(this ILogger logger, bool activationResult)
        {
            _activationStatusForFeatureIs(logger, activationResult, null);
        }
        public static void UnableToFindAndComputeFeature(this ILogger logger)
        {
            _unableToFindAndComputeFeature(logger, null);
        }
        public static void StartGettingAllFeatures(this ILogger logger, int numberOfFeatureActivators)
        {
            _startGettingAllFeatures(logger, numberOfFeatureActivators, null);
        }
        public static void EndGettingAllFeatures(this ILogger logger)
        {
            _endGettingAllFeatures(logger, null);
        }
        public static void StartComputingFeatureActivationStatus(this ILogger logger)
        {
            _startComputingFeatureActivationStatus(logger, null);
        }
        public static void FeatureActivationStatusComputed(this ILogger logger, bool isFeatureActive)
        {
            _featureActivationStatusComputed(logger, isFeatureActive, null);
        }
        public static void DisposeCurrentFeatureContext(this ILogger logger)
        {
            _disposeCurrentFeatureContext(logger, null);
        }
        public static void GetCurrentFeatureContext(this ILogger logger)
        {
            _getCurrentFeatureContext(logger, null);
        }
        public static void CreateFeatureContext(this ILogger logger, int numberOfFeatureContextPartFactory)
        {
            _createFeatureContext(logger, numberOfFeatureContextPartFactory, null);
        }
        public static void ReturnNewlyCreatedFeatureContext(this ILogger logger)
        {
            _returnNewlyCreatedFeatureContext(logger, null);
        }
        public static void StartDisposingFeatureContext(this ILogger logger)
        {
            _startDisposingFeatureContext(logger, null);
        }
        public static void EndDisposingFeatureContext(this ILogger logger)
        {
            _endDisposingFeatureContext(logger, null);
        }
    }
}
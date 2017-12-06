using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace MAF.FeaturesFlipping.AspNetCore
{
    internal static class LoggingExtensions
    {
        private static readonly Action<ILogger, FeatureSpec, Exception> _createFeatureResourceFilterForFeatureSpec;
        private static readonly Func<ILogger, PathString, IDisposable> _createScopeForRequest;
        private static readonly Action<ILogger, Exception> _skipExecutionOfTheResource;
        private static readonly Action<ILogger, Exception> _continueExecutionOfTheResource;
        private static readonly Func<ILogger, string, IDisposable> _createScopeForTagName;
        private static readonly Action<ILogger, Exception> _suppressOutputOfTag;
        private static readonly Action<ILogger, Exception> _doNotSuppressOutputOfTag;
        private static readonly Action<ILogger, string, string, string, Exception> _getFeatureSpecFromProperties;
        private static readonly Action<ILogger, FeatureSpec, Exception> _getFeatureSpecFromProperty;

        static LoggingExtensions()
        {
            var eventId = 1;
            _createFeatureResourceFilterForFeatureSpec = LoggerMessage.Define<FeatureSpec>(
                LogLevel.Debug,
                new EventId(eventId++, nameof(CreateFeatureResourceFilterForFeatureSpec)),
                "Create FeatureResourceFilter for '{FeatureSpec}'.");
            _createScopeForRequest = LoggerMessage.DefineScope<PathString>("{RequestPath}");
            _skipExecutionOfTheResource = LoggerMessage.Define(
                LogLevel.Information,
                new EventId(eventId++, nameof(SkipExecutionOfTheResource)),
                "Skip execution of the resource.");
            _continueExecutionOfTheResource = LoggerMessage.Define(
                LogLevel.Information,
                new EventId(eventId++, nameof(ContinueExecutionOfTheResource)),
                "Continue execution of the resource.");
            _createScopeForTagName = LoggerMessage.DefineScope<string>("{TagName}");
            _suppressOutputOfTag = LoggerMessage.Define(
                LogLevel.Information,
                new EventId(eventId++, nameof(SuppressOutputOfTag)),
                "Suppress output of tag.");
            _doNotSuppressOutputOfTag = LoggerMessage.Define(
                LogLevel.Information,
                new EventId(eventId++, nameof(DoNotSuppressOutputOfTag)),
                "Do not suppress output of tag.");
            _getFeatureSpecFromProperties = LoggerMessage.Define<string, string, string>(
                LogLevel.Information,
                new EventId(eventId++, nameof(GetFeatureSpecFromProperties)),
                "Get FeatureSpec from properties Application='{Application}', Scope='{Scope}', FeatureName='{FeatureName}'.");
            _getFeatureSpecFromProperty = LoggerMessage.Define<FeatureSpec>(
                LogLevel.Information,
                new EventId(eventId++, nameof(GetFeatureSpecFromProperty)),
                "Get FeatureSpec from property FeatureSpec='{FeatureSpec}'.");
        }

        public static void CreateFeatureResourceFilterForFeatureSpec(this ILogger logger, FeatureSpec featureSpec)
        {
            _createFeatureResourceFilterForFeatureSpec(logger, featureSpec, null);
        }
        public static IDisposable CreateScopeForRequest(this ILogger logger, PathString requestPath)
        {
            return _createScopeForRequest(logger, requestPath);
        }
        public static void SkipExecutionOfTheResource(this ILogger logger)
        {
            _skipExecutionOfTheResource(logger, null);
        }
        public static void ContinueExecutionOfTheResource(this ILogger logger)
        {
            _continueExecutionOfTheResource(logger, null);
        }
        public static IDisposable CreateScopeForTagName(this ILogger logger, string tagName)
        {
            return _createScopeForTagName(logger, tagName);
        }
        public static void SuppressOutputOfTag(this ILogger logger)
        {
            _suppressOutputOfTag(logger, null);
        }
        public static void DoNotSuppressOutputOfTag(this ILogger logger)
        {
            _doNotSuppressOutputOfTag(logger, null);
        }
        public static void GetFeatureSpecFromProperties(this ILogger logger, string application, string scope, string featureName)
        {
            _getFeatureSpecFromProperties(logger, application, scope, featureName, null);
        }
        public static void GetFeatureSpecFromProperty(this ILogger logger, FeatureSpec featureSpec)
        {
            _getFeatureSpecFromProperty(logger, featureSpec, null);
        }
    }
}
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
        private static readonly Action<ILogger, string, Exception> _suppressOutputOfTag;
        private static readonly Action<ILogger, string, Exception> _doNotSuppressOutputOfTag;

        static LoggingExtensions()
        {
            var eventId = 1;
            _createFeatureResourceFilterForFeatureSpec = LoggerMessage.Define<FeatureSpec>(
                LogLevel.Debug,
                new EventId(eventId++, nameof(CreateFeatureResourceFilterForFeatureSpec)),
                "Create FeatureResourceFilter for '{FeatureSpec}'.");
            _createScopeForRequest = LoggerMessage.DefineScope<PathString>("{FeatureSpec}");
            _skipExecutionOfTheResource = LoggerMessage.Define(
                LogLevel.Information,
                new EventId(eventId++, nameof(SkipExecutionOfTheResource)),
                "Skip execution of the resource.");
            _continueExecutionOfTheResource = LoggerMessage.Define(
                LogLevel.Information,
                new EventId(eventId++, nameof(ContinueExecutionOfTheResource)),
                "Continue execution of the resource.");
            _suppressOutputOfTag = LoggerMessage.Define<string>(
                LogLevel.Information,
                new EventId(eventId++, nameof(SuppressOutputOfTag)),
                "Suppress output of tag '{TagName}'.");
            _doNotSuppressOutputOfTag = LoggerMessage.Define<string>(
                LogLevel.Information,
                new EventId(eventId++, nameof(DoNotSuppressOutputOfTag)),
                "Do not suppress output of tag '{TagName}'.");
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
        public static void SuppressOutputOfTag(this ILogger logger, string tagName)
        {
            _suppressOutputOfTag(logger, tagName, null);
        }
        public static void DoNotSuppressOutputOfTag(this ILogger logger, string tagName)
        {
            _doNotSuppressOutputOfTag(logger, tagName, null);
        }
    }
}
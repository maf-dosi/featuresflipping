using System;
using Microsoft.Extensions.Logging;

namespace MAF.FeaturesFlipping.FeatureContext.Delegate
{
    internal static class LoggingExtensions
    {
        private static readonly Action<ILogger, Exception> _addPartToFeatureContext;
        private static readonly Action<ILogger, Exception> _releasePartToFeatureContext;

        static LoggingExtensions()
        {
            int eventId = 1;
            _addPartToFeatureContext = LoggerMessage.Define(
                LogLevel.Debug,
                new EventId(eventId++, nameof(AddPartToFeatureContext)),
                "Add part to feature context via delegate.");
            _releasePartToFeatureContext = LoggerMessage.Define(
                LogLevel.Debug,
                new EventId(eventId++, nameof(ReleasePartToFeatureContext)),
                "Release part from feature context via delegate.");
        }
        public static void AddPartToFeatureContext(this ILogger logger)
        {
            _addPartToFeatureContext(logger, null);
        }
        public static void ReleasePartToFeatureContext(this ILogger logger)
        {
            _releasePartToFeatureContext(logger, null);
        }
    }
}

using System;
using Microsoft.Extensions.Logging;

namespace MAF.FeaturesFlipping.Extensibility.Activators
{
    internal static class LoggingExtensions
    {
        private static readonly Action<ILogger, Exception> _getActivationStatus;

        static LoggingExtensions()
        {
            var eventId = 1;
            _getActivationStatus = LoggerMessage.Define(
                LogLevel.Debug,
                new EventId(eventId++, nameof(GetActivationStatus)),
                "Get activation status (false) from NotSetFeature.");
        }

        public static void GetActivationStatus(this ILogger logger)
        {
            _getActivationStatus(logger, null);
        }
    }
}
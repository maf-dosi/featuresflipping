using System;
using Microsoft.Extensions.Logging;

namespace MAF.FeaturesFlipping.Activators.Configuration
{
    internal static class LoggingExtensions
    {
        private static readonly Action<ILogger, string, Exception> _retrieveValueOfConfigurationSection;
        private static readonly Action<ILogger, Exception> _returnNotSetActivationStatus;
        private static readonly Action<ILogger, Exception> _returnActiveActivationStatus;
        private static readonly Action<ILogger, Exception> _returnInactiveActivationStatus;

        static LoggingExtensions()
        {
            var eventId = 1;
            _retrieveValueOfConfigurationSection = LoggerMessage.Define<string>(
                LogLevel.Debug,
                new EventId(eventId++, nameof(RetrieveValueOfConfigurationSection)),
                "Retrieve value of configuration section '{value}'.");
            _returnNotSetActivationStatus = LoggerMessage.Define(
                LogLevel.Information,
                new EventId(eventId++, nameof(ReturnNotSetActivationStatus)),
                "Return NotSet activation status.");
            _returnActiveActivationStatus = LoggerMessage.Define(
                LogLevel.Information,
                new EventId(eventId++, nameof(ReturnActiveActivationStatus)),
                "RetrieveReturn Active activation status.");
            _returnInactiveActivationStatus = LoggerMessage.Define(
                LogLevel.Information,
                new EventId(eventId++, nameof(ReturnInactiveActivationStatus)),
                "Return Inactive activation status.");
        }

        public static void RetrieveValueOfConfigurationSection(this ILogger logger, string value)
        {
            _retrieveValueOfConfigurationSection(logger, value, null);
        }
        public static void ReturnNotSetActivationStatus(this ILogger logger)
        {
            _returnNotSetActivationStatus(logger, null);
        }
        public static void ReturnActiveActivationStatus(this ILogger logger)
        {
            _returnActiveActivationStatus(logger, null);
        }
        public static void ReturnInactiveActivationStatus(this ILogger logger)
        {
            _returnInactiveActivationStatus(logger, null);
        }
    }
}
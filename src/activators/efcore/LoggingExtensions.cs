using System;
using MAF.FeaturesFlipping.Extensibility.Activators;
using Microsoft.Extensions.Logging;

namespace MAF.FeaturesFlipping.Activators.EntityFrameworkCore
{
    internal static class LoggingExtensions
    {
        private static readonly Action<ILogger, string, string, string, string, string, string, Exception> _createGlobalFeatureDbContextModel;
        private static readonly Action<ILogger, Exception> _getGlobalFeatureEntity;
        private static readonly Action<ILogger, FeatureActivationStatus, Exception> _featureActivationStatusForFeatureFromEntityIs;

        static LoggingExtensions()
        {
            var eventId = 1;
            _createGlobalFeatureDbContextModel = LoggerMessage.Define<string, string, string, string, string, string>(
                LogLevel.Debug,
                new EventId(eventId++, nameof(CreateGlobalFeatureDbContextModel)),
                "Create GlobalFeatureDbContext with: Schema='{Schema}', Table='{Table}', Application='{Application}', Scope='{Scope}', FeatureName='{FeatureName}', IsActive='{IsActive}'.");
            _getGlobalFeatureEntity = LoggerMessage.Define(
                LogLevel.Debug,
                new EventId(eventId++, nameof(GetGlobalFeatureEntity)),
                "Get global FeatureEntity from EFCore.");
            _featureActivationStatusForFeatureFromEntityIs = LoggerMessage.Define<FeatureActivationStatus>(
                LogLevel.Debug,
                new EventId(eventId++, nameof(CreateGlobalFeatureDbContextModel)),
                "FeatureActivationStatus for FeatureFromEntity is {FeatureActivationStatus}.");
        }

        public static void CreateGlobalFeatureDbContextModel(this ILogger logger, string schema, string table, string application, string scope, string featureName, string isActive)
        {
            _createGlobalFeatureDbContextModel(logger, schema, table, application, scope, featureName, isActive, null);
        }
        public static void GetGlobalFeatureEntity(this ILogger logger)
        {
            _getGlobalFeatureEntity(logger, null);
        }
        public static void FeatureActivationStatusForFeatureFromEntityIs(this ILogger logger, FeatureActivationStatus featureActivationStatus)
        {
            _featureActivationStatusForFeatureFromEntityIs(logger, featureActivationStatus, null);
        }
    }
}
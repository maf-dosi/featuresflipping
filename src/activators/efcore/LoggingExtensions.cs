using System;
using MAF.FeaturesFlipping.Extensibility.Activators;
using Microsoft.Extensions.Logging;

namespace MAF.FeaturesFlipping.Activators.EntityFrameworkCore
{
    internal static class LoggingExtensions
    {
        private static readonly Action<ILogger, string, string, string, string, string, string, Exception> _createGlobalFeatureDbContextModel;
        private static readonly Action<ILogger, Exception> _getGlobalFeatureEntity;
        private static readonly Action<ILogger, Type, Exception> _noFeatureEntityFound;
        private static readonly Action<ILogger, int, Exception> _featureEntityIsNotSet;
        private static readonly Action<ILogger, int, bool, Exception> _featureEntityIsActiveValue;
        private static readonly Action<ILogger, FeatureActivationStatus, Exception> _featureActivationStatusForFeatureFromEntityIs;
        private static readonly Action<ILogger, string, string, string, string, string, string, Exception> _createSpecificFeatureDbContextModel;
        private static readonly Action<ILogger, Exception> _loadSpecificFeatureEntity;

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
            _noFeatureEntityFound = LoggerMessage.Define<Type>(
                LogLevel.Debug,
                new EventId(eventId++, nameof(NoFeatureEntityFound)),
                "No feature entity of type '{FeatureEntityType}' found.");
            _featureEntityIsNotSet = LoggerMessage.Define<int>(
                LogLevel.Debug,
                new EventId(eventId++, nameof(FeatureEntityIsNotSet)),
                "Feature entity ({FeatureEntityId}) has no IsActive value.");
            _featureEntityIsActiveValue = LoggerMessage.Define<int, bool>(
                    LogLevel.Debug,
                    new EventId(eventId++, nameof(FeatureEntityIsActiveValue)),
                    "Feature entity ({FeatureEntityId}) has IsActive value = '{IsActive}'.");
            _featureActivationStatusForFeatureFromEntityIs = LoggerMessage.Define<FeatureActivationStatus>(
                LogLevel.Debug,
                new EventId(eventId++, nameof(CreateGlobalFeatureDbContextModel)),
                "FeatureActivationStatus for FeatureFromEntity is {FeatureActivationStatus}.");
            _createSpecificFeatureDbContextModel = LoggerMessage.Define<string, string, string, string, string, string>(
                LogLevel.Debug,
                new EventId(eventId++, nameof(CreateSpecificFeatureDbContextModel)),
                "Create GlobalFeatureDbContext with: Schema.Table='{SchemaAndTable}', Application='{Application}', Scope='{Scope}', FeatureName='{FeatureName}', OtherColumn='{OtherColumn}' IsActive='{IsActive}'.");
            _loadSpecificFeatureEntity = LoggerMessage.Define(
                LogLevel.Debug,
                new EventId(eventId++, nameof(LoadSpecificFeatureEntity)),
                "Load specific FeatureEntity from EFCore.");
        }

        public static void CreateGlobalFeatureDbContextModel(this ILogger logger, string schema, string table, string application, string scope, string featureName, string isActive)
        {
            _createGlobalFeatureDbContextModel(logger, schema, table, application, scope, featureName, isActive, null);
        }
        public static void GetGlobalFeatureEntity(this ILogger logger)
        {
            _getGlobalFeatureEntity(logger, null);
        }
        public static void NoFeatureEntityFound(this ILogger logger, Type featureEntityType)
        {
            _noFeatureEntityFound(logger, featureEntityType, null);
        }
        public static void FeatureEntityIsNotSet(this ILogger logger, int featureEntityId)
        {
            _featureEntityIsNotSet(logger, featureEntityId, null);
        }
        public static void FeatureEntityIsActiveValue(this ILogger logger, int featureEntityId, bool isActive)
        {
            _featureEntityIsActiveValue(logger, featureEntityId, isActive, null);
        }
        public static void FeatureActivationStatusForFeatureFromEntityIs(this ILogger logger, FeatureActivationStatus featureActivationStatus)
        {
            _featureActivationStatusForFeatureFromEntityIs(logger, featureActivationStatus, null);
        }
        public static void CreateSpecificFeatureDbContextModel(this ILogger logger, string schema, string table, string application, string scope, string featureName, string otherColumn, string isActive)
        {
            _createSpecificFeatureDbContextModel(logger, $"{schema}.{table}", application, scope, featureName, otherColumn, isActive, null);
        }
        public static void LoadSpecificFeatureEntity(this ILogger logger)
        {
            _loadSpecificFeatureEntity(logger, null);
        }
    }
}
using System;
using System.Linq.Expressions;
using MAF.FeaturesFlipping.Extensibility.Activators;
using Microsoft.EntityFrameworkCore;

namespace MAF.FeaturesFlipping.Activators.EntityFrameworkCore.Specific
{
    public sealed class SpecificDbContextConfiguration<TOtherColumn>
    {
        private readonly Action<DbContextOptionsBuilder> _dbContextBuilderAction;

        private readonly Func<IFeatureContext, Expression<Func<SpecificFeatureEntity<TOtherColumn>, bool>>>
            _specificFeatureFilterWithContext;

        private string _applicationColumnName;
        private string _featureColumnName;
        private string _isActiveColumnName;
        private string _otherColumnName;
        private string _schema;
        private string _scopeColumnName;
        private string _tableName;

        public SpecificDbContextConfiguration(Action<DbContextOptionsBuilder> dbContextBuilderAction,
            string otherColumnName,
            Func<IFeatureContext, Expression<Func<SpecificFeatureEntity<TOtherColumn>, bool>>>
                specificFeatureFilterWithContext)
        {
            _specificFeatureFilterWithContext = specificFeatureFilterWithContext ??
                                                throw new ArgumentNullException(
                                                    nameof(specificFeatureFilterWithContext));
            Schema("Feature")
                .TableName("SpecificFeature")
                .ApplicationColumnName("Application")
                .ScopeColumnName("Scope")
                .FeatureColumnName("Feature")
                .IsActiveColumnName("IsActive")
                .OtherColumnName(otherColumnName);
            _dbContextBuilderAction = dbContextBuilderAction ?? (_ => { });
        }

        public string Schema()
        {
            return _schema;
        }

        public SpecificDbContextConfiguration<TOtherColumn> Schema(string schema)
        {
            _schema = schema ?? _schema;
            return this;
        }

        public string TableName()
        {
            return _tableName;
        }

        public SpecificDbContextConfiguration<TOtherColumn> TableName(string tableName)
        {
            if (!string.IsNullOrWhiteSpace(tableName))
            {
                _tableName = tableName;
            }
            return this;
        }

        public string ApplicationColumnName()
        {
            return _applicationColumnName;
        }

        public SpecificDbContextConfiguration<TOtherColumn> ApplicationColumnName(string applicationColumnName)
        {
            if (!string.IsNullOrWhiteSpace(applicationColumnName))
            {
                _applicationColumnName = applicationColumnName;
            }
            return this;
        }

        public string ScopeColumnName()
        {
            return _scopeColumnName;
        }

        public SpecificDbContextConfiguration<TOtherColumn> ScopeColumnName(string scopeColumnName)
        {
            if (!string.IsNullOrWhiteSpace(scopeColumnName))
            {
                _scopeColumnName = scopeColumnName;
            }
            return this;
        }

        public string FeatureColumnName()
        {
            return _featureColumnName;
        }

        public SpecificDbContextConfiguration<TOtherColumn> FeatureColumnName(string featureColumnName)
        {
            if (!string.IsNullOrWhiteSpace(featureColumnName))
            {
                _featureColumnName = featureColumnName;
            }
            return this;
        }

        public string IsActiveColumnName()
        {
            return _isActiveColumnName;
        }

        public SpecificDbContextConfiguration<TOtherColumn> IsActiveColumnName(string isActiveColumnName)
        {
            if (!string.IsNullOrWhiteSpace(isActiveColumnName))
            {
                _isActiveColumnName = isActiveColumnName;
            }
            return this;
        }

        public string OtherColumnName()
        {
            return _otherColumnName;
        }

        public SpecificDbContextConfiguration<TOtherColumn> OtherColumnName(string otherColumnName)
        {
            if (!string.IsNullOrWhiteSpace(otherColumnName))
            {
                _otherColumnName = otherColumnName;
            }
            return this;
        }

        internal Expression<Func<SpecificFeatureEntity<TOtherColumn>, bool>> FilterFeatureWithScope(
            IFeatureContext featureContext)
        {
            return _specificFeatureFilterWithContext(featureContext);
        }

        internal void ConfigureDbContext(DbContextOptionsBuilder optionsBuilder)
        {
            _dbContextBuilderAction(optionsBuilder);
        }
    }
}
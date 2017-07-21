using System;
using Microsoft.EntityFrameworkCore;

namespace MAF.FeaturesFlipping.Activators.EntityFrameworkCore.Global
{
    public sealed class GlobalDbContextConfiguration
    {
        private readonly Action<DbContextOptionsBuilder> _dbContextBuilderAction;
        private string _applicationColumnName;
        private string _featureColumnName;
        private string _isActiveColumnName;
        private string _schema;
        private string _scopeColumnName;
        private string _tableName;

        public GlobalDbContextConfiguration(Action<DbContextOptionsBuilder> dbContextBuilderAction)
        {
            Schema("Feature")
                .TableName("GlobalFeature")
                .ApplicationColumnName("Application")
                .ScopeColumnName("Scope")
                .FeatureColumnName("Feature")
                .IsActiveColumnName("IsActive");
            _dbContextBuilderAction = dbContextBuilderAction ?? (_ => { });
        }

        public string Schema()
        {
            return _schema;
        }

        public GlobalDbContextConfiguration Schema(string schema)
        {
            _schema = schema ?? _schema;
            return this;
        }

        public string TableName()
        {
            return _tableName;
        }

        public GlobalDbContextConfiguration TableName(string tableName)
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

        public GlobalDbContextConfiguration ApplicationColumnName(string applicationColumnName)
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

        public GlobalDbContextConfiguration ScopeColumnName(string scopeColumnName)
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

        public GlobalDbContextConfiguration FeatureColumnName(string featureColumnName)
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

        public GlobalDbContextConfiguration IsActiveColumnName(string isActiveColumnName)
        {
            if (!string.IsNullOrWhiteSpace(isActiveColumnName))
            {
                _isActiveColumnName = isActiveColumnName;
            }
            return this;
        }

        internal void ConfigureDbContext(DbContextOptionsBuilder optionsBuilder)
        {
            _dbContextBuilderAction(optionsBuilder);
        }
    }
}
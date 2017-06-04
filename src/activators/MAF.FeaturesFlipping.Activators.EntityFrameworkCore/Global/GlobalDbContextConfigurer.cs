using System;
using Microsoft.EntityFrameworkCore;

namespace MAF.FeaturesFlipping.Activators.EntityFrameworkCore.Global
{
    public sealed class GlobalDbContextConfigurer
    {
        private readonly Action<DbContextOptionsBuilder> _dbContextBuilderAction;
        private string _applicationColumnName;
        private string _featureColumnName;
        private string _isActiveColumnName;
        private string _schema;
        private string _scopeColumnName;
        private string _tableName;

        public GlobalDbContextConfigurer(Action<DbContextOptionsBuilder> dbContextBuilderAction)
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

        public GlobalDbContextConfigurer Schema(string schema)
        {
            _schema = schema ?? _schema;
            return this;
        }

        public string TableName()
        {
            return _tableName;
        }

        public GlobalDbContextConfigurer TableName(string tableName)
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

        public GlobalDbContextConfigurer ApplicationColumnName(string applicationColumnName)
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

        public GlobalDbContextConfigurer ScopeColumnName(string scopeColumnName)
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

        public GlobalDbContextConfigurer FeatureColumnName(string featureColumnName)
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

        public GlobalDbContextConfigurer IsActiveColumnName(string isActiveColumnName)
        {
            if (!string.IsNullOrWhiteSpace(isActiveColumnName))
            {
                _isActiveColumnName = isActiveColumnName;
            }
            return this;
        }

        public void ConfigureDbContext(DbContextOptionsBuilder optionsBuilder)
        {
            _dbContextBuilderAction(optionsBuilder);
        }
    }
}
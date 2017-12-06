using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MAF.FeaturesFlipping.Activators.EntityFrameworkCore.Global
{
    public class GlobalFeatureDbContext : DbContext
    {
        private readonly GlobalDbContextConfiguration _globalDbContextConfigurer;
        private readonly ILoggerFactory _loggerFactory;

        public GlobalFeatureDbContext(GlobalDbContextConfiguration globalDbContextConfigurer, ILoggerFactory loggerFactory)
        {
            _globalDbContextConfigurer = globalDbContextConfigurer;
            _loggerFactory = loggerFactory;
        }

        public DbSet<GlobalFeatureEntity> Features { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            _globalDbContextConfigurer.ConfigureDbContext(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var featureEntityModelBuilder = modelBuilder.Entity<GlobalFeatureEntity>();
            var relationalMetadata = featureEntityModelBuilder
                .Metadata.Relational();
            relationalMetadata.Schema = _globalDbContextConfigurer.Schema();
            relationalMetadata.TableName = _globalDbContextConfigurer.TableName();
            featureEntityModelBuilder.HasKey(globalFeatureEntity => globalFeatureEntity.FeatureId);
            featureEntityModelBuilder.Property(_ => _.Application)
                .IsRequired().HasColumnName(_globalDbContextConfigurer.ApplicationColumnName());
            featureEntityModelBuilder.Property(_ => _.Scope)
                .IsRequired().HasColumnName(_globalDbContextConfigurer.ScopeColumnName());
            featureEntityModelBuilder.Property(_ => _.FeatureName)
                .IsRequired().HasColumnName(_globalDbContextConfigurer.FeatureNameColumnName());
            featureEntityModelBuilder.Property(_ => _.IsActive)
                .HasColumnName(_globalDbContextConfigurer.IsActiveColumnName());
            var logger = _loggerFactory.CreateLogger<FeatureSpec>();
            logger.CreateGlobalFeatureDbContextModel(_globalDbContextConfigurer.Schema(),
                _globalDbContextConfigurer.TableName(),
                _globalDbContextConfigurer.ApplicationColumnName(),
                _globalDbContextConfigurer.ScopeColumnName(),
                _globalDbContextConfigurer.FeatureNameColumnName(),
                _globalDbContextConfigurer.IsActiveColumnName());
        }

        internal async Task<FeatureFromEntity<GlobalFeatureEntity>> GetGlobalFeatureEntity(
            FeatureSpec featureSpec)
        {
            var logger = _loggerFactory.CreateLogger<FeatureSpec>();
            logger.GetGlobalFeatureEntity();
            var globalFeatureEntity = await Features.FirstOrDefaultAsync(
                feature => feature.Application == featureSpec.Application && feature.Scope ==
                           featureSpec.Scope && feature.FeatureName == featureSpec.FeatureName);
            var featureFromEntity = new FeatureFromEntity<GlobalFeatureEntity>(globalFeatureEntity, logger);
            return featureFromEntity;
        }
    }
}
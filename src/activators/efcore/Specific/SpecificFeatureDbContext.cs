using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MAF.FeaturesFlipping.Extensibility.Activators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MAF.FeaturesFlipping.Activators.EntityFrameworkCore.Specific
{
    public class SpecificFeatureDbContext<TOtherColumn> : DbContext
    {
        private readonly SpecificDbContextConfiguration<TOtherColumn> _specificDbContextConfigurer;
        private readonly ILoggerFactory _loggerFactory;

        public SpecificFeatureDbContext(SpecificDbContextConfiguration<TOtherColumn> specificDbContextConfigurer, ILoggerFactory loggerFactory)
        {
            _specificDbContextConfigurer = specificDbContextConfigurer;
            _loggerFactory = loggerFactory;
        }

        public DbSet<SpecificFeatureEntity<TOtherColumn>> Features { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            _specificDbContextConfigurer.ConfigureDbContext(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var featureEntityModelBuilder = modelBuilder.Entity<SpecificFeatureEntity<TOtherColumn>>();
            var relationalMetadata = featureEntityModelBuilder
                .Metadata.Relational();
            relationalMetadata.Schema = _specificDbContextConfigurer.Schema();
            relationalMetadata.TableName = _specificDbContextConfigurer.TableName();
            featureEntityModelBuilder.HasKey(specificFeatureEntity => specificFeatureEntity.FeatureId);
            featureEntityModelBuilder.Property(_ => _.Application)
                .IsRequired().HasColumnName(_specificDbContextConfigurer.ApplicationColumnName());
            featureEntityModelBuilder.Property(_ => _.Scope)
                .IsRequired().HasColumnName(_specificDbContextConfigurer.ScopeColumnName());
            featureEntityModelBuilder.Property(_ => _.FeatureName)
                .IsRequired().HasColumnName(_specificDbContextConfigurer.FeatureNameColumnName());
            featureEntityModelBuilder.Property(_ => _.IsActive)
                .HasColumnName(_specificDbContextConfigurer.IsActiveColumnName());
            featureEntityModelBuilder.Property(_ => _.OtherColumn)
                .HasColumnName(_specificDbContextConfigurer.OtherColumnName());
            var logger = _loggerFactory.CreateLogger<FeatureSpec>();
            logger.CreateSpecificFeatureDbContextModel(_specificDbContextConfigurer.Schema(),
                _specificDbContextConfigurer.TableName(),
                _specificDbContextConfigurer.ApplicationColumnName(),
                _specificDbContextConfigurer.ScopeColumnName(),
                _specificDbContextConfigurer.FeatureNameColumnName(),
                _specificDbContextConfigurer.FeatureNameColumnName(),
                _specificDbContextConfigurer.IsActiveColumnName());
        }
        internal async Task<FeatureFromEntity<SpecificFeatureEntity<TOtherColumn>>> LoadSpecificFeatureEntity(Expression<Func<SpecificFeatureEntity<TOtherColumn>, bool>> specificFeatureQuery, IFeatureContext featureContext)
        {
            var logger = _loggerFactory.CreateLogger<FeatureSpec>();
            logger.LoadSpecificFeatureEntity();

            var specificFeatureEntity = await Features.Where(specificFeatureQuery)
                .Where(_specificDbContextConfigurer.FilterFeatureWithScope(featureContext))
                .SingleOrDefaultAsync();
            var featureFromEntity = new FeatureFromEntity<SpecificFeatureEntity<TOtherColumn>>(specificFeatureEntity, logger);
            return featureFromEntity;
        }
    }
}
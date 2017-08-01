using Microsoft.EntityFrameworkCore;

namespace MAF.FeaturesFlipping.Activators.EntityFrameworkCore.Global
{
    public class GlobalFeatureDbContext : DbContext
    {
        private readonly GlobalDbContextConfiguration _globalDbContextConfigurer;

        public GlobalFeatureDbContext(GlobalDbContextConfiguration globalDbContextConfigurer)
        {
            _globalDbContextConfigurer = globalDbContextConfigurer;
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
        }
    }
}
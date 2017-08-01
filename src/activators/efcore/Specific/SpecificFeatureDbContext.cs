using Microsoft.EntityFrameworkCore;

namespace MAF.FeaturesFlipping.Activators.EntityFrameworkCore.Specific
{
    public class SpecificFeatureDbContext<TOtherColumn> : DbContext
    {
        private readonly SpecificDbContextConfiguration<TOtherColumn> _specificDbContextConfigurer;

        public SpecificFeatureDbContext(SpecificDbContextConfiguration<TOtherColumn> specificDbContextConfigurer)
        {
            _specificDbContextConfigurer = specificDbContextConfigurer;
        }

        public DbSet<SpecificFeatureEntity<TOtherColumn>> Features { get; set; }

        internal SpecificDbContextConfiguration<TOtherColumn> SpecificConfiguration => _specificDbContextConfigurer;
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
            featureEntityModelBuilder.HasKey(globalFeatureEntity => globalFeatureEntity.FeatureId);
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
        }
    }
}
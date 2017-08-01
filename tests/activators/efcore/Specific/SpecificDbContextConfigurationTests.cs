using MAF.FeaturesFlipping.Activators.EntityFrameworkCore.Specific;
using Xunit;

namespace MAF.FeaturesFlipping.Activators.EntityFrameworkCore.UnitTests.Specific
{
    [Trait("Category", "UnitTest")]
    public partial class SpecificDbContextConfigurationTests
    {
        [Fact]
        public void The_Constructor_Sets_The_Default_Value()
        {
            // Arrange
            var expectedSchema = "Feature";
            var expectedTableName = "SpecificFeature";
            var expectedApplicationColumnName = "Application";
            var expectedScopeColumnName = "Scope";
            var expectedFeatureNameColumnName = "FeatureName";
            var expectedIsActiveColumnName = "IsActive";
            var expectedOtherColumnName = "Other";

            // Act
            var actual = new SpecificDbContextConfiguration<string>(_ => { }, expectedOtherColumnName, _ => feature => false);

            // Assert
            Assert.Equal(expectedSchema, actual.Schema());
            Assert.Equal(expectedTableName, actual.TableName());
            Assert.Equal(expectedApplicationColumnName, actual.ApplicationColumnName());
            Assert.Equal(expectedScopeColumnName, actual.ScopeColumnName());
            Assert.Equal(expectedFeatureNameColumnName, actual.FeatureNameColumnName());
            Assert.Equal(expectedIsActiveColumnName, actual.IsActiveColumnName());
            Assert.Equal(expectedOtherColumnName, actual.OtherColumnName());
        }
    }
}
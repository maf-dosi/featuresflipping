using Xunit;

namespace MAF.FeaturesFlipping.Activators.EntityFrameworkCore.Global
{
    [Trait("Category", "UnitTest")]
    public partial class GlobalDbContextConfigurationTests
    {
        [Fact]
        public void The_Constructor_Sets_The_Default_Value()
        {
            // Arrange
            var expectedSchema = "Feature";
            var expectedTableName = "GlobalFeature";
            var expectedApplicationColumnName = "Application";
            var expectedScopeColumnName = "Scope";
            var expectedFeatureNameColumnName = "FeatureName";
            var expectedIsActiveColumnName = "IsActive";

            // Act
            var actual = new GlobalDbContextConfiguration(_ => { });

            // Assert
            Assert.Equal(expectedSchema, actual.Schema());
            Assert.Equal(expectedTableName, actual.TableName());
            Assert.Equal(expectedApplicationColumnName, actual.ApplicationColumnName());
            Assert.Equal(expectedScopeColumnName, actual.ScopeColumnName());
            Assert.Equal(expectedFeatureNameColumnName, actual.FeatureNameColumnName());
            Assert.Equal(expectedIsActiveColumnName, actual.IsActiveColumnName());
        }
    }
}
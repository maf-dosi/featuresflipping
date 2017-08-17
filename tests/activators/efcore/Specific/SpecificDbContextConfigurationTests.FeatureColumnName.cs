using MAF.FeaturesFlipping.Activators.EntityFrameworkCore.Specific;
using Xunit;

namespace MAF.FeaturesFlipping.Activators.EntityFrameworkCore.UnitTests.Specific
{
    public partial class SpecificDbContextConfigurationTests
    {
        [Trait("Category", "UnitTest")]
        public class FeatureNameColumnName
        {
            [Fact]
            public void Calling_FeatureNameColumnName_With_A_New_Name_Changes_The_FeatureNameColumnName()
            {
                // Arrange
                var expectedSchema = "Feature";
                var expectedTableName = "SpecificFeature";
                var expectedApplicationColumnName = "Application";
                var expectedScopeColumnName = "Scope";
                var expectedFeatureNameColumnName = "NewFeatureColumn";
                var expectedIsActiveColumnName = "IsActive";
                var actual = new SpecificDbContextConfiguration<object>(_ => { }, "OtherColumnName", _ => feature => false);

                // Act
                actual.FeatureNameColumnName(expectedFeatureNameColumnName);

                // Assert
                Assert.Equal(expectedSchema, actual.Schema());
                Assert.Equal(expectedTableName, actual.TableName());
                Assert.Equal(expectedApplicationColumnName, actual.ApplicationColumnName());
                Assert.Equal(expectedScopeColumnName, actual.ScopeColumnName());
                Assert.Equal(expectedFeatureNameColumnName, actual.FeatureNameColumnName());
                Assert.Equal(expectedIsActiveColumnName, actual.IsActiveColumnName());
            }

            [Fact]
            public void Calling_FeatureNameColumnName_With_A_Null_Doesnt_Change_The_FeatureNameColumnName()
            {
                // Arrange
                var expectedSchema = "Feature";
                var expectedTableName = "SpecificFeature";
                var expectedApplicationColumnName = "Application";
                var expectedScopeColumnName = "Scope";
                var expectedFeatureNameColumnName = "FeatureName";
                var expectedIsActiveColumnName = "IsActive";
                var actual = new SpecificDbContextConfiguration<object>(_ => { }, "OtherColumnName", _ => feature => false);

                // Act
                actual.FeatureNameColumnName(null);

                // Assert
                Assert.Equal(expectedSchema, actual.Schema());
                Assert.Equal(expectedTableName, actual.TableName());
                Assert.Equal(expectedApplicationColumnName, actual.ApplicationColumnName());
                Assert.Equal(expectedScopeColumnName, actual.ScopeColumnName());
                Assert.Equal(expectedFeatureNameColumnName, actual.FeatureNameColumnName());
                Assert.Equal(expectedIsActiveColumnName, actual.IsActiveColumnName());
            }

            [Fact]
            public void Calling_FeatureNameColumnName_With_An_Empty_String_Doesnt_Change_The_FeatureNameColumnName()
            {
                // Arrange
                var expectedSchema = "Feature";
                var expectedTableName = "SpecificFeature";
                var expectedApplicationColumnName = "Application";
                var expectedScopeColumnName = "Scope";
                var expectedFeatureNameColumnName = "FeatureName";
                var expectedIsActiveColumnName = "IsActive";
                var actual = new SpecificDbContextConfiguration<object>(_ => { }, "OtherColumnName", _ => feature => false);

                // Act
                actual.FeatureNameColumnName("");

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
}
